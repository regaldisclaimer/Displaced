using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeightIndicatorTextScript : MonoBehaviour {

	private GameManagerSingleton gameManager;
	Text txt;

	void Awake() {
		gameManager = GameManagerSingleton.Instance;
	}

	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text>(); 
		txt.text = gameManager.getPlayerHeldWeight().ToString();
		//Debug.Log(gameManager.getPlayerHeldWeight());
	}
	
	// Update is called once per frame
	void Update () {
		txt.text = gameManager.getPlayerHeldWeight().ToString();
	}
}
