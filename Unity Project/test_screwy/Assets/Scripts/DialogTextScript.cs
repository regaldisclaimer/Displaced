using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogTextScript : MonoBehaviour {

	public Transform instParentLocation;
	public Transform mainParentLocation;
	public Transform loudParentLocation;
	public Transform fastParentLocation;
	public Transform strongParentLocation;
	private GameManagerSingleton gameManager;
	private GameObject theImage;
	public GameObject instructionImage;
	public GameObject mainImage;
	public GameObject loudImage;
	public GameObject fastImage;
	public GameObject strongImage;
	Text txt;

	void Awake () {
		gameManager = GameManagerSingleton.Instance;
	}

	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp("Cancel")) {

			//get input
			string inputText = gameManager.getNextString();

			if (inputText=="") {
				theImage.transform.parent.gameObject.SetActive(false);
				txt.text = "";
			} else {
				GameObject oldImage = theImage;
				//parse id
				string idText = inputText.Substring(0,1);
				//set image
				theImage = pickImage(idText);
				theImage.transform.SetParent(pickParentLocation(idText));
				Transform textBox = GameObject.Find("DialogText").transform;
				textBox.transform.SetParent(theImage.transform);
				// textBox.gameObject.GetComponent<RectTransform>().anchoredPosition = pickParentLocation(idText).position;
				textBox.gameObject.GetComponent<RectTransform>().anchoredPosition = pickTextOffset(idText);
				//theImage.SetActive(true);
				txt.text = inputText.Substring(2);
				oldImage.SetActive(false);
			}
		}
	}

	public void fakeInstantiate() {
		string inputText = gameManager.getNextString();
		string idText = inputText.Substring(0,1);
		gameObject.GetComponent<Text>().text = inputText.Substring(2);
		theImage = pickImage(idText);
		theImage.transform.SetParent(pickParentLocation(idText));
		Transform textBox = GameObject.Find("DialogText").transform;
		textBox.transform.SetParent(theImage.transform);
		// textBox.gameObject.GetComponent<RectTransform>().anchoredPosition = pickParentLocation(idText).position;
		textBox.gameObject.GetComponent<RectTransform>().anchoredPosition = pickTextOffset(idText);
		theImage.SetActive(true);
	}

	private Vector3 pickTextOffset (string idText) {
		float y = 0F;

		if (idText == "i") {
			y = 0F;
		} else if (idText == "m") {
			y = 0F;
		} else if (idText == "l") {
			y = 0F;
		} else if (idText == "f") {
			y = 50F;
		} else if (idText == "s") {
			y = 50F;
		} else {
			y = 0F;
		}

		return new Vector3(0F, y, 0F);
	}

	private Transform pickParentLocation(string idText) {
		if (idText == "i") {
			return instParentLocation;
		} else if (idText == "m") {
			return mainParentLocation;
		} else if (idText == "l") {
			return loudParentLocation;
		} else if (idText == "f") {
			return fastParentLocation;
		} else if (idText == "s") {
			return strongParentLocation;
		} else {
			return instParentLocation;
		}
	}

	private GameObject pickImage(string idText) {
		GameObject theImage = null;

		if (idText == "i") {
			theImage = Instantiate(instructionImage, instParentLocation.position, instParentLocation.rotation) as GameObject;
		} else if (idText == "m") {
			theImage = Instantiate(mainImage, mainParentLocation.position, mainParentLocation.rotation) as GameObject;
		} else if (idText == "l") {
			theImage = Instantiate(loudImage, loudParentLocation.position, loudParentLocation.rotation) as GameObject;
		} else if (idText == "f") {
			theImage = Instantiate(fastImage, fastParentLocation.position, fastParentLocation.rotation) as GameObject;
		} else if (idText == "s") {
			theImage = Instantiate(strongImage, strongParentLocation.position, strongParentLocation.rotation) as GameObject;
		}

		return theImage;
	}
}
