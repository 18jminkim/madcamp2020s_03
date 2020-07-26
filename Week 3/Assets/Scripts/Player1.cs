using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public Transform groundCheck;
    public float speed;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("Idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        //wasd로만 움직이게 해놨음
        if(Input.GetKey(KeyCode.D)) 
        {
			transform.Translate (Vector3.right * speed);
            // animator.SetBool("RightTurn", true);
		} 
        // else
        // {
        //     animator.SetBool("RightTurn", false);
        // }

		if(Input.GetKey(KeyCode.A))
        {
			transform.Translate (-Vector3.right * speed);
            // animator.SetBool("LeftTurn", true);
		}
        // else
        // {
        //     animator.SetBool("LeftTurn", false);
        // }

		if(Input.GetKey(KeyCode.W))
        {
			transform.Translate (Vector3.forward * speed);
            // animator.SetBool("Walking", true);
		} 
        // else
        // {
        //     animator.SetBool("Walking", false);
        // }

		if(Input.GetKey(KeyCode.S))
        {
			transform.Translate (-Vector3.forward * speed);
		}
        
        if(Input.GetKey(KeyCode.G))
        {
            animator.SetBool("Punch", true);
		}
        else
        {
            animator.SetBool("Punch", false);
        }
        
    }
}
