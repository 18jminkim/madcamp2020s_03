using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class P1Rb2 : MonoBehaviour
{
    public int redPoint;
    public int bluePoint;
    public ClearManager manager;
    public CamScript cam;
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
    public Collider jumpCollider;
    public float thresholdH = 1f;

    //animator and states
    public Animator animator;
    public bool run = false;
    public bool jump = false;
    public bool punch = false;
    public bool dead = false;

    // combat
    public Collider rightFist;
    public Transform opponent;
    public Transform me;
    public Rigidbody hitPoint;
    public float power = 100f;
    public float thresholdV = 10f;


    // Start is called before the first frame update
    void Start()
    {
        characterRb = GetComponent<Rigidbody>();
        characterCol = GetComponent<Collider>();
        setRigidbodyState(true);
        setColliderState(false);
        distanceToTheGround = jumpCollider.bounds.extents.y;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        //jump
        if ((!dead)&&  isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            characterRb.AddForce(new Vector3(0f, jumpHeight, 0f), ForceMode.Impulse);
            Debug.Log("Rb jump key detected.");
            jump = true;
            animator.SetBool("Jump", true);
        }
        if ((!dead) && isGrounded())
        {
            jumpCollider.enabled = true;
        }
        else
        {
            jumpCollider.enabled = false;

        }



    }


    private void FixedUpdate()
    {
        if ( (!dead) && movable)
        {
            move();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        jump = false;
        animator.SetBool("Jump", false);

        if (collision.gameObject.tag == "Obstacle" && collision.relativeVelocity.magnitude > thresholdV)
        {
            Debug.Log("P1 struck by an obstacle");
            // turn into ragdoll
            die();
            //characterRb.isKinematic = false;
            Vector3 direction = me.position - collision.transform.position;
            hitPoint.AddForce(direction.normalized * power, ForceMode.Impulse);

            bluePoint++;
            manager.setBluePoint(bluePoint);


            Invoke("revive", 2f);
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        

        if (collision.gameObject.tag == "P2")
        {
            Debug.Log("Received punch from " + collision.gameObject.name);
            // turn into ragdoll
            die();
            //characterRb.isKinematic = false;
            Vector3 direction = me.position - opponent.position;
            hitPoint.AddForce(direction.normalized * power, ForceMode.Impulse);

            bluePoint++;
            manager.setBluePoint(bluePoint);


            Invoke("revive", 2f);

        }

    }


  



    // helper functions
    void move()
    {
        // punch.
        if (Input.GetKey(KeyCode.G))
        {
            //Debug.Log("Punch key detected.");
            setRightPunch(true);
            return;
        }
        else
        {
            setRightPunch(false);
        }

        // translation
        float x = Input.GetAxisRaw("Horizontal1");
        float z = Input.GetAxisRaw("Vertical1");
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


    void setRightPunch(bool state)
    {
        punch = state;
        rightFist.enabled = state;
        animator.SetBool("Punch", state);
    }

    public void die()
    {
        animator.enabled = false;
        setRigidbodyState(false);
        setColliderState(true);
        setRightPunch(false);
        dead = true;
        cam.P1Dead();
    }


    public void revive()
    {
        Debug.Log("Revived");
        animator.enabled = true;
        setRigidbodyState(true);
        setColliderState(false);
        dead = false;
        setRightPunch(false);
        characterCol.transform.position = hitPoint.position;
        cam.P1Revive();

    }


}
