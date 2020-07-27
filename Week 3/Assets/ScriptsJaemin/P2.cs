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

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Vector3 direction = new Vector3 (0f,0f,0f);



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
            //characterController.center = me.position + new Vector3(0f, 0.91f, 0f);
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
        me.SetPositionAndRotation(characterController.center, new Quaternion()) ;
        



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
        direction = new Vector3(0f, 0f, 0f);
        // if punching, don't run.
        if (Input.GetKey(KeyCode.Slash))
        {
            setRightPunch(true);
            return;
        }
        else
        {
            setRightPunch(false);
        }

        // not punching. Can run.
        run = false;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.Translate(Vector3.right * speed);
            characterController.Move(Vector3.right * speed * Time.deltaTime);
            run = true;
            direction.x += 1f;
        }



        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.Translate(-Vector3.right * speed);
            characterController.Move(-Vector3.right * speed * Time.deltaTime);
            run = true;
            direction.x -= 1f;


        }


        if (Input.GetKey(KeyCode.UpArrow))
        {
            //transform.Translate(Vector3.forward * speed);
            characterController.Move(Vector3.forward * speed * Time.deltaTime);
            run = true;
            direction.z += 1f;

        }


        if (Input.GetKey(KeyCode.DownArrow))
        {
            //transform.Translate(-Vector3.forward * speed);
            characterController.Move(-Vector3.forward * speed * Time.deltaTime);
            run = true;
            direction.z -= 1f;

        }

        if (direction.normalized.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

        }


        animator.SetBool("Run", run);


    }





}
