using UnityEngine;
using System.Collections;

public class CollectableRenderScript : MonoBehaviour {

	public Sprite[] images;

	void Awake () {
		gameObject.GetComponent<SpriteRenderer>().sprite = images[Random.Range(0, images.Length)];
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
