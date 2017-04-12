using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

	Vector3 mouseLook = new Vector3(0.0f, 0.0f, 10f);
	Vector3 smooth;
	private float sensitivity = 1f, smoothing = 2f;

	public GameObject wizard;

	void Start () 
	{
		wizard = this.transform.parent.gameObject;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;
	}
	

	void FixedUpdate () 
	{
		Vector3 temp = Input.mousePosition;
		temp.x = Screen.width / 2;
		temp.z = 10f;
		transform.position = Camera.main.ScreenToWorldPoint (temp);
	}

	void Update()
	{
		Vector2 deltaMouse = new Vector3 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));
		deltaMouse = Vector2.Scale (deltaMouse, new Vector2 (sensitivity * smoothing, sensitivity * smoothing));
		smooth.x = Mathf.Lerp (smooth.x, deltaMouse.x, 1f / smoothing);
		smooth.y = Mathf.Lerp (smooth.y, deltaMouse.y, 1f / smoothing);
		mouseLook += smooth;
		mouseLook.y = Mathf.Clamp (mouseLook.y, -1.5f, 90f);

		//Camera.main.transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
		transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);

		wizard.transform.rotation = Quaternion.AngleAxis (mouseLook.x, wizard.transform.up);

		if(Input.GetKeyDown(KeyCode.Escape))
			Cursor.lockState = CursorLockMode.None;
	}
}
