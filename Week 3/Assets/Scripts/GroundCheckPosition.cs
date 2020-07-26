using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckPosition : MonoBehaviour
{
    public Transform groundCheck;
    public Transform player;
    void Start()
    {
        groundCheck.parent = player;
        groundCheck.transform.localPosition = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
