using UnityEngine;
using System.Collections;

public class FloatingPlatformWobbleScript : MonoBehaviour {

	// There is some SERIOUS physics thought needed for this (without juggling player)

	// public float wobbleDistance;
	// public float wobbleTime;

	// private Vector3 startPosition;
	// private Vector3 wobblePosition;

	// private bool isWobbling = false;

	void Awake () {
		// startPosition = transform.position;
		// wobblePosition = new Vector3(transform.position.x, transform.position.y - wobbleDistance, transform.position.z);
	}

	// Use this for initialization
	void Start () {

	}

	void FixedUpdate () {

	}

	// Update is called once per frame
	void Update () {

	}

	// void OnCollisionEnter2D(Collision2D other) {
	// 	if (other.gameObject.tag == "Player") {
	// 		if (other.transform.position.y - other.gameObject.collider2D.bounds.extents.y >= transform.position.y + collider2D.bounds.extents.y) {
	// 			wobble();
	// 		}
	// 	}
	// }

	// private void wobble () {
	// 	isWobbling = true;
	// }
}
