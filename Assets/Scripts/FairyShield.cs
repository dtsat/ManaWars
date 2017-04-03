using UnityEngine;
using System.Collections;

public class FairyShield : MonoBehaviour {

	public float lifetime = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		lifetime -= (Time.deltaTime) * 2;

		if (lifetime <= 0) {
			GameObject.Destroy (gameObject);
		}
	}
}
