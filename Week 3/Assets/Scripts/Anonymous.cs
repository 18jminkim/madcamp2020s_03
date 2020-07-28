using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anonymous : MonoBehaviour
{

    public float power = 100f;
    public Transform P1;
    public Transform P2;
    public Transform me;
    public bool isDead; // for debugging only.
    public CharacterController characterController;

    public Rigidbody rb;
    //public Rigidbody body;
    Vector3 velocity;
    bool isGrounded;


    public float speed = 1f;
    public float gravity = -20f;
    public float jumpHeight = 3f;



    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;


    public Animator animator;
    public bool run;
    public bool moveable = true;
    public Collider rightFist;
    public bool punch;
    //public float height;


    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Vector3 direction = new Vector3(0f, 0f, 0f);

    private readonly System.Random random = new System.Random();
    private int dirVal;


    Collider jumpCollider;



    // Start is called before the first frame update
    void Start()
    {

        

        setRigidbodyState(true);
        setColliderState(false);
        setRightPunch(false);

        //jumpCollider = groundCheck.GetComponent<Collider>();
        if (!isDead && moveable)
        {
            randomMove();
        }





    }

    // Update is called once per frame
    void Update()
    {
        //height = me.position.y;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isDead)
        {
            die();
            //characterController.center = me.position + new Vector3(0f, 0.91f, 0f);
        }
        else
        {
            revive();
        }



        if (isGrounded && (velocity.y < -1))
        {
            velocity.y = -2f;
            animator.SetBool("Jump", false);
        }

        //jumpCollider.enabled = true;


        if (!isDead)
        {

            velocity.y += gravity * Time.deltaTime;

            characterController.Move(velocity * Time.deltaTime);

            if (direction.normalized.magnitude > 0f)
            {

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                run = true;
            }
            else
            {
                run = false;
            }

            characterController.Move(direction.normalized * speed * Time.deltaTime);

            animator.SetBool("Run", run);

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
        //me.SetPositionAndRotation(characterController.center, new Quaternion()) ;




    }

    void OnTriggerEnter(Collider collision)
    {
        string tag = collision.gameObject.tag;
        Transform hitter = null;


        if (tag == "P1")
        {
            Debug.Log("Bot struck by P1.");
            hitter = P1;
        }
        else if (tag == "P2")
        {
            Debug.Log("Bot struck by P2.");
            hitter = P2;

        }

        if (hitter != null)
        {

            Debug.Log(hitter.ToString());
            die();
            Vector3 direction = me.position - hitter.position;
            rb.AddForce(direction.normalized * power, ForceMode.Impulse);
            
            Debug.Log("Obstacle hit");
            Invoke("revive", 2f);
        }

        






    }

    private void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("P1 collision.");

            setRightPunch(false);
            //rb.isKinematic = false;
            Debug.Log("Collided with obstacle.");

            die();

            Vector3 direction = me.position - collision.transform.position;
            rb.AddForce(direction.normalized * power, ForceMode.Impulse);
            //Debug.Log("Collision");


            //Invoke("revive", 2f);

        }

    }

    private void OnCollisionEnter(Collision collision)
    {


    }


    void setRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodies)
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

        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            GetComponent<Rigidbody>().isKinematic = !state;
        }

        //groundCheck.GetComponent<Collider>().enabled = true;
        rightFist.enabled = true;
    }


    private void setRightPunch(bool state)
    {
        punch = state;
        rightFist.enabled = state;
        animator.SetBool("Punch", state);
    }
    // receives keyboard input to move the character.
    

    void jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

    }


    void disableRightPunch()
    {
        setRightPunch(false);
    }

    void randomMove()
    {
        if (isDead)
        {
            return;
        }

        dirVal = random.Next(8);
        float x = 0f;
        float y = 0f;
        float z = 0f;

        

        switch (dirVal)
        {


            case 0:
                x--;
                z--;
                break;
            case 1:
                x++;
                z++;
                break;
            case 2:
                x++;
                z--;
                break;
            case 3:
                x--;
                z++;
                break;
            case 4:
                x++;
                break;
            case 5:
                x--;
                break;
            case 6:
                z++;
                break;
            case 7:
                z--;
                break;

            default:
                direction = new Vector3(0f, 0f, 0f);
                break;


                
        }

        direction = new Vector3(x, y, z);

        Invoke("randomMove", 1f);


    }






}
