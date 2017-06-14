using UnityEngine;
using System.Collections;

public class PlatformRenderScript : MonoBehaviour {

	void Awake () {
		// float xScale = transform.lossyScale.x;
		// float yScale = transform.lossyScale.y;

		transform.renderer.material.mainTextureScale = transform.lossyScale;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
