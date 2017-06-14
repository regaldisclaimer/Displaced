using UnityEngine;
using System.Collections;

public class SignRenderScript : MonoBehaviour {

	public Transform[] signs;

	void Awake () {
		// Debug.Log(AssetDatabase.GetAssetPath(gameObject));

		Transform sign;

		float weightVal = gameObject.GetComponent<BreakableScript>().getWeight();
		// float width = collider2D.bounds.extents.x * 2;
		float topBound = transform.position.y + collider2D.bounds.extents.y;

		sign = signs[((int) weightVal) - 1];

		Vector3 position = new Vector3(transform.position.x - collider2D.bounds.extents.x + 0.5F, topBound + 0.5F, transform.position.z);

		Transform left;

		left = Instantiate(sign, position, Quaternion.identity) as Transform;

		// Scale in the y dimension, but not x
		left.localScale = new Vector3(1.0F, 1.0F, 1.0F);

		left.parent = transform;
	}

	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void Update () {}
}
