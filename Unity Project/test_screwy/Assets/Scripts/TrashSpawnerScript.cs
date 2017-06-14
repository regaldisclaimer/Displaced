using UnityEngine;
using System.Collections;

public class TrashSpawnerScript : MonoBehaviour {

	public GameObject toInstantiate;
	public float interval;

	private float timeSinceSpawn = 0;

	// Use this for initialization
	void Start () {

	}

	void FixedUpdate () {
		timeSinceSpawn += Time.deltaTime;
		if (timeSinceSpawn >= interval) {
			timeSinceSpawn = 0;
			Instantiate(toInstantiate, transform.position, Quaternion.identity);
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
