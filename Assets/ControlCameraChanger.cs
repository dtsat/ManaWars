using UnityEngine;
using System.Collections;

public class ControlCameraChanger : MonoBehaviour {

	public GameObject IntroCam;
	public GameObject SmashCam;
	public GameObject ControlCam;

	// Use this for initialization
	void Start () {
		IntroCam = GameObject.Find ("IntroCam");
		SmashCam = GameObject.Find ("SmashCamera");
		ControlCam = GameObject.Find ("Controls Camera");
	}

	public void ToSmash()
	{
		Debug.Log ("WTF!");
		SmashCam.GetComponent<Camera> ().enabled = true;
		IntroCam.GetComponent<Camera> ().enabled = false;
	}

	public void ToControls()
	{
		ControlCam.GetComponent<Camera> ().enabled = true;
		SmashCam.GetComponent<Camera> ().enabled = false;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
