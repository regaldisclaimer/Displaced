using UnityEngine;
using System.Collections;

public class LadderContactScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionExit2D (Collision2D other) {
		GameObject otherObject = other.gameObject;
		string otherTag = otherObject.tag;

		if (otherTag == "Player") {
			otherObject.SendMessage("leaveLadder", gameObject);
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		GameObject otherObject = other.gameObject;
		string otherTag = otherObject.tag;

		if (otherTag == "Player") {
			otherObject.SendMessage("leaveLadder", gameObject);
		}
	}
}
