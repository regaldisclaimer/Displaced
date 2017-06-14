using UnityEngine;
using System.Collections;

public class PlayerControlScriptOld : MonoBehaviour {

	public float moveSpeed;
	public float baseJumpSpeed;
	public float jumpTime;
	public float baseWeight;
	public float gravityStrength;
	public float minJumpSpeed;
	public float groundingWeight; // The number of collectables required to stop jumping
	public float maxWeight;
	public Vector3 startPosition;

	private GameManagerSingleton gameManager;
	private PlayerSoundScript soundManager;

	private float hortSpeed;
	private float vertSpeed;

	private float timeJumping;

	private bool isJumping = false;
	private bool isTouchingLadder = false;
	private bool isOnLadder = false;
	private bool isLeavingLadder = false;

	private bool leftPressed = false;
	private bool rightPressed = false;
	private bool upPressed = false;
	private bool downPressed = false;
	// private bool spacePressed = false;

	private GameObject collidedLadder = null;

	private float heldWeight = 0; // Weight for picking up objects

	void Awake() {
		gameManager = GameManagerSingleton.Instance;
		soundManager = gameObject.GetComponent<PlayerSoundScript>();

		heldWeight = gameManager.getPlayerHeldWeight();
	}

	void Start() {
		rigidbody2D.gravityScale = gravityStrength;
	}

	void OnCollisionEnter2D (Collision2D other) {
		GameObject otherObject = other.gameObject;
		string otherTag = otherObject.tag;
		if (otherTag == "Collectable") {
			// TODO(tfs): Abstract this out and make it neater?
			heldWeight += otherObject.GetComponent<CollectableScript>().getWeight();
			if (heldWeight > maxWeight) {
				heldWeight = maxWeight;
			}
			Destroy(otherObject);
			soundManager.playPickupSound();
		} else if (otherTag == "Enemy") {
			reset();
		} else if (otherTag == "Breakable") {
			if (otherObject.GetComponent<BreakableScript>().getWeight() <= (heldWeight + baseWeight)) {
				if ((otherObject.transform.position.y + otherObject.collider2D.bounds.extents.y) < (transform.position.y - collider2D.bounds.extents.y)) {
					// This might be the ugliest conditional I've ever seen. But it's sort of beautiful, in its own way.
					// if ((otherObject.transform.position.x + otherObject.collider2D.bounds.extents.x) > (transform.position.x - collider2D.bounds.extents.x) && (otherObject.transform.position.x + otherObject.collider2D.bounds.extents.x) < (transform.position.x + rigidbody2D.bounds.extents.x)) {
					Destroy(otherObject);
				}
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		GameObject otherObject = other.gameObject;
		string otherTag = otherObject.tag;

		if (otherTag == "Ladder") {
			// Climb ladder
			isTouchingLadder = true;
			collidedLadder = otherObject;

			// if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)) {
			// 	stickToLadder();
			// }
		} else if (otherTag == "Door") {
			// Exit through door
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
				unstickFromLadder();
			}
			isLeavingLadder = false;
			collidedLadder = null;
		}
	}

	// Update is called once per frame
	void Update () {
		leftPressed = Input.GetKey(KeyCode.LeftArrow);
		rightPressed = Input.GetKey(KeyCode.RightArrow);
		upPressed = Input.GetKey(KeyCode.UpArrow);
		downPressed = Input.GetKey(KeyCode.DownArrow);

		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			if (isOnLadder) {
				// NOTE(tfs): ladder jumping is currently handled with ladder movement. blech.
				// 		add some more code to jump here instead?
			} else if ((rigidbody2D.velocity.y > -0.025) && (rigidbody2D.velocity.y < 0.025)) {
				// NOTE(tfs): We should probably switch to detecting ground collision as well... :/
				startJump();
			}
		}

		if (!isOnLadder) {
			if (leftPressed) {
				if (rightPressed) {
					hortSpeed = 0;
				} else {
					// Right not pressed
					hortSpeed = (-1) * moveSpeed;
				}
			} else {
				// Left not pressed
				if (rightPressed) {
					hortSpeed = moveSpeed;
				} else {
					// Right not pressed
					hortSpeed = 0;
				}
			}
		} else {
			hortSpeed = 0;
		}

		// Handle vertical movement
		vertSpeed = rigidbody2D.velocity.y; // Fall through without changing the vert speed unnecessarily

		if (isTouchingLadder) {
			if (isOnLadder) {
				if (upPressed) {
					if (downPressed) {
						vertSpeed = 0;
					} else {
						vertSpeed = moveSpeed * ((4 + calculateWeightRatio()) / 5);
					}
				} else if (downPressed) {
					if ((transform.position.y - collider2D.bounds.extents.y) <= (collidedLadder.transform.position.y - collidedLadder.collider2D.bounds.extents.y)) {
						unstickFromLadder();
					} else {
						vertSpeed = (-1) * moveSpeed * ((4 + calculateWeightRatio()) / 5);
					}
				} else {
					vertSpeed = 0;
				}

				if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
					if (upPressed) {
						startJump();
					} else {
						startJump();
					}
				}

				transform.position = new Vector3(collidedLadder.transform.position.x, transform.position.y, 0);
			} else {
				if (upPressed && collidedLadder && ((transform.position.y - collider2D.bounds.extents.y) <= (collidedLadder.transform.position.y + collidedLadder.collider2D.bounds.extents.y))) {
					stickToLadder();
				}
				if (downPressed && collidedLadder && (transform.position.y > collidedLadder.transform.position.y - collidedLadder.collider2D.bounds.extents.y)) {
					stickToLadder();
				}
				if (Input.GetKeyDown(KeyCode.UpArrow)) {
					if (collidedLadder && ((transform.position.y - collider2D.bounds.extents.y) <= (collidedLadder.transform.position.y + collidedLadder.collider2D.bounds.extents.y))) {
						isLeavingLadder = false;
						stickToLadder();
					}
				}
			}
		}

		if (isJumping && upPressed) {
			timeJumping += Time.deltaTime;

			if (timeJumping < jumpTime) {
				vertSpeed = Mathf.Max(minJumpSpeed, (baseJumpSpeed * calculateWeightRatio()));
			}
		}

		if (Input.GetKeyUp(KeyCode.UpArrow) || (timeJumping > (jumpTime * calculateWeightRatio()))) {
			isJumping = false;
			timeJumping = 0F;
			isLeavingLadder = false;
		}

		// Apply velocity changes calculated above
		rigidbody2D.velocity = new Vector3 (hortSpeed, vertSpeed, 0);

		// Press "R" to reset level
		if (Input.GetKey(KeyCode.R)) {
			reset();
		}

		// Press 'Q' to reset back to first lobby/level
		if (Input.GetKey(KeyCode.Q)) {
			gameManager.resetLevels();
		}
	}


	// Public call functions
	public void reset() {
		// soundManager.playDeathSound();
		gameManager.setPlayerHeldWeight(heldWeight);
		gameManager.resetCurrentLevel();
	}

	public void enemyCollision() {
		reset();
	}

	public void washAll() {
		heldWeight = 0;
		gameManager.setPlayerHeldWeight(0);
	}

	// Private functions
	private float calculateWeightRatio() {
		return ((groundingWeight - heldWeight) / groundingWeight);
	}

	private void startJump() {
		if (isOnLadder) {
			unstickFromLadder();
		}
		isJumping = true;
		timeJumping = 0F;
		soundManager.playJumpSound();
	}

	private void stickToLadder() {
		// Stick to ladder
		if (!isLeavingLadder) {
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
}
