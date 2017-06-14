using UnityEngine;
using System.Collections;

public class BoosterAnimationScript : MonoBehaviour {

	public Sprite[] fireFrames;
	public float frameDelay;

	private float timeInCurrentFrame = 0.0F;
	private int frameIndex = 0;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		timeInCurrentFrame += Time.deltaTime;

		if (timeInCurrentFrame >= frameDelay) {
			frameIndex += 1;

			if (frameIndex >= fireFrames.Length) {
				frameIndex -= fireFrames.Length;
			}

			gameObject.GetComponent<SpriteRenderer>().sprite = fireFrames[frameIndex];
			timeInCurrentFrame = 0;
		}
	}
}
