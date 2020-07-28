using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class P1Rb2 : MonoBehaviour
{

    public Rigidbody characterRb;
    public Collider characterCol;

    // movement
    public float speed = 1f;
    public bool movable;


    // rotation
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // jump
    public float jumpHeight = 3f;
    float distanceToTheGround;

    //animator and states
    public Animator animator;
    public bool run = false;
    public bool jump = false;
    public bool punch = false;
    public bool dead = false;





    // Start is called before the first frame update
    void Start()
    {
        characterRb = GetComponent<Rigidbody>();
        characterCol = GetComponent<Collider>();
        setRigidbodyState(true);
        setColliderState(false);
        distanceToTheGround = GetComponent<Collider>().bounds.extents.y;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        //jump
        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            characterRb.AddForce(new Vector3(0f, jumpHeight, 0f), ForceMode.Impulse);
            Debug.Log("Rb jump key detected.");
            jump = true;
            animator.SetBool("Jump", true);
        }

    }


    private void FixedUpdate()
    {
        if (movable)
        {
            move();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        jump = false;
        animator.SetBool("Jump", false);
    }



    // helper functions
    void move()
    {
        // punch.
        if (Input.GetKey(KeyCode.G))
        {
            Debug.Log("Punch key detected.");
            //setRightPunch(true);
            //Invoke("disableRightPunch", 10f);
            return;
        }
        else
        {
            //setRightPunch(false);
        }

        // translation
        float x = Input.GetAxis("Horizontal1");
        float z = Input.GetAxis("Vertical1");
        Vector3 direction = new Vector3(x, 0f, z).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);


        //rotation
        if (direction.normalized.magnitude > 0f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            run = true;
            animator.SetBool("Run", true);
        }
        else
        {
            run = false;
            animator.SetBool("Run", false);
        }



    }


    void setRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }
        characterRb.isKinematic = !state;


    }


    void setColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }
        characterCol.enabled = !state;

        
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanceToTheGround + 0.1f);
    }


    void rotate()
    {

    }

}
