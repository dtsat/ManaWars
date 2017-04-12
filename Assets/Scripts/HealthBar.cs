using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Image playerHealth;
    Human_Wizard player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Human_Wizard>();
    }

    // Update is called once per frame
    void Update () {
	
	}
    public void updateHealthBar()
    {
        playerHealth.fillAmount = player.health / 100f;  //fill is between 0 and 1, take player health to get a float value for fill bar
        if(player.health <= 0)  //just because the player health will show -10 before game over screen
        {
            GameObject.Find("HPText").GetComponent<Text>().text = "HP: 0/100";
        }
        else
        {
            GameObject.Find("HPText").GetComponent<Text>().text = "HP: " + player.health + "/100";
        }
    }
}
