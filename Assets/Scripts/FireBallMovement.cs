using UnityEngine;
using System.Collections;

public class FireBallMovement : MonoBehaviour {


	public GameObject crosshair, explosion, groundExplosion;
	public float speed;

	private Transform target;
	private RaycastHit hit;
	private Rigidbody rb;
	LayerMask lm = ~(1 << 10);

	public AudioSource BoomSound;
	public AudioSource FwooshSound;

	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		crosshair = GameObject.FindGameObjectWithTag ("Crosshair");
		if (crosshair != null) 
		{
			target = crosshair.transform;
			Vector3 camPos = Camera.main.transform.position;
			if (Physics.Raycast (camPos, (target.position - camPos), out hit, 200f, lm)) 
			{
				SetPosition (hit.point);
			} 
			else
				rb.velocity = (target.position - transform.position).normalized * speed;
		}

	
	}
	
	void SetPosition(Vector3 targ)
	{
		rb.velocity = (targ - transform.position).normalized * speed;
		transform.LookAt (targ);
	}

	public void explode()
	{
		BoomSound.Play ();
		Instantiate (explosion, transform.position, transform.rotation);
		Destroy (gameObject);
	}

	public void groundExplode ()
	{
		FwooshSound.Play ();
		Instantiate (groundExplosion, transform.position, transform.rotation);
		Destroy (gameObject, 0.15f);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Ground")) 
		{
			groundExplode ();
			if(this.CompareTag("PlayerSpellFire"))
				other.GetComponent<Ground>().setFire();
		} else if (other.CompareTag ("Boundary") || other.CompareTag ("Tree"))
			explode ();
	}
}
