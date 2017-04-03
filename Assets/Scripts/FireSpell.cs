using UnityEngine;
using System.Collections;

public class FireSpell : Spell {

    public GameObject fireBall;
    GameObject castedSpell;

    public override void fire()
    {
        castedSpell = (GameObject)Instantiate(fireBall, transform.position + (transform.forward * 2)+(transform.up * 1), transform.rotation);
    }

    void OnMouseDown()
    {
        fire();
    }
    
    public int getDamage()
    {
        return damage;
    }
    public float getSpeed()
    {
        return speed;
    }

    public float getDistance()
    {
        return travelDistance;
    }

   
}
