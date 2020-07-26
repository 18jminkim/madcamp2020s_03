using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public bool isDead;
    public CharacterController characterController;
    
    private void OnCollisionEnter(Collision collision)
    {
        die();
        
    }
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
        
    }

    public void die()
    {
        GetComponent<Animator>().enabled = false;
        setRigidbodyState(false);
        setColliderState(true);
        characterController.enabled = false;


    }

    public void revive()
    {

        GetComponent<Animator>().enabled = true;
        setRigidbodyState(true);
        setColliderState(false);
        characterController.enabled = true;


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





}
