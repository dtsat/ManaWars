using UnityEngine;
using System.Collections;

public class FairyBullet : MonoBehaviour {

	public bool targetAssigned = false;

	public GameObject targetNode;

	public float lifetime = 4f;

	float distFromTarget;
	float speed = 7.0f;
	float radius = 0.11f;
	Vector3 goalFacing;
	Quaternion lookWhereYoureGoing;
	float rotationSpeedRads = 1.0f;

	public GameObject target;

	// Use this for initialization
	void Start () {
	}

	public void SetTarget(GameObject t){
		target = t;

		targetNode.transform.position = target.transform.position;

	}
	
	// Update is called once per frame
	void Update () {

		distFromTarget = (targetNode.transform.position - transform.position).magnitude;

		lifetime -= (Time.deltaTime) * 1;

		if (lifetime <= 0) {
			Destroy (gameObject);
		}


		if (targetAssigned) {
			//float dot = Vector3.Dot (transform.forward, (targetNode.transform.position - transform.position).normalized);
			GetComponent<Rigidbody> ().velocity = ((targetNode.transform.position - transform.position).normalized * speed);
			goalFacing = (targetNode.transform.position - transform.position).normalized;
			lookWhereYoureGoing = Quaternion.LookRotation (goalFacing, Vector3.up);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, lookWhereYoureGoing, rotationSpeedRads);
		}

		if (distFromTarget <= radius) {
			//Destroy (targetNode);
			Destroy (gameObject);
		}

	}

	public void OnTriggerEnter(Collider other){
		if (other.tag == "Enemy") {

			Destroy (other.gameObject);
			//Destroy (targetNode);
			Destroy (gameObject);
		}
	}
}
