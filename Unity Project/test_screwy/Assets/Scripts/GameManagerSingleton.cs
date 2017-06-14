using UnityEngine;
using System.Collections;

public class GameManagerSingleton : MonoBehaviour {
	// Singleton implementation
	private static GameManagerSingleton _instance;
	public static GameManagerSingleton Instance {
		// Accessor for singleton
		get {
			if (!_instance) {
				// NOTE(tfs): Should switch to array and check if any others exist
				_instance = GameObject.FindObjectOfType(typeof(GameManagerSingleton)) as GameManagerSingleton;
				if (!_instance) {
					GameObject container = new GameObject();
					container.name = "GameManagerContainer";
					_instance = container.AddComponent(typeof(GameManagerSingleton)) as GameManagerSingleton;
				}
			}

			return _instance;
		}
	}

	// Main code
	private int currentLevelNumber;
	private int lobbyLevelNumber = 1; // This really shouldn't change. Would be problematic in current implementation.
	private int menuLevelNumber;
	private float playerHeldWeight;
	private string[] dialogArray;
	private int dialogIndex;
	private bool isFromDed = false;


	void Awake () {
		// Do all initializations here
		menuLevelNumber = 0;
		DontDestroyOnLoad(transform.gameObject); // This way data persists between scene changes
		// currentLevelNumber = lobbyLevelNumber + 1;
		currentLevelNumber = Mathf.Max(lobbyLevelNumber + 1, Application.loadedLevel);
	}

	void Update () {
		if (Input.GetButtonDown("ResetLevel")) {
			resetCurrentLevel();
		}

		if (Input.GetButtonDown("QuitToMenu")) {
			resetLevels();
		}
	}

	//level transition
	public void nextLevel () {
		if ((Application.loadedLevel == lobbyLevelNumber)) {
			// currentLevelNumber += 1;
			if (Application.CanStreamedLevelBeLoaded(currentLevelNumber)) {
				Application.LoadLevel(currentLevelNumber);
			} else {
				// Debug.Log("End of game! Resetting...");
				resetLevels();
			}
		} else {
			//entering lobby
			if (Application.CanStreamedLevelBeLoaded(lobbyLevelNumber)) {
				currentLevelNumber += 1;
				// Debug.Log(currentLevelNumber);
				isFromDed = false;
				Application.LoadLevel(lobbyLevelNumber);
			} else {
				// Debug.Log("End of game! Resetting...");
				resetLevels();
			}
		}
	}

	public void resetCurrentLevel() {
		isFromDed = true;
		Application.LoadLevel(lobbyLevelNumber);
	}

	public void resetLevels () {
		currentLevelNumber = lobbyLevelNumber + 1;
		playerHeldWeight = 0;
		Application.LoadLevel(menuLevelNumber);
	}

	public void startGame () {
		currentLevelNumber = lobbyLevelNumber + 1;
		playerHeldWeight = 0;
		Application.LoadLevel(lobbyLevelNumber);
	}

	public void setPlayerHeldWeight (float newWeight) {
		playerHeldWeight = newWeight;
	}

	public float getPlayerHeldWeight () {
		return playerHeldWeight;
	}

    public void setDialogString (string[] stringArray) {
    	dialogArray = stringArray;
    	dialogIndex = -1;
    }

	public string getNextString () {
		dialogIndex++;
		if (dialogIndex >= dialogArray.Length) {
			GameObject.Find("Player").GetComponent<PlayerControlScript>().exitCutscene();
			GameObject op1 = GameObject.Find("JumpOpponent");
			GameObject op2 = GameObject.Find("FastOpponent");
			if (op1 != null) {
				op1.GetComponent<Opponent1ControlScript>().exitCutscene();
			}
			if (op2 != null) {
				op2.GetComponent<Opponent1ControlScript>().exitCutscene();
			}
			GameObject.Find("Cutscene").SetActive(false);
			return "";
		} else {
			return dialogArray[dialogIndex];
		}
	}

	public bool isDeathSpawn() {
		return isFromDed;
	}

	public string getConversationID () {
		//check if ded
		if (isFromDed) {
			return "DeathSequence1";
		} else {
			//if not ded
			if (currentLevelNumber-lobbyLevelNumber == 1) { //next level is level 1.
				return "LobbySequence1";
			} else if (currentLevelNumber-lobbyLevelNumber == 2) {
				return "LobbySequence2";
			} else if (currentLevelNumber-lobbyLevelNumber == 3) {
				return "LobbySequence3";
			} else if (currentLevelNumber-lobbyLevelNumber == 4) {
				return "LobbySequence4";
			} else if (currentLevelNumber-lobbyLevelNumber == 5) {
				return "LobbySequence5";
			} else if (currentLevelNumber-lobbyLevelNumber == 6) {
				return "LobbySequence6";
			} else if (currentLevelNumber-lobbyLevelNumber == 7) {
				return "LobbySequence7";
			}
			return "dead Squirrel";
		}

	}
}
