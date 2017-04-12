using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Human_Wizard : AbstractCharacter {

	private Animator animator;
	private Rigidbody rb;
	private RaycastHit hit;
	private float nextFireFireBall, nextFireIce;

	public float fireFirerate, iceFirerate;
	public Camera mainCamera;
	public GameObject crosshair;
	public GameObject testbullet;

	public float speedboostTimer = 3f;
	public bool isSpeedBoosted = false;

	public AudioSource IceSound;
	public AudioSource FlameSound;
	public AudioSource HurtSound;
	public AudioSource DeadSound;

    GameObject score;

	void Start () 
	{
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();
        score = GameObject.FindGameObjectWithTag("Score");

		health = 100;
	}
	

	void Update () 
	{
        if(health > 0)
        {
            Move();
        }
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemySpell")
        {
            health -= 10;
            score.GetComponent<Score>().UpdateHealth();
            if (health > 0)
            {
				HurtSound.Play ();
                gameObject.GetComponent<Animator>().SetTrigger("Damaged");
            }
            else
            {
				DeadSound.Play ();
                PlayerPrefs.SetFloat("Score", score.GetComponent<Score>().score);
                PlayerPrefs.SetFloat("MobsRemaining", score.GetComponent<Score>().livingMobs);
                SceneManager.LoadScene(2);
                gameObject.GetComponent<Animator>().SetTrigger("Death");
            }
            Destroy(other.gameObject);
        }

		if (other.tag == "MeleeAttack")
		{
			other.GetComponentInChildren<Collider> ().enabled = false;
			health -= 10;
			score.GetComponent<Score>().UpdateHealth();
			if (health > 0)
			{
				gameObject.GetComponent<Animator>().SetTrigger("Damaged");
			}
			else
			{
                PlayerPrefs.SetFloat("Score", score.GetComponent<Score>().score);
                PlayerPrefs.SetFloat("MobsRemaining", score.GetComponent<Score>().livingMobs);
                SceneManager.LoadScene(2);
                gameObject.GetComponent<Animator>().SetTrigger("Death");
			}
			Destroy(other.gameObject);
		}
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.Q))
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.E))
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(new Vector3(0.0f, -5f, 0.0f));
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(new Vector3(0.0f, 5f, 0.0f));
		if (Input.GetMouseButtonDown(0) && Time.time > nextFireIce)
        {
			nextFireIce = Time.time + iceFirerate;
			IceSound.Play ();
			leftSpell.fire ();
        }
		if (Input.GetMouseButtonDown(1) && Time.time > nextFireFireBall)
		{
			nextFireFireBall = Time.time + fireFirerate;
			FlameSound.Play ();
			rightSpell.fire ();
		}


        if (isSpeedBoosted == true)
        {
            speedboostTimer -= (Time.deltaTime) * 1;

            if (speedboostTimer <= 0)
            {
                speed = 5;
                isSpeedBoosted = false;
                speedboostTimer = 3;
            }
        }
    }
}
