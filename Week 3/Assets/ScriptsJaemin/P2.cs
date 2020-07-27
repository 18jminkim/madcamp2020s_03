using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2 : MonoBehaviour
{

    public float power = 100f;
    public Transform opponent;
    public Transform me;
    public bool isDead; // for debugging only.
    public CharacterController characterController;

    public Rigidbody rb;

    public float speed = 1f;
    public Animator animator;
    public bool run;
    public bool moveable = true;
    public Collider rightFist;
    public bool punch;




    // Start is called before the first frame update
    void Start()
    {
       
        setRigidbodyState(true);
        setColliderState(false);
        setRightPunch(false);
    }

    // Update is called once per frame
    void Update()
    {

        
        if (isDead)
        {
            die();
        }
        else
        {
            revive();
        }



        // move if alive.
        if (!isDead && moveable)
        {
            move();
        }
        
    }

    public void die()
    {
        GetComponent<Animator>().enabled = false;
        setRigidbodyState(false);
        setColliderState(true);
        characterController.enabled = false;
        isDead = true;
        setRightPunch(false);

    }

    public void revive()
    {

        GetComponent<Animator>().enabled = true;
        setRigidbodyState(true);
        setColliderState(false);
        characterController.enabled = true;
        isDead = false;
        setRightPunch(false);



    }

    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "P1")
        {
            setRightPunch(false);
            //rb.isKinematic = false;
            Debug.Log("Punch received");

            die();

            Vector3 direction = me.position - opponent.position;
            rb.AddForce(direction.normalized * power, ForceMode.Impulse);
            Debug.Log("punch");
            

            Invoke("revive", 2f);

        }
    }


    void setRigidbodyState(bool state)
    { 
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            GetComponent<Rigidbody>().isKinematic = !state;
        }


    }


    void setColliderState(bool state)
    { 
        Collider[] colliders = GetComponentsInChildren<Collider>();
 
        foreach(Collider collider in colliders)
        {
            collider.enabled = state;
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            GetComponent<Rigidbody>().isKinematic = !state;
        }


        rightFist.enabled = true;
    }


    private void setRightPunch(bool state)
    {
        punch = state;
        rightFist.enabled = state;
        animator.SetBool("Punch", state);
    }
    // receives keyboard input to move the character.
    void move ()
    {

        if (Input.GetKey(KeyCode.Slash))
        {
            setRightPunch(true);
            return;
        }
        else
        {
            setRightPunch(false);
        }
        run = false;
        //wasd로만 움직이게 해놨음
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.Translate(Vector3.right * speed);
            characterController.Move(Vector3.right * speed * Time.deltaTime);
            run = true;
            // animator.SetBool("RightTurn", true);
        }

        // else
        // {
        //     animator.SetBool("RightTurn", false);
        // }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.Translate(-Vector3.right * speed);
            characterController.Move(-Vector3.right * speed * Time.deltaTime);
            run = true;

            // animator.SetBool("LeftTurn", true);
        }
        // else
        // {
        //     animator.SetBool("LeftTurn", false);
        // }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //transform.Translate(Vector3.forward * speed);
            characterController.Move(Vector3.forward * speed * Time.deltaTime);
            run = true;
            // animator.SetBool("Walking", true);
        }
        // else
        // {
        //     animator.SetBool("Walking", false);
        // }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //transform.Translate(-Vector3.forward * speed);
            characterController.Move(-Vector3.forward * speed * Time.deltaTime);
            run = true;
        }

        animator.SetBool("Run", run);


    }





}
