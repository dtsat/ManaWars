using UnityEngine;
using System.Collections;

public class FairyMagic : MonoBehaviour {

	public GameObject Wizard;
	public Vector3 wizPosition;
	public Quaternion wizRotation;
	float distFromWiz;
	public GameObject Shield;
	public Vector3 offsetShield;

	public float fairyshieldtimer = 6f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		fairyshieldtimer -= (Time.deltaTime) * 1;

		if (fairyshieldtimer <= 0) {
			wizPosition = new Vector3 (Wizard.transform.position.x, (Wizard.transform.position.y + 1), (Wizard.transform.position.z));
			wizRotation = Quaternion.LookRotation (Wizard.transform.position, Vector3.up);
			Shield.transform.position = wizPosition;
			offsetShield = new Vector3(Shield.transform.position.x, Shield.transform.position.y + 0.5f, Shield.transform.position.z + 2);
			Shield.transform.rotation = Quaternion.RotateTowards (transform.rotation, wizRotation, 999999f);
			Shield.transform.position = offsetShield;
			Instantiate (Shield);
			fairyshieldtimer = 6;
		}

	}
}
