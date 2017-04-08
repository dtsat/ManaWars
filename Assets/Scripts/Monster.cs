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

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        openSlots = new GameObject[4];

        if(tag == "MobLeader")
        {
            slotCount = 4;
            slotPositions = new Vector3[4];
            slotPositions[0] = transform.position + new Vector3(0, 0, 10);
            slotPositions[1] = transform.position + new Vector3(0, 0, -10);
            slotPositions[2] = transform.position + new Vector3(10, 0, 0);
            slotPositions[3] = transform.position + new Vector3(-10, 0, 0);
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
                    slotPositions[0] = transform.position + new Vector3(0, 0, 5);
                    slotPositions[1] = transform.position + new Vector3(0, 0, -5);
                    slotPositions[2] = transform.position + new Vector3(5, 0, 0);
                    slotPositions[3] = transform.position + new Vector3(-5, 0, 0);
                }
            }
            else
            {
                //Wander
            }
        }
        else if (tag == "MobRanged")
        {
            if (!inGroup)
            {
                FindGroup();
            }
            else
            {
                agent.SetDestination(leaderScript.slotPositions[slotCount]);
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
                agent.SetDestination(leaderScript.slotPositions[slotCount]);
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
