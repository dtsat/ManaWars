using UnityEngine;
using System.Collections;

public class CompanionFollow : MonoBehaviour {

	public GameObject target;
	public GameObject targetLeft;
	public GameObject targetRight;
	//public GameObject finalTarget;
	//public GameObject current;

	public float distFromTarget;
	public int targetNum = 1;

	//public int iteration = 0;
	float speed = 3.0f;
	float radius = 1.0f;
	Vector3 goalFacing;
	Quaternion lookWhereYoureGoing;
	float rotationSpeedRads = 10.0f;

	RaycastHit hit;


	public bool chasing = true;

	//public List<GameObject> DriveList;
	//public List<GameObject> ClosedList;

	// Use this for initialization
	void Start () {

		//Get car's nearest node, set this to start.
		//Make this all in the iterator and make iterator have the car as an object?  ...Yeah.

	}


	// Update is called once per frame
	void Update () {





		distFromTarget = (target.transform.position - transform.position).magnitude;

		if (distFromTarget <= radius) {
			chasing = false;
			GetComponent<Rigidbody> ().velocity = transform.forward.normalized * 0;
			goalFacing = (target.transform.forward).normalized;
			lookWhereYoureGoing = Quaternion.LookRotation (goalFacing, Vector3.up);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, lookWhereYoureGoing, rotationSpeedRads);
		} else {
			chasing = true;

		}

		if (chasing == true) {

			if (Physics.SphereCast (transform.position, 2.0f, (target.transform.position - transform.position).normalized, out hit)) {
				target = targetLeft;
			} else {
				target = targetRight;
			}

			float dot = Vector3.Dot (transform.forward, (target.transform.position - transform.position).normalized);
			GetComponent<Rigidbody> ().velocity = ((target.transform.position - transform.position).normalized * speed);
			goalFacing = (target.transform.position - transform.position).normalized;
			lookWhereYoureGoing = Quaternion.LookRotation (goalFacing, Vector3.up);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, lookWhereYoureGoing, rotationSpeedRads);


		}

	}

		//have on mouse over and click, then get the node for that tile set that at goal
		//check for nearest node to car set that at start.
		//give all nodes an ID.  If the ID that the pathfindings script is looking at is the ID of the thing that's clicked
		//then you are at Goal.  (Iterating through game object's list of children google it)

		//Start from start node, call node, to sort through all conns




	//if (current != finalTarget) {
	//int len = DriveList.Count;

	/*
			if (iteration != DriveList.Count) {

				if (distFromTarget <= radius) {
					current = DriveList [iteration];
					iteration++;


					if (target != finalTarget) {
						target = DriveList [iteration];
					}

				} else {

					float dot = Vector3.Dot (transform.forward, (target.transform.position - transform.position).normalized);

					//GetComponent<Rigidbody> ().velocity = transform.forward.normalized * speed;

					if (dot >= 1f) {
						GetComponent<Rigidbody> ().velocity = ((target.transform.position - transform.position).normalized * speed);
						//GetComponent<Rigidbody> ().velocity = transform.forward.normalized * speed;
						//GetComponent<Rigidbody> ().AddForce( (target.transform.position - transform.position).normalized * speed);
					} else {
						goalFacing = (target.transform.position - transform.position).normalized;
						lookWhereYoureGoing = Quaternion.LookRotation (goalFacing, Vector3.up);
						transform.rotation = Quaternion.RotateTowards (transform.rotation, lookWhereYoureGoing, rotationSpeedRads);
					}
				}

			} else {
				GetComponent<Rigidbody> ().velocity = transform.forward.normalized * 0;
				driving = false;
			}*/
	

}
