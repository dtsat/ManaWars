using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monster : MonoBehaviour {
    public GameObject player;
    NavMeshAgent agent;
    GameObject[] openSlots;
    int slotCount = 0;
    List<GameObject> leaders;
    Vector3[] slotPositions;
    bool inGroup;
    GameObject closestLeader;
    Monster leaderScript;
    Animator animator;
    GameObject score;

	bool isDead = false;

	//AI variables
	public float distToPlayer;
	public GameObject monsterBullet;
	public float ShootTimer = 0f;
	public float ShootRate = 6f;
	public float BonusShootRate = 4f;
    int health;
	public float MeleeTimer = 2f;
	public bool rangedSupport = false;
	public bool meleeSupport = false;


	public GameObject MeleeStrike;

	public Vector3 offsetShot;
	//public float formBonus = 2f //Make enemies shoot faster whie in formation.

	//SFX
	public AudioSource RawrSound;
	public AudioSource SmashSound;
	public AudioSource HurtSound;
	public AudioSource ShootSound;
	public AudioSource DeadSound;


    // Use this for initialization
    void Start () {
		
        agent = GetComponent<NavMeshAgent>();
        openSlots = new GameObject[4];
        animator = gameObject.GetComponent<Animator>();
        health = 100;
        score = GameObject.FindGameObjectWithTag("Score");

        if (tag == "MobLeader")
        {
			ShootRate = 3f;
            slotCount = 4;
            slotPositions = new Vector3[4];
            slotPositions[0] = new Vector3(0, 0, 6);
            slotPositions[1] = new Vector3(0, 0, -6);
            slotPositions[2] = new Vector3(6, 0, 0);
            slotPositions[3] = new Vector3(-6, 0, 0);
			leaderScript = this;
        }
	}

	IEnumerator ShockWave(){

		agent.Stop ();

		RawrSound.Play ();
		animator.SetTrigger("Shockwave Attack");

		yield return new WaitForSeconds (0.75f);

		MeleeStrike.transform.position = transform.position;
		MeleeStrike.transform.rotation = transform.rotation;

		SmashSound.Play ();
		Instantiate (MeleeStrike);

		yield return new WaitForSeconds (1f);

		agent.Resume ();

	}

    // Update is called once per frame
    void Update() {
		if (agent.velocity.magnitude > 0)
			animator.SetBool ("Walk", true);
		else
			animator.SetBool ("Walk", false);


		if(health > 0)
        {
            if (tag == "MobLeader")
            {
				if (meleeSupport) {
					//Debug.Log ("BOOSTAGE!");
					agent.speed = 4.5f;
				} else {
					agent.speed = 3.5f;
				}
				rangedSupport = false;
				meleeSupport = false;

				for (int i = 0; i < leaderScript.openSlots.Length; i++)
				{
					if (leaderScript.openSlots [i] != null) {
						if (leaderScript.openSlots [i].tag == "MobRanged") {
							rangedSupport = true;
						}
						if (leaderScript.openSlots [i].tag == "MobMelee") {
							///Debug.Log("I HAVE MELEE!!");
							meleeSupport = true;
							agent.speed = 4.5f;
						}
					}
				}


				if (MeleeTimer >= 0) {
					MeleeTimer -= (Time.deltaTime) * 1;
				}

				ShootTimer += (Time.deltaTime) * 1;
				distToPlayer = (player.transform.position - transform.position).magnitude;
				if (distToPlayer <= 6f) {
					//Debug.Log ("BOSS SEE  YOU!");

					if (MeleeTimer <= 0) {
						//Debug.Log ("BOSS SMASH!!!");
						StartCoroutine (ShockWave ());
						MeleeTimer = 2f;
					}
				} else if (distToPlayer <= 30f && rangedSupport) {
					//Debug.Log ("BOSS BLASTER!!!");

					transform.LookAt (player.transform);


					if (ShootTimer >= ShootRate)
					{
						//transform.LookAt (player.transform);
						ShootTimer = 0f;

						offsetShot = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
						monsterBullet.transform.position = offsetShot;

						ShootSound.Play ();
						animator.SetTrigger("Projectile Attack");

						Instantiate(monsterBullet);
						monsterBullet.GetComponent<MonsterBullet>().SetTarget(player);

						//instantiate enemy bullet
						//Enemy bullet script will autograb the player's location and home in on it.
					}
				}

                if (slotCount == 0)
                {

                    float distance = MinionDistance();
                    if (distance < 200.0f)
                    {


                        agent.SetDestination(player.transform.position);
                    }
						
                }
                else
                {
                    //Wander
                }
            }
            else if (tag == "MobRanged")
            {
				ShootTimer += (Time.deltaTime) * 1;
                //If in range, fire bullets
                distToPlayer = (player.transform.position - transform.position).magnitude;

                if (!inGroup)
                {
					if (distToPlayer <= 20)
					{

						//transform.LookAt (player.transform);

						if (ShootTimer >= ShootRate)
						{
							transform.LookAt (player.transform);
							ShootTimer = 0f;

							offsetShot = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
							monsterBullet.transform.position = offsetShot;

							//RawrSound.Play ();
							animator.SetTrigger("Projectile Attack");

							Instantiate(monsterBullet);
							monsterBullet.GetComponent<MonsterBullet>().SetTarget(player);

							ShootSound.Play ();
							//instantiate enemy bullet
							//Enemy bullet script will autograb the player's location and home in on it.
						}

					}

                    FindGroup();
                }
                else
                {

					if (distToPlayer <= 30)
					{
						
						transform.LookAt (player.transform);

						if (ShootTimer >= BonusShootRate)
						{
							//transform.LookAt (player.transform);
							ShootTimer = 0f;

							//Debug.Log ("I'm shooting now");

							offsetShot = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
							monsterBullet.transform.position = offsetShot;

							//RawrSound.Play ();
							animator.SetTrigger("Projectile Attack");

							Instantiate(monsterBullet);
							monsterBullet.GetComponent<MonsterBullet>().SetTarget(player);

							ShootSound.Play ();
							//instantiate enemy bullet
							//Enemy bullet script will autograb the player's location and home in on it.
						}

					}

                    agent.SetDestination(leaderScript.slotPositions[slotCount] + closestLeader.gameObject.transform.position);
                }
            }
            else if (tag == "MobMelee")
            {

				if (MeleeTimer >= 0) {
					MeleeTimer -= (Time.deltaTime) * 1;
				}


				distToPlayer = (player.transform.position - transform.position).magnitude;



				//else the group code

				if (!inGroup)
				{
					
					distToPlayer = (player.transform.position - transform.position).magnitude;

					if (distToPlayer <= 20) {
						//Debug.Log ("I SEE YOU, PLAYER!");
						agent.SetDestination(player.transform.position);
						//chase player
					}

					FindGroup();
				}
				else
				{
					agent.SetDestination(leaderScript.slotPositions[slotCount] + closestLeader.gameObject.transform.position);

					float distFromLeader = (player.transform.position - closestLeader.gameObject.transform.position).magnitude;

					if (distFromLeader <= 20) {
						//Debug.Log ("RAWR LEADER ASK ME TO CHASE YOU!");

						agent.SetDestination(player.transform.position);
						//chase player
					}
				}

				if (distToPlayer <= 3f) {
					//Debug.Log ("I ATTACKED YOU!");



					if (MeleeTimer <= 0) {

						StartCoroutine (ShockWave ());
						MeleeTimer = 2f;
					}
				}
            }
        }
    }

    void FindGroup()
    {
        leaders = new List<GameObject>(GameObject.FindGameObjectsWithTag("MobLeader"));
        do
        {
            closestLeader = FindClosestLeader();
            leaderScript = closestLeader.GetComponent<Monster>();
            leaders.Remove(closestLeader);
        } while (leaderScript.slotCount == 0 && leaders.Count > 0);

        if (leaderScript.slotCount > 0)
        {
            for (int i = 0; i < leaderScript.openSlots.Length; i++)
            {
                if (leaderScript.openSlots[i] == null)
                {
                    leaderScript.openSlots[i] = gameObject;
                    leaderScript.slotCount--;
                    slotCount = i;
                    break;
                }
            }
            inGroup = true;
        }
        else
        {
            //Wander
        }
    }

    float MinionDistance()
    {
        float distance = 0;
        Vector3 position = transform.position;
        foreach (GameObject go in openSlots)
        {
            Vector3 diff = go.transform.position - position;
            distance += diff.sqrMagnitude;
        }

        return distance;
    }

    GameObject FindClosestLeader()
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in leaders)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public void OnTriggerEnter(Collider other)
    {
		if (other.tag == "PlayerSpellFire" || other.tag == "PlayerSpellIce" || other.tag == "FireTrap" || other.tag == "FairySpell")
        {
			if (isDead) {
				if (other.tag == "PlayerSpellFire" || other.tag == "PlayerSpellIce")
					Destroy (other.gameObject);
				return;
			}

			if (other.tag == "PlayerSpellFire") {
				health -= 30;
				other.GetComponent<FireBallMovement> ().explode ();
			}
			if (other.tag == "PlayerSpellIce") {
				health -= 20;
				other.GetComponent<FireBallMovement> ().explode ();
			}
			if (other.tag == "FireTrap") {
				health -= 10;
			}
			if (other.tag == "FairySpell") {
				health -= 10;
			}

            if (health > 0)
            {
				HurtSound.Play ();
                gameObject.GetComponent<Animator>().SetTrigger("Take Damage");
            }
			else
            {
				DeadSound.Play ();
                gameObject.GetComponent<Animator>().SetTrigger("Die");
				isDead = true;
                if(tag == "MobLeader")
                {
                    score.GetComponent<Score>().UpdateLeaderDeath();
                }
                else if (tag == "MobRanged")
                {
                    score.GetComponent<Score>().UpdateRangedDeath();
                }
                else if (tag == "MobMelee")
                {
                    score.GetComponent<Score>().UpdateMeleeDeath();
                }
            }
        }
    }
}
