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

	//AI variables
	float distToPlayer;
	public GameObject monsterBullet;
	public float ShootTimer = 0f;
	public float ShootRate = 4f;

	public Vector3 offsetShot;
	//public float formBonus = 2f //Make enemies shoot faster whie in formation.


    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        openSlots = new GameObject[4];
        animator = gameObject.GetComponent<Animator>();

        if (tag == "MobLeader")
        {
            slotCount = 4;
            slotPositions = new Vector3[4];
            slotPositions[0] = new Vector3(0, 0, 6);
            slotPositions[1] = new Vector3(0, 0, -6);
            slotPositions[2] = new Vector3(6, 0, 0);
            slotPositions[3] = new Vector3(-6, 0, 0);
        }
	}

    // Update is called once per frame
    void Update() {
        if (tag == "MobLeader")
        {
            if(slotCount == 0)
            {
                float distance = MinionDistance();
                if(distance < 200.0f)
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
			//If in range, fire bullets
			distToPlayer = (player.transform.position - transform.position).magnitude;

			if (distToPlayer <= 10) {
				ShootTimer += (Time.deltaTime) * 1;

				if (ShootTimer >= ShootRate) {
					ShootTimer = 0f;

					offsetShot = new Vector3 (transform.position.x, transform.position.y + 2f, transform.position.z);
					monsterBullet.transform.position = offsetShot;
					Instantiate (monsterBullet);
					monsterBullet.GetComponent<MonsterBullet> ().SetTarget (player);
                    animator.SetTrigger("Projectile Attack");
					//instantiate enemy bullet
					//Enemy bullet script will autograb the player's location and home in on it.
				}

			}

            if (!inGroup)
            {
                FindGroup();
            }
            else
            {

                agent.SetDestination(leaderScript.slotPositions[slotCount] + closestLeader.gameObject.transform.position);
            }
        }
        else if (tag == "MobMelee")
        {
            if (!inGroup)
            {
                FindGroup();
            }
            else
            {
                agent.SetDestination(leaderScript.slotPositions[slotCount] + closestLeader.gameObject.transform.position);
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
}
