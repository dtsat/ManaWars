using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

	void Start () {
	
	}
	

	void FixedUpdate () 
	{
		Vector3 temp = Input.mousePosition;
		temp.z = 10f;
		transform.position = Camera.main.ScreenToWorldPoint (temp);
	}
}
