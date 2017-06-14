using UnityEngine;
using System.Collections;

public class EnemyAnimationScript : MonoBehaviour {

	// This is an abomination. I wish I knew how to use mecanim. But I don't.
	// 	Ideally this would be a state machine. Yay mecanim. But I'm not implementing my own state machine. So instead this ugly refuse exists.

	public Sprite[] frames;
	public float frameDelay;

	private float currentFrameDelay = 0F;
	private float timeInCurrentFrame = 0F;
	private Sprite[] currentAnimation = null;
	private int currentFrameIndex = 0;

	void Awake () {
		currentAnimation = frames;
		currentFrameDelay = frameDelay;
	}

	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void Update () {
		timeInCurrentFrame += Time.deltaTime;

		if (timeInCurrentFrame >= currentFrameDelay) {
			currentFrameIndex += 1;

			if (currentFrameIndex >= currentAnimation.Length) {
				currentFrameIndex -= currentAnimation.Length;
			}

			gameObject.GetComponent<SpriteRenderer>().sprite = currentAnimation[currentFrameIndex];
			timeInCurrentFrame = 0;
		}
	}

	// public void setAnimationStanding () {
	// 	if (animationTag != "Standing") {
	// 		currentAnimation = standingFrames;
	// 		currentFrameDelay = standingFrameDelay;
	// 		currentFrameIndex = 0;
	// 		isOneShot = false;
	// 		animationTag = "Standing";
	// 	}
	// }

	// public void setAnimationLeftWalk () {
	// 	if (animationTag != "LeftWalk") {
	// 		currentAnimation = leftWalkFrames;
	// 		currentFrameDelay = leftWalkFrameDelay;
	// 		currentFrameIndex = 0;
	// 		isOneShot = false;
	// 		animationTag = "LeftWalk";
	// 	}
	// }

	// public void setAnimationRightWalk () {
	// 	if (animationTag != "RightWalk") {
	// 		currentAnimation = rightWalkFrames;
	// 		currentFrameDelay = rightWalkFrameDelay;
	// 		currentFrameIndex = 0;
	// 		isOneShot = false;
	// 		animationTag = "RightWalk";
	// 	}
	// }

	// public void playAnimationJump () {
	// 	if (animationTag != "Jump") {
	// 		currentAnimation = jumpFrames;
	// 		currentFrameDelay = jumpFrameDelay;
	// 		currentFrameIndex = 0;
	// 		isOneShot = true;
	// 		animationTag = "Jump";
	// 	}
	// }

	// public void setAnimationFall () {
	// 	if (animationTag != "Fall") {
	// 		currentAnimation = fallFrames;
	// 		currentFrameDelay = fallFrameDelay;
	// 		currentFrameIndex = 0;
	// 		isOneShot = false;
	// 		animationTag = "Fall";
	// 	}
	// }
}
