using UnityEngine;
using System.Collections;

public class AdditiveEndcapRenderScript : MonoBehaviour {

	public Transform endCap;

	void Awake () {
		float width = collider2D.bounds.extents.x * 2;
		if (width <= 1) {
			Instantiate(endCap, transform.position, Quaternion.identity);
		} else {
			Vector3 leftPosition = new Vector3(transform.position.x - collider2D.bounds.extents.x + 0.5F, transform.position.y, transform.position.z);
			Vector3 rightPosition = new Vector3(transform.position.x + collider2D.bounds.extents.x - 0.5F, transform.position.y, transform.position.z);

			Transform left;
			Transform right;

			left = Instantiate(endCap, leftPosition, Quaternion.identity) as Transform;
			right = Instantiate(endCap, rightPosition, Quaternion.identity) as Transform;

			// Scale in the y dimension, but not x
			left.localScale =  new Vector3(1.0F, transform.localScale.y, 1.0F);
			right.localScale =  new Vector3(1.0F, transform.lossyScale.y, 1.0F);

			left.parent = transform;
			right.parent = transform;
		}
	}

	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void Update () {}
}
