using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    bool p1_dead;
    public Transform cam;
    public Transform playerCol;
    public Transform playerHit;
    public float thresholdV = 10f;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = (new Vector3(0f, 7.35f, -6f));
        p1_dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!p1_dead)
        {
            //안죽었을떄 카메라
            cam.position = playerCol.position + offset;
        }
        else
        {
            //죽었을 떄
            cam.position = playerHit.position + offset;
        }
    }

    public void P1Dead()
    {
        p1_dead = true;
    }

    public void P1Revive()
    {
        p1_dead = false;
    }




}
