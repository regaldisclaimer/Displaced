using UnityEngine;
using System.Collections;

public class PlayerAnimationScript : MonoBehaviour {

	// This is an abomination. I wish I knew how to use mecanim. But I don't.
	// 	Ideally this would be a state machine. Yay mecanim. But I'm not implementing my own state machine. So instead this ugly refuse exists.

	public Sprite[] standingFrames;
	public float standingFrameDelay;

	public Sprite[] leftWalkFramesFirst;
	public Sprite[] leftWalkFrames;
	public float leftWalkFrameDelay;

	public Sprite[] rightWalkFramesFirst;
	public Sprite[] rightWalkFrames;
	public float rightWalkFrameDelay;

	public Sprite[] jumpFrames;
	public float jumpFrameDelay;

	public Sprite[] fallFrames;
	public float fallFrameDelay;

	public Sprite[] ladderFrames;
	public float ladderFrameDelay;

	private float currentFrameDelay = 0F;
	private float timeInCurrentFrame = 0F;
	private Sprite[] currentAnimation = null;
	private int currentFrameIndex = 0;
	private bool isOneShot = false;
	private string animationTag = "";

	private int ladderFrameIndex = 0;
	private float ladderFrameTimer = 0;

	void Awake () {
		setAnimationStanding();
	}

	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void Update () {
		if (animationTag != "Ladder") {
			timeInCurrentFrame += Time.deltaTime;

			if (timeInCurrentFrame >= currentFrameDelay) {
				currentFrameIndex += 1;

				if (currentFrameIndex >= currentAnimation.Length) {
					if (isOneShot == false) {
						currentFrameIndex -= currentAnimation.Length;
					} else {
						setAnimationStanding();
					}
				}

				gameObject.GetComponent<SpriteRenderer>().sprite = currentAnimation[currentFrameIndex];
				timeInCurrentFrame = 0;
			}
		}
	}

	public void setAnimationStanding () {
		if (animationTag != "Standing") {
			currentAnimation = standingFrames;
			currentFrameDelay = standingFrameDelay;
			currentFrameIndex = 0;
			isOneShot = false;
			animationTag = "Standing";
		}
	}

	public void setAnimationLeftWalk () {
		if (animationTag != "LeftWalk") {
			currentAnimation = leftWalkFrames;
			currentFrameDelay = leftWalkFrameDelay;
			currentFrameIndex = 0;
			isOneShot = false;
			animationTag = "LeftWalk";
		}
	}

	public void setAnimationRightWalk () {
		if (animationTag != "RightWalk") {
			currentAnimation = rightWalkFrames;
			currentFrameDelay = rightWalkFrameDelay;
			currentFrameIndex = 0;
			isOneShot = false;
			animationTag = "RightWalk";
		}
	}

	public void playAnimationJump () {
		if (animationTag != "Jump") {
			currentAnimation = jumpFrames;
			currentFrameDelay = jumpFrameDelay;
			currentFrameIndex = 0;
			isOneShot = true;
			animationTag = "Jump";
		}
	}

	public void setAnimationFall () {
		if (animationTag != "Fall") {
			currentAnimation = fallFrames;
			currentFrameDelay = fallFrameDelay;
			currentFrameIndex = 0;
			isOneShot = false;
			animationTag = "Fall";
		}
	}

	public void incrementLadderAnimation (float deltaTime) {
		animationTag = "Ladder";
		ladderFrameTimer += deltaTime;

		if (ladderFrameTimer >= ladderFrameDelay) {
			ladderFrameIndex += 1;

			if (ladderFrameIndex >= ladderFrames.Length) {
				ladderFrameIndex -= ladderFrames.Length;
			}

			gameObject.GetComponent<SpriteRenderer>().sprite = ladderFrames[ladderFrameIndex];
			ladderFrameTimer = 0;
		}

		// }
	}
}
