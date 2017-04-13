using UnityEngine;
using System.Collections;

public class MonsterBullet : MonoBehaviour {

	public bool targetAssigned = false;
	public GameObject targetNode;
	public GameObject player;

	public float lifetime = 10f;

	float distFromTarget;
	float speed = 6.0f;
	float radius = 0.11f;
	Vector3 goalFacing;
	Quaternion lookWhereYoureGoing;
	float rotationSpeedRads = 1.0f;

	public GameObject target;
	Vector3 targetOffset;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("RedWizardHuman");
	}

	public void SetTarget(GameObject t){
		target = t;

		targetOffset = new Vector3 (target.transform.position.x, target.transform.position.y + 1f, target.transform.position.z);

		targetNode.transform.position = targetOffset;
		targetAssigned = true;

	}

	// Update is called once per frame
	void Update () {

		if (targetAssigned) {
			distFromTarget = (targetNode.transform.position - transform.position).magnitude;

			lifetime -= (Time.deltaTime) * 1;

			if (lifetime <= 0) {
				Destroy (gameObject);
			}



			//float dot = Vector3.Dot (transform.forward, (targetNode.transform.position - transform.position).normalized);
			GetComponent<Rigidbody> ().velocity = ((targetNode.transform.position - transform.position).normalized * speed);
			goalFacing = (targetNode.transform.position - transform.position).normalized;
			lookWhereYoureGoing = Quaternion.LookRotation (goalFacing, Vector3.up);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, lookWhereYoureGoing, rotationSpeedRads);


			if (distFromTarget <= radius) {
				//Destroy (targetNode);

				Destroy (gameObject);
			}
		}

	}

}
