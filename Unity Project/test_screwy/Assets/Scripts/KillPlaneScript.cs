//Just for testing purposes. Will be removed.
//Further, colllison script will be on playerScript

using UnityEngine;
using System.Collections;

public class KillPlaneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.GetComponent<PlayerControlScript>() != null) {
			other.transform.position = new Vector3(0, 0, 0);
			other.transform.position = new Vector3(0, 0, 0);
		}
	}
}
