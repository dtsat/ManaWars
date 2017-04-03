using UnityEngine;
using System.Collections;

public class Human_Wizard : AbstractCharacter {

	private Animator animator;
	private Rigidbody rb;
	private RaycastHit hit;


	public Camera mainCamera;
	public GameObject crosshair;
	public GameObject testbullet;


	void Start () 
	{
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();
	}
	

	void Update () 
	{
		if(Input.GetKey(KeyCode.W))
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
		if(Input.GetKey(KeyCode.S))
			transform.Translate(Vector3.back * speed * Time.deltaTime);
		if(Input.GetKey(KeyCode.Q))
			transform.Translate(Vector3.left * speed * Time.deltaTime);
		if(Input.GetKey(KeyCode.E))
			transform.Translate(Vector3.right * speed * Time.deltaTime);
		if (Input.GetKey (KeyCode.A))
			transform.Rotate(new Vector3(0.0f, -5f, 0.0f));
		if(Input.GetKey(KeyCode.D))
			transform.Rotate(new Vector3(0.0f, 5f, 0.0f));
		if (Input.GetMouseButtonDown (0)) 
		{
			if (Physics.Raycast (mainCamera.transform.position, crosshair.transform.position, out hit)) {
				Debug.DrawLine (mainCamera.transform.position, crosshair.transform.position, Color.cyan, 20f, false);
			}
		}
	
	}
}
