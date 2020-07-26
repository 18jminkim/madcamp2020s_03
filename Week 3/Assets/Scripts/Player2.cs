using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{

    public Transform player1;
    public Transform player2;
    Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Punch")
        {
            Vector3 direction = player2.position - player1.position;
            rigidBody.AddForce(direction, ForceMode.Impulse);
            Debug.Log("punch");
        }
    }
}
