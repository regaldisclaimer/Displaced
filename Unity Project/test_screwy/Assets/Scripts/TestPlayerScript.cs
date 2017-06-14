using UnityEngine;
using System.Collections;

public class TestPlayerScript : MonoBehaviour {

	public float moveForce;
	public float maxSpeed;
	public float maxLadderSpeed;
	public float friction;
	public float minWeightlessJumpHeight;
	public float maxWeightlessJumpHeight;
	public float gravityStrength;
	public float maxWeight;
	public float minWeightedJumpHeight;
	public float jumpControlLength; // Scales extra jump force
	public float ladderStickThreshold;
	public float inAirFallThreshold;
	public float inAirHortFallRatio;

	public Transform groundingUpperLeft;
	public Transform groundingLowerRight;
	public LayerMask groundLayer;
	public LayerMask ladderLayer;

	private bool canJump = true;
	private bool isGrounded = true;

	private bool isOnLadder = false;
	private bool isTouchingLadder = false;
	private bool isLeavingLadder = false;
	private bool isJumping = false; // To implement, keeps track of whether jump button has been released
	private GameObject collidedLadder = null;
	private float heldWeight = 0;
	private float timeJumping = 0; // To implement, keeps track of how long button is held
	private float initialJumpVelocity;
	private Vector3 continuingJumpForce;

	// NOTE(tfs): "jumping" refers to adding force to rise
	// 				After this, I call the action "falling" (even while veritical velocity is still positive)

	private GameManagerSingleton gameManager;
	private PlayerSoundScript soundManager;

	// Called once, on instantiation
	void Awake () {
		gameManager = GameManagerSingleton.Instance;
		soundManager = gameObject.GetComponent<PlayerSoundScript>();

		heldWeight = gameManager.getPlayerHeldWeight();

		calculateInitialJumpVelocity();
		calculateContinuingJumpForceVector();
	}

	// Use this for initialization
	void Start () {

	}

	void OnCollisionEnter2D (Collision2D other) {
		GameObject otherObject = other.gameObject;
		string otherTag = otherObject.tag;

		if (otherTag == "Collectable") {
			heldWeight += otherObject.GetComponent<CollectableScript>().getWeight();
			if (heldWeight > maxWeight) {
				heldWeight = maxWeight;
			}
			calculateInitialJumpVelocity();
			calculateContinuingJumpForceVector();
			Destroy(otherObject);
			soundManager.playPickupSound();
		} else if (otherTag == "Enemy") {
			reset();
		} else if (otherTag == "Breakable") {
			if (otherObject.GetComponent<BreakableScript>().getWeight() <= (heldWeight)) {
				if ((otherObject.transform.position.y + otherObject.collider2D.bounds.extents.y) < (transform.position.y - collider2D.bounds.extents.y)) {
					Destroy(otherObject);
				}
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		GameObject otherObject = other.gameObject;
		string otherTag = otherObject.tag;

		if (otherTag == "Ladder") {
			isTouchingLadder = true;
			collidedLadder = otherObject;
		} else if (otherTag == "Door") {
			gameManager.setPlayerHeldWeight(heldWeight);
			gameManager.nextLevel();
		} else if (otherTag == "Shower") {
			washAll();
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		GameObject otherObject = other.gameObject;
		string otherTag = otherObject.tag;

		if (otherTag == "Ladder") {
			if (isTouchingLadder) {
				isTouchingLadder = false;
			}
			unstickFromLadder();
			isLeavingLadder = false;
			collidedLadder = null;
		}
	}

	// Called on reliable timer
	void FixedUpdate () {
		float hortAxis = Input.GetAxis("Horizontal");
		float vertAxis = Input.GetAxis("Vertical");

		// Check groundedness
		checkGrounded();

		// Check jumping
		checkCanJump();

		// Handle continued jumping
		if (isJumping) {
			addForceIfJumpButtonPressed();
		}

		// Check ladder movement
		handleLadderMovement(hortAxis, vertAxis);

		// // Handle horizontal movement
		if (!isOnLadder) {
			Vector3 forceVector = new Vector3(hortAxis * moveForce, 0, 0);

			rigidbody2D.AddForce(forceVector);
		}

		// // Limit horizontal speed
		limitHorizontalSpeed(hortAxis, vertAxis);

		// Apply friction
		if (isGrounded) {
			enableFriction();
		} else {
			disableFriction();
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump")) {
			jump();
		}

		if (Input.GetButtonUp("Jump")) {
			isJumping = false;
			isLeavingLadder = false;
		}

		if (isJumping) {
			checkJumpTime(Time.deltaTime);
		}
	}

	// INTERNAL  CALLS

	// Check functions
	private void checkGrounded () {
		isGrounded = Physics2D.OverlapArea(groundingUpperLeft.position, groundingLowerRight.position, groundLayer);
		if (isGrounded) {
			isLeavingLadder = false;
		}
	}

	private void checkCanJump () {
		canJump = (isGrounded || isOnLadder);
	}

	private void checkJumpTime (float deltaTime) {
		timeJumping += deltaTime;

		if (timeJumping >= jumpControlLength) {
			isJumping = false;
			timeJumping = 0;
		}
	}

	private void checkLadderContact () {
		Vector2 upperLeft = new Vector2(transform.position.x - collider2D.bounds.extents.x, transform.position.y + collider2D.bounds.extents.y);
		Vector2 lowerRight = new Vector2(transform.position.x + collider2D.bounds.extents.x, transform.position.y - collider2D.bounds.extents.y);
		isTouchingLadder = Physics2D.OverlapArea(upperLeft, lowerRight, ladderLayer);
		if (isTouchingLadder == false) {
			isLeavingLadder = false;
		}

		if (isGrounded) {
			isLeavingLadder = false;
		}
	}

	private bool checkLanding (GameObject other) {
		float selfHortExtent = collider2D.bounds.extents.x;
		float selfLeftBound = transform.position.x - selfHortExtent;
		float selfRightBound = transform.position.x + selfHortExtent;
		float selfBotBound = transform.position.y - collider2D.bounds.extents.y;

		float otherHortExtent = other.collider2D.bounds.extents.x;
		float otherLeftBound = other.transform.position.x - otherHortExtent;
		float otherRightBound = other.transform.position.x + otherHortExtent;
		float otherTopBound = other.transform.position.y + other.collider2D.bounds.extents.y;

		return ((selfLeftBound > otherLeftBound) && (selfRightBound < otherRightBound) && (selfBotBound > otherTopBound));
	}

	// Functions that update state/movement

	private void handleLadderMovement (float hortAxis, float vertAxis) {
		float hortVelocity;
		float vertVelocity;

		if (isTouchingLadder) {
			if (isOnLadder) {
				vertVelocity = vertAxis * maxLadderSpeed;
				hortVelocity = 0;

				rigidbody2D.velocity = new Vector3(hortVelocity, vertVelocity, 0F);
				transform.position = new Vector3(collidedLadder.transform.position.x, transform.position.y, transform.position.z);
				if ((vertAxis < 0) && (collidedLadder != null) && (((transform.position.y - collider2D.bounds.extents.y) < collidedLadder.transform.position.y - collidedLadder.collider2D.bounds.extents.y))) {
					unstickFromLadder();
				}
			} else {
				if (Mathf.Abs(vertAxis) < 0.5) {
					vertAxis = 0;
				}

				if ((vertAxis > ladderStickThreshold) && (collidedLadder != null) && ((transform.position.y - collider2D.bounds.extents.y) <= (collidedLadder.transform.position.y + collidedLadder.collider2D.bounds.extents.y))) {
					stickToLadder();
				}

				if ((vertAxis < (-1) * ladderStickThreshold) && (collidedLadder != null) && (((transform.position.y - collider2D.bounds.extents.y) >= collidedLadder.transform.position.y - collidedLadder.collider2D.bounds.extents.y))) {
					stickToLadder();
				}
			}
		}
	}

	private void limitHorizontalSpeed (float hortAxis, float vertAxis) {
		float hortVelocity = rigidbody2D.velocity.x;
		float vertVelocity = rigidbody2D.velocity.y;
		if (hortVelocity > 0) {
			if (hortVelocity > maxSpeed) {
				if (isGrounded == false && hortAxis < inAirFallThreshold) {
					hortVelocity = maxSpeed * inAirHortFallRatio;
				} else {
					hortVelocity = maxSpeed;
				}
			}
		} else {
			if (hortVelocity < (maxSpeed * (-1))) {
				if (isGrounded == false && hortAxis > ((-1) * inAirFallThreshold)) {
					hortVelocity = maxSpeed * inAirHortFallRatio * (-1);
				} else {
					hortVelocity = maxSpeed * (-1);
				}
			}
		}

		rigidbody2D.velocity = new Vector3(hortVelocity, vertVelocity, 0F);
	}

	private void addForceIfJumpButtonPressed () {
		if (Input.GetButton("Jump")) {
			rigidbody2D.AddForce(continuingJumpForce);
		}
	}

	// Use single impulse
	private void jump () {
		if (canJump) {
			if (isOnLadder) {
				unstickFromLadder();
			}

			Vector3 impulseVector = new Vector3(0F, initialJumpVelocity * rigidbody2D.mass, 0F);

			rigidbody2D.AddForce(impulseVector, ForceMode2D.Impulse);
			canJump = false;
			isJumping = true;
			timeJumping = 0;

			disableFriction();

			soundManager.playJumpSound();
		}
	}

	private void calculateInitialJumpVelocity () {
		// The jump event scales the minimum weightless jump height based on weight for impulse calc
		// 		Increasing jump height is managed by scaling jump height difference
		// 		and applying a force counter/equal to gravity (in update)

		float weightedJumpHeight = minWeightlessJumpHeight * (1 - (heldWeight / maxWeight));
		if (weightedJumpHeight < minWeightedJumpHeight) {
			weightedJumpHeight = minWeightedJumpHeight;
		}

		initialJumpVelocity = Mathf.Sqrt((-2) * weightedJumpHeight * Physics2D.gravity.y * rigidbody2D.gravityScale);
	}

	private void calculateContinuingJumpForceVector () {
		// Scaled based on length of jump control time
		float gravityAcceleration = (Physics2D.gravity.y * gravityStrength);
		float scaledJumpHeightDifference = (maxWeightlessJumpHeight - minWeightlessJumpHeight) * (1- (heldWeight / maxWeight)); // Unlike jump height, this can be zero

		// Yay physics. I'm not a physicist. This is probably wrong somehow.
		float acceleration = Mathf.Sqrt(((2 * initialJumpVelocity * jumpControlLength) + (gravityAcceleration * jumpControlLength * jumpControlLength) - (2 * gravityAcceleration * scaledJumpHeightDifference) / (jumpControlLength * jumpControlLength)));
		if (float.IsNaN(acceleration)) {
			acceleration = 0;
		}

		continuingJumpForce = new Vector3(0F, (acceleration * rigidbody2D.mass), 0F);
	}

	private void enableFriction () {
		collider2D.sharedMaterial.friction = friction;
		collider2D.enabled = false;
		collider2D.enabled = true;
	}

	private void disableFriction () {
		collider2D.sharedMaterial.friction = 0;
		collider2D.enabled = false;
		collider2D.enabled = true;
	}

	private void stickToLadder() {
		// Stick to ladder
 		if (isLeavingLadder == false) {
			isOnLadder = true;
			rigidbody2D.gravityScale = 0;
			collider2D.isTrigger = true;
			transform.position = new Vector3(collidedLadder.transform.position.x, transform.position.y, 0);
		}
	}

	private void unstickFromLadder() {
		isOnLadder = false;
		collider2D.isTrigger = false;
		rigidbody2D.gravityScale = gravityStrength;
		isLeavingLadder = true;
	}

	private void washAll() {
		heldWeight = 0;
		gameManager.setPlayerHeldWeight(0);

		calculateInitialJumpVelocity();
		calculateContinuingJumpForceVector();
	}

	// Special event functions (e.g. reseting)

	public void reset() {
		// soundManager.playDeathSound();
		gameManager.setPlayerHeldWeight(heldWeight);
		gameManager.resetCurrentLevel();
	}
}
