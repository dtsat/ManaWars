using UnityEngine;
using System.Collections;

public class VictoryWizard : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetTrigger("Victory");
	}
}
