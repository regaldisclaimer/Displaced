using UnityEngine;
using System.Collections;

public class MenuManagerScript : MonoBehaviour {

	void Awake () {
		Screen.showCursor = false;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump")) {
			startNewGame();
		}
	}

	public void startNewGame () {
		GameManagerSingleton.Instance.startGame();
	}
}
