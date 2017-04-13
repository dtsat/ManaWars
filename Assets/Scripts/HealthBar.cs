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
	    if(playerHealth.fillAmount != player.health/100f)
        {
            playerHealth.fillAmount = Mathf.Lerp(playerHealth.fillAmount, player.health / 100f, 5 * Time.deltaTime);  //health bar values are lerped to make the bar move gradually instead of instantly
        }
	}
    public void updateHealthBar()
    {
       // playerHealth.fillAmount = player.health / 100f;  //fill is between 0 and 1, take player health to get a float value for fill bar
        if(player.health <= 0)  //just because the player health will show -10 before game over screen or sometimes a little over 100
        {
            GameObject.Find("HPText").GetComponent<Text>().text = "HP: 0/100";
        }
        else if(player.health >=100)
        {
            GameObject.Find("HPText").GetComponent<Text>().text = "HP: 100/100";

        }
        else
        {
            GameObject.Find("HPText").GetComponent<Text>().text = "HP: " + player.health + "/100";
        }
    }
}
