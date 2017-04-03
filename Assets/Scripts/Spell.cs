using UnityEngine;
using System.Collections;

public abstract class Spell : MonoBehaviour
{
    public int damage;

    public float speed;
    public float travelDistance;

    public abstract void fire();



}
