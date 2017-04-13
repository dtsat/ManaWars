using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {
    Text textBox;
    Human_Wizard player;
    public int score = 0;
    public int livingMobs = 0;
    GameObject[] LeaderMobs;
    GameObject[] RangedMobs;
    GameObject[] MeleeMobs;

    // Use this for initialization
    void Start () {
        textBox = GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Human_Wizard>();
        LeaderMobs = GameObject.FindGameObjectsWithTag("MobLeader");
        RangedMobs = GameObject.FindGameObjectsWithTag("MobRanged");
        MeleeMobs = GameObject.FindGameObjectsWithTag("MobMelee");
        livingMobs = LeaderMobs.Length + RangedMobs.Length + MeleeMobs.Length;
        textBox.text = "Score - " + score + "\nHealth - " + player.health + "\nNumber of Living Mobs - " + livingMobs;
    }
	
    public void IncreaseMobCount()
    {
        livingMobs++;
        textBox.text = "Score - " + score + "\nHealth - " + player.health + "\nNumber of Living Mobs - " + livingMobs;
    }

	public void UpdateLeaderDeath()
    {
        score += 100;
        livingMobs--;
        textBox.text = "Score - " + score + "\nHealth - " + player.health + "\nNumber of Living Mobs - " + livingMobs;
    }

    public void UpdateRangedDeath()
    {
        score += 20;
        livingMobs--;
        textBox.text = "Score - " + score + "\nHealth - " + player.health + "\nNumber of Living Mobs - " + livingMobs;
    }

    public void UpdateMeleeDeath()
    {
        score += 5;
        livingMobs--;
        textBox.text = "Score - " + score + "\nHealth - " + player.health + "\nNumber of Living Mobs - " + livingMobs;
    }

    public void UpdateHealth()
    {
        textBox.text = "Score - " + score + "\nHealth - " + player.health + "\nNumber of Living Mobs - " + livingMobs;
    }

    void Update()
    {
        if (score >= 100)
        {
            SceneManager.LoadSceneAsync(4);
        }
    }
}
