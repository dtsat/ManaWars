using UnityEngine;
using System.Collections;

public class MonsterSpawner : MonoBehaviour {

    public GameObject bossMonster;
    public GameObject rangedMinion;
    public GameObject meleeMinion;

    private Vector3 bossSpawnPos;
    private Vector3 minionSpawnPos;
    private Vector3[] fromSpawnDestinations;

    private bool gameOver = false;

    private float timeSinceGameStart;

    private int maxBosses;
    private int maxMinions;

    int bossesInScene;
    int minionsInScene;

    // Use this for initialization
    void Start () {
        bossSpawnPos = transform.Find("BossSpawner").transform.position;
        minionSpawnPos = transform.Find("MinionSpawner").transform.position;
        timeSinceGameStart = 0;
        fromSpawnDestinations = new Vector3[3];
        fromSpawnDestinations[0] = transform.Find("D1").position;
        fromSpawnDestinations[1] = transform.Find("D2").position;
        fromSpawnDestinations[2] = transform.Find("D3").position;
        Debug.Log("MonsterSpawner: " + fromSpawnDestinations[0] + " | " + fromSpawnDestinations[1] + " | " + fromSpawnDestinations[2]);

        spawnBoss();    // Initial enemies
        spawnMeleeMinion();
        spawnRangedMinion();
        bossesInScene = 1;
        minionsInScene = 2;
        StartCoroutine(EnemySpawning());
	}

    void Update()
    {
        if (timeSinceGameStart < 5000)
            timeSinceGameStart += Time.deltaTime;

        /* These values control what the max number of each enemy type in the scene is.
        Increases over time */

        maxBosses = 1 + ((int)timeSinceGameStart / 25); // Allows an additional boss every 15 seconds
        maxMinions = 2 + ((int)timeSinceGameStart / 10); // Allows an additional small minion every 8 seconds
    }

    // Runs every 10 seconds. Spawns in enemies if there is room for more according to 
    // maxMinions and maxBosses parameters.
    IEnumerator EnemySpawning()
    {
        while (!gameOver)
        {
            bossesInScene = GameObject.FindGameObjectsWithTag("MobLeader").Length;
            minionsInScene = GameObject.FindGameObjectsWithTag("MobRanged").Length
                + GameObject.FindGameObjectsWithTag("MobMelee").Length;

            Debug.Log("EnemySpawning: bossesInScene = " + bossesInScene + " | " + "maxBosses: " + maxBosses);
            Debug.Log("EnemySpawning: minionsInScene = " + minionsInScene + " | " + "maxMinions: " + maxMinions);
            if (bossesInScene < maxBosses)
            {
                Debug.Log("Spawning boss");
                spawnBoss();
            }


            if (minionsInScene < maxMinions)
            {
                Debug.Log("Spawning minions");
                float f = Random.Range(0, 2);
                if (f == 0)
                {
                    spawnMeleeMinion();
                }
                else
                {
                    spawnRangedMinion();
                }
            }

            yield return new WaitForSeconds(3f);
        }
    }

    private void spawnRangedMinion()
    {
        GameObject spawnedEnemy = Instantiate(rangedMinion, minionSpawnPos, Quaternion.identity) as GameObject;
        float f = Random.Range(0, 3);
        if (f < 0.33f)
            StartCoroutine(spawnedEnemy.GetComponent<Monster>().MoveFromSpawn(fromSpawnDestinations[0]));
        else if (f < 0.66f)
            StartCoroutine(spawnedEnemy.GetComponent<Monster>().MoveFromSpawn(fromSpawnDestinations[1]));
        else
            StartCoroutine(spawnedEnemy.GetComponent<Monster>().MoveFromSpawn(fromSpawnDestinations[2]));
    }

    private void spawnMeleeMinion()
    {
        GameObject spawnedEnemy = Instantiate(meleeMinion, minionSpawnPos, Quaternion.identity) as GameObject;
        float f = Random.Range(0, 3);
        if (f < 0.33f)
            StartCoroutine(spawnedEnemy.GetComponent<Monster>().MoveFromSpawn(fromSpawnDestinations[0]));
        else if (f < 0.66f)
            StartCoroutine(spawnedEnemy.GetComponent<Monster>().MoveFromSpawn(fromSpawnDestinations[1]));
        else
            StartCoroutine(spawnedEnemy.GetComponent<Monster>().MoveFromSpawn(fromSpawnDestinations[2]));
    }

    private void spawnBoss()
    {
        GameObject spawnedEnemy = Instantiate(bossMonster, bossSpawnPos, Quaternion.identity) as GameObject;
        int f = Random.Range(0, 3);
        Debug.Log("Boss f: " + f);
        if (f == 0)
            StartCoroutine(spawnedEnemy.GetComponent<Monster>().MoveFromSpawn(fromSpawnDestinations[0]));
        else if (f == 1)
            StartCoroutine(spawnedEnemy.GetComponent<Monster>().MoveFromSpawn(fromSpawnDestinations[1]));
        else
            StartCoroutine(spawnedEnemy.GetComponent<Monster>().MoveFromSpawn(fromSpawnDestinations[2]));
    }
}
