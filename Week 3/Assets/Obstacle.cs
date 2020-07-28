using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public Transform P1;
    public Transform P2;

    public float power = 100f;
    public Transform me;
    public Rigidbody rb;
    public float thresholdV = 10f;
    public Vector3 lastP = new Vector3(0f, 0f, 0f);

    Collider col;

    // Start is called before the first frame update
    void Start()
    {
        rb = me.GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if ((me.position - lastP).magnitude > thresholdV  )
        {
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }
        */


        lastP = me.position;

    }



    private void OnTriggerEnter(Collider collision)
    {

        string tag = collision.gameObject.tag;
        Transform hitter = null;


        if (tag == "P1")
        {
            Debug.Log("Obstacle stuck by P1.");
            hitter = P1;
        }
        else if (tag == "P2")
        {
            Debug.Log("Obstacle stuck by P1.");
            hitter = P2;

        }

        if (hitter != null)
        {
            Vector3 direction = me.position - hitter.position;
            rb.AddForce(direction.normalized * power, ForceMode.Impulse);
            Debug.Log("Obstacle hit");
        }



    }
}
