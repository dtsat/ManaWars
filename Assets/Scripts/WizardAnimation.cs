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
        Death();
        Damaged();
        Movement();
        Jump();
        Fire();
        Victory();
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

    void Death()
    {
        bool death = Input.GetMouseButtonDown(1);
        animator.SetBool("Death", death);
    }

    void Damaged()
    {
        bool damaged = Input.GetMouseButtonDown(2);
        animator.SetBool("Damaged", damaged);
    }

    void Victory()
    {
        bool victory = Input.GetButtonDown("Submit");
        animator.SetBool("Victory", victory);
    }
}
