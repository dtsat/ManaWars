using UnityEngine;
using System.Collections;

public abstract class AbstractCharacter : MonoBehaviour {

	public int health;
	public float speed;
	public Spell leftSpell, rightSpell;

	void fireRight ()
	{
		rightSpell.fire ();
	}

	void fireLeft()
	{
		leftSpell.fire ();
	}


}
