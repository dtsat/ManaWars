using UnityEngine;
using System.Collections;

public class FairyBullet : MonoBehaviour {

	public bool targetAssigned = false;
	public float lifetime = 4f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		lifetime -= (Time.deltaTime) * 1;

		if (lifetime <= 0) {
			Destroy (gameObject);
		}


		if (targetAssigned) {
			
		}

	}
}
