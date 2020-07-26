using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1move : MonoBehaviour
{

    public CharacterController controller;
    //public BoxCollider attackCollider;
    
    public float speed = 12f;
    public float gravity = -20f;
    public float jumpHeight = 3f;

    //public Transform groundCheck;
    public float groundDistance = 0.2f;
    //public LayerMask groundMask;

    //public Animator animator;
    Vector3 velocity;
    //bool isGrounded;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // if (isGrounded && (velocity.y < -1))
        // {
        //     velocity.y = -2f;
        //     animator.SetInteger("jumping", 0);
        // }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(x, 0f, z).normalized;

        //Debug.Log(x.ToString() + " " + z.ToString());

        Vector3 move = transform.right * x + transform.forward * z;

        // if (move != new Vector3 (0f,0f,0f) && isGrounded  )
        // {
        //     //animator.SetInteger("moving", 1);
        //     animator.SetBool("idle", false);
        //     animator.SetBool("moving", true);

        // } else 
        // {
        //     //animator.SetInteger("moving", 0);
        //     animator.SetBool("idle", true);
        //     animator.SetBool("moving", false);

        // }


        //controller.Move(move * speed * Time.deltaTime);

        // if (Input.GetButtonDown("Jump") && isGrounded)
        // {
        //     Invoke("jump", 0f);
        //     //animator.SetInteger("jumping", 1);
        // }

        // if (Input.GetKey("j")) 
        // {
        //     //animator.SetBool("attack", true);
        //     Debug.Log("Attack button hit.");
        // } else {
        //     //animator.SetBool("attack", false);
        // }

        //velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        // if(direction.magnitude >= 0.1f)
        // {

        //     float targetAngle = Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg;
        //     float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);



        //     Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        //     transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //     controller.Move(moveDir.normalized * speed * Time.deltaTime);
        // }



    }


}
