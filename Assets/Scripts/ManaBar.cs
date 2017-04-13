using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour {
    public Image manaBar;
    FairyMagic fairyCompanion;

    // Use this for initialization
    void Start()
    {
        fairyCompanion = GameObject.Find("Fairy").GetComponent<FairyMagic>();
    }

    // Update is called once per frame
    void Update()
    {
        updateManaBar();
    }
    public void updateManaBar()
    {
        manaBar.fillAmount = fairyCompanion.fairyshieldtimer / 10f;  //fill is between 0 and 1, take player health to get a float value for fill bar
        
    }
}
