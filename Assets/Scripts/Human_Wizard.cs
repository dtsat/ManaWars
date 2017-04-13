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
    GameObject healthBar;

	private int screenWidth;

	void Start () 
	{
		screenWidth = Screen.width;
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();
        score = GameObject.FindGameObjectWithTag("Score");
        healthBar = GameObject.FindGameObjectWithTag("HealthContainer");
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
            health -= 5;
            score.GetComponent<Score>().UpdateHealth();
            healthBar.transform.GetChild(0).GetComponent<HealthBar>().updateHealthBar();
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
				SceneManager.LoadSceneAsync(2);
                gameObject.GetComponent<Animator>().SetTrigger("Death");
            }
            Destroy(other.gameObject);
        }

		if (other.tag == "MobMelee")
		{
			health -= 1;
			score.GetComponent<Score>().UpdateHealth();
			healthBar.transform.GetChild(0).GetComponent<HealthBar>().updateHealthBar();
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
				SceneManager.LoadSceneAsync(2);
				gameObject.GetComponent<Animator>().SetTrigger("Death");
			}
		}

		if (other.tag == "FireTrap")
		{
			health -= 10;
			score.GetComponent<Score>().UpdateHealth();
			healthBar.transform.GetChild(0).GetComponent<HealthBar>().updateHealthBar();
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
				SceneManager.LoadSceneAsync(2);
				gameObject.GetComponent<Animator>().SetTrigger("Death");
			}
		}

		if (other.tag == "MeleeAttack")
		{
			other.GetComponentInChildren<Collider> ().enabled = false;
			health -= 20;
			score.GetComponent<Score>().UpdateHealth();
            healthBar.transform.GetChild(0).GetComponent<HealthBar>().updateHealthBar();
            if (health > 0)
			{
				gameObject.GetComponent<Animator>().SetTrigger("Damaged");
			}
			else
			{
                PlayerPrefs.SetFloat("Score", score.GetComponent<Score>().score);
                PlayerPrefs.SetFloat("MobsRemaining", score.GetComponent<Score>().livingMobs);
				SceneManager.LoadSceneAsync(2);
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
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * speed * Time.deltaTime);
		if (Input.GetKey (KeyCode.Escape))
			SceneManager.LoadSceneAsync (0);
		/*if (Input.mousePosition.x > screenWidth / 2)
            transform.Rotate(new Vector3(0.0f, 5f, 0.0f));
		if (Input.mousePosition.x < screenWidth / 2)
            transform.Rotate(new Vector3(0.0f, -5f, 0.0f));
		if (Input.GetKey(KeyCode.E))
			transform.Rotate(new Vector3(0.0f, 5f, 0.0f));
		if (Input.GetKey(KeyCode.Q))
			transform.Rotate(new Vector3(0.0f, -5f, 0.0f));*/
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
