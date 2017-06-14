using UnityEngine;
using System.Collections;

public class SetRenderQueueScript : MonoBehaviour {

	public int renderNumber;

	void Awake () {
		gameObject.GetComponent<MeshRenderer>().material.renderQueue = renderNumber;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
