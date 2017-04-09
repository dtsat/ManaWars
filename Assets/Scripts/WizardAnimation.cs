using UnityEngine; 
using System.Collections;

public class WizardAnimation : MonoBehaviour {
    Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if(gameObject.GetComponent<Human_Wizard>().health > 0)
        {
            Movement();
            Jump();
            Fire();
            Victory();
        }
    }

    void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    void Jump()
    {
        bool jump = Input.GetButtonDown("Jump");
        animator.SetBool("Jump", jump);
    }

    void Fire()
    {
        bool fire = Input.GetMouseButtonDown(0);
        animator.SetBool("Fire", fire);
    }

    void Victory()
    {
        bool victory = Input.GetButtonDown("Submit");
        animator.SetBool("Victory", victory);
    }
}
