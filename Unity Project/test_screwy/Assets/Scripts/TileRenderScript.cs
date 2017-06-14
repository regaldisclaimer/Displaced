using UnityEngine;
using System.Collections;

public class TileRenderScript : MonoBehaviour {

	public bool tileXAxis;
	public bool tileYAxis;

	public int renderNumber;

	void Awake () {
		float xAxisScale;
		float yAxisScale;

		if (tileXAxis) {
			xAxisScale = transform.lossyScale.x;
		} else {
			xAxisScale = 1;
		}

		if (tileYAxis) {
			yAxisScale = transform.lossyScale.y;
		} else {
			yAxisScale = 1;
		}

		transform.renderer.material.mainTextureScale = new Vector3(xAxisScale, yAxisScale, 1);

		gameObject.GetComponent<MeshRenderer>().material.renderQueue = renderNumber;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
