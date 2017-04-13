using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

	public GameObject flames; 
	public bool onFire = false;

	void Start () {
	
	}
	
	public void setFire()
	{
		if (!onFire) 
		{
			onFire = true;
			Instantiate (flames, this.transform.position, flames.transform.rotation);
			StartCoroutine (reset ());
		}
	}

	IEnumerator reset()
	{
		yield return new WaitForSeconds (10f);
		onFire = false;
	}
}
