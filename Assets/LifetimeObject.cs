using UnityEngine;
using System.Collections;

public class LifetimeObject : MonoBehaviour {

	public float lifetime = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		lifetime -= (Time.deltaTime) * 1;

		if (lifetime <= 0) {
			Destroy (gameObject);
		}
	
	}
}
