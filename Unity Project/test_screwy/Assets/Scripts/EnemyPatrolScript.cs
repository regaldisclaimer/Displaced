using UnityEngine;
using System.Collections;

public class EnemyPatrolScript : MonoBehaviour {

	public float patrolSpeed;
	public float patrolDistance;
	public bool startFacingLeft;

	private Vector3 prevPosition;

	// Use this for initialization
	void Start () {
		if (startFacingLeft) {
			rigidbody2D.velocity = patrolSpeed * Vector3.left;
		} else {
			rigidbody2D.velocity = patrolSpeed * Vector3.right;
		}
		prevPosition = transform.position;
	}

	// This gets around the player's collider becoming a trigger on ladders
	// void OnTriggerEnter2D (Collider2D other) {
	// 	GameObject otherObject = other.gameObject;
	// 	string otherTag = otherObject.tag;

	// 	// if (otherTag == "Player") {
	// 	// 	otherObject.GetComponent<PlayerControlScript>().SendMessage("enemyCollision");
	// 	// }
	// }

	// Update is called once per frame
	void FixedUpdate () {
		if (calculateDistanceTraveled() >= patrolDistance) {
			prevPosition = transform.position;
			rigidbody2D.velocity = new Vector3 ((-1) * rigidbody2D.velocity.x, rigidbody2D.velocity.y, 0);
		}
	}

	private float calculateDistanceTraveled() {
		float distance = transform.position.x - prevPosition.x;
		return Mathf.Max(distance, (-1) * distance);
	}
}
