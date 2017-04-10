using UnityEngine;
using System.Collections;

public class FireSpell : Spell {

    public GameObject fireBall;
	public Transform wand;
	private GameObject castedSpell;

    public override void fire()
    {
		castedSpell = Instantiate (fireBall, wand.position, wand.rotation) as GameObject;
    }

    /*void OnMouseDown()
    {
        fire();
    }*/
    
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
