using UnityEngine;
using System.Collections;

public class SimpleMove : MonoBehaviour {

	public float timer = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (timer >= 0.1) {
			timer = 0f;

			float newX = transform.position.z + 0.1f;

			transform.position = new Vector3(transform.position.x, transform.position.y, newX);

		} else {
			timer += (Time.deltaTime) * 1;
		}
	}
}
