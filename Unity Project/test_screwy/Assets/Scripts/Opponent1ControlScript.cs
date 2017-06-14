using UnityEngine;
using System.Collections;

public class Opponent1ControlScript : MonoBehaviour {

	public float moveSpeed;
	public float jumpSpeed;
	public float gravityStrength;
	public float jumpTime;

	public bool startInCutscene;

	private bool isOnLadder;
	private bool isTouchingLadder;
	private GameObject collidedLadder = null;
	private bool leftPressed = false;
	private bool rightPressed = false;
	private bool upPressed = false;
	private bool downPressed = false;
	private bool spacePressed = false;
	private float hortSpeed;
	private float vertSpeed;
	private float timeJumping;
	private bool isJumping;

	private bool isInteractive = true;

	void Awake() {
		if (startInCutscene) {
			isInteractive = false;
		}
	}

	// Use this for initialization
	void Start () {

	}

	// FixedUpdate for rigidbody
	void FixedUpdate () {
		if (isInteractive) {
			rightPressed = true;
			upPressed = true;

			//------------------- Use Implementation from PlayerControlScript ----------------------//

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
							vertSpeed = moveSpeed;
						}
					} else if (downPressed) {
						vertSpeed = (-1) * moveSpeed;
					} else {
						vertSpeed = 0;
					}

					transform.position = new Vector3(collidedLadder.transform.position.x, transform.position.y, 0);
				} else {
					if (upPressed || downPressed) {
						stickToLadder();
					}
				}
			}

			if (spacePressed) {
				if (isJumping) {
					timeJumping += Time.deltaTime;

					if (timeJumping < jumpTime) {
						vertSpeed = Mathf.Max(jumpSpeed);
					}
				}
			}

			if (Input.GetKeyUp(KeyCode.Space) || (timeJumping > (jumpTime))) {
				isJumping = false;
				timeJumping = 0F;
			}


			// Apply velocity changes calculated above
			rigidbody2D.velocity = new Vector3 (hortSpeed, vertSpeed, 0);

			//transform instead of getting velocity
			//transform.Translate(hortSpeed*Time.deltaTime, vertSpeed*Time.deltaTime, 0);
		}
	}

	//Implementation from PlayerControlScript
	void OnTriggerEnter2D (Collider2D other) {
		GameObject otherObject = other.gameObject;
		string otherTag = otherObject.tag;

		if (otherTag == "Ladder") {
			isTouchingLadder = true;
			collidedLadder = otherObject;

			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || upPressed) {
				stickToLadder();
			}
		} else if (otherTag == "Door"||otherTag == "OpponentDoor") {
			disappear();
		} else if (otherTag == "JumpTrigger") {
			spacePressed = true;
			isJumping = true;
		} else if (otherTag == "OpponentReverser") {
			moveSpeed = -1.0f * moveSpeed;
		} else if (otherTag == "RightJumpTrigger") {
			if(moveSpeed>0){
				spacePressed = true;
				isJumping = true;
			}
		} else if (otherTag == "RightOpponentReverser") {
			if(moveSpeed>0){
				moveSpeed = -1.0f * moveSpeed;
			}
		} else if (otherTag == "LeftOpponentReverser") {
			if(moveSpeed<0){
				moveSpeed = -1.0f * moveSpeed;
			}
		}
	}

	//Implementation from PlayerControlScript
	void OnTriggerExit2D (Collider2D other) {
		GameObject otherObject = other.gameObject;
		string otherTag = otherObject.tag;

		if (otherTag == "Ladder") {
			if (isTouchingLadder) {
				isTouchingLadder = false;
				unstickFromLadder();
			}

			collidedLadder = null;
		}
	}

	public void exitCutscene() {
		isInteractive = true;
	}

	// Implementation from PlayerControlScript
	private void stickToLadder() {
		// Stick to ladder
		isOnLadder = true;
		rigidbody2D.gravityScale = 0;
		transform.position = new Vector3(collidedLadder.transform.position.x, transform.position.y, 0);
		collider2D.isTrigger = true;
	}

	// Implementation from PlayerControlScript
	private void unstickFromLadder() {
		isOnLadder = false;
		rigidbody2D.gravityScale = gravityStrength;
		collider2D.isTrigger = false;
	}

	private void disappear() {
		// Get rid of opponent (e.g. when they hit the door)
		Destroy(gameObject);
	}
}
