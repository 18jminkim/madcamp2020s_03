using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{

    public float power = 100f;
    public Transform player1;
    public Transform player2;
    public bool isDead; // for debugging only.
    public CharacterController characterController;

    public Rigidbody rb;

    public float speed;
    public Animator animator;
    public bool run;




    // Start is called before the first frame update
    void Start()
    {
       
        setRigidbodyState(true);
        setColliderState(false);
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
        if (!isDead)
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


    }

    public void revive()
    {

        GetComponent<Animator>().enabled = true;
        setRigidbodyState(true);
        setColliderState(false);
        characterController.enabled = true;
        isDead = false;



    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Punch")
        {
            //rb.isKinematic = false;
            die();

            Vector3 direction = player2.position - player1.position;
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
    }


    // receives keyboard input to move the character.
    void move ()
    {
        run = false;
        //wasd로만 움직이게 해놨음
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.Translate(Vector3.right * speed);
            characterController.Move(Vector3.right * speed);
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
            characterController.Move(-Vector3.right * speed);
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
            characterController.Move(Vector3.forward * speed);
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
            characterController.Move(-Vector3.forward * speed);
            run = true;
        }

        animator.SetBool("Run", run);



        if (Input.GetKey(KeyCode.G))
        {
            animator.SetBool("Punch", true);
        }
        else
        {
            animator.SetBool("Punch", false);
        }

    }





}
