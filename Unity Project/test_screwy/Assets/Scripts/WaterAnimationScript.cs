using UnityEngine;
using System.Collections;

public class WaterAnimationScript : MonoBehaviour {

	public int frameDelay;
	public Sprite[] images;

	private int animIndex = 0;
	private int frameCount = 0;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		frameCount += 1;
		if (frameCount >= frameDelay) {
			animIndex += 1;

			while (animIndex >= images.Length) {
				animIndex -= images.Length;
			}

			gameObject.GetComponent<SpriteRenderer>().sprite = images[animIndex];

			frameCount = 0;
		}
	}
}
