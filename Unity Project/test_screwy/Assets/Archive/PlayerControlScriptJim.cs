using UnityEngine;
using System.Collections;

public class PlayerControlScriptJim : MonoBehaviour {

	public float moveSpeed;
	public float jumpSpeed;

	private bool vertRelease = false;
	private bool onGround = true;


	////////////////////
	///////////////////
	//TODO: Put rigidbody on platform, lock its movements, and make trigger collider?
	//having two colliders may prevent one from touching the other...


	void OnTriggerEnter(Collider other) {
		//!TO IMPLEMENT
	    //Check that the player's bottom side collides with another object before it can jump again.
	    //Right now only attempts to check that it collides with a platform
	    //this isn't even working...
	    //OnTriggerEnter nor OnTriggerExit fire...
	    onGround = true;
	    Debug.Log("true");
	}

	void OnTriggerExit(Collider other) {
		onGround = false;
		Debug.Log("false");
	}



	void Update () {
		////////////////
	    ////////////////		Jim's Implementation
	    ////////////////

	    //Supposedly this is a common issue for a lot of people. Good video example of a solution is given here:
	    //http://forum.unity3d.com/threads/2d-platformer-no-unity-physics-or-animator-controller-possible.291283/
	    //From my research, I concluded that we want to be using colliders to check if the player is on the ground, or touching a side. 


	    //WHILE horizontal down, give HORIZONTAL speed
	    //On horizontal up, set HORIZONTAL speed to 0

	    //if(!check for collision here) {
	    transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);
	    //} 





	    //On vertical down ONCE, give VERTICAL SPEED
	    if (Input.GetKeyDown(KeyCode.UpArrow)&&onGround) {

	    	//Jumped, so on next arrow release, speed can reset once
	    	vertRelease = true;
	    	rigidbody2D.velocity = new Vector3(rigidbody2D.velocity.x, jumpSpeed, 0);

	    } else if (Input.GetKeyUp(KeyCode.UpArrow)) {

	    	//reset vert speed to zero only if jumped
	    	if (vertRelease) {
	    		rigidbody2D.velocity = new Vector3(rigidbody2D.velocity.x, 0, 0);
	    		vertRelease = false;
	    	}
	    }
	    //On vertical up AFTER down ONCE, set VERTICAL SPEED to 0
	}
}
