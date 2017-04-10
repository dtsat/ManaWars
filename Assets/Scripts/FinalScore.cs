using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour {
    Text textBox;

    // Use this for initialization
    void Start () {
        textBox = gameObject.GetComponent<Text>();
        textBox.text = "Final Score: " + PlayerPrefs.GetFloat("Score",0) + "\nLiving Mobs: " + PlayerPrefs.GetFloat("MobsRemaining",0);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
