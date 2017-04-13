using UnityEngine;
using System.Collections;

public class FairyShield : MonoBehaviour {

	public float lifetime = 5f;

	public AudioSource ReflectSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		lifetime -= (Time.deltaTime) * 1;

		Collider[] hitColliders = Physics.OverlapSphere (transform.position, 2.5f);
		int i = 0;

		while (i < hitColliders.Length) {
			if (hitColliders [i].tag == "EnemySpell") {
				ReflectSound.Play ();
				Destroy (hitColliders [i].gameObject);
				//Destroy (gameObject);

			}
			i++;
		}

		if (lifetime <= 0) {
			Destroy (gameObject);
		}


	}

	public void onTriggerEnter(Collider other){
		if (other.tag == "EnemySpell") {
			Destroy (other.gameObject);
		}
	}
}
