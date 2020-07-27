using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPunch2 : MonoBehaviour
{
    BoxCollider boxCollider;

    public 
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GameObject.Find("Player1").GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Period))
        {
			boxCollider.enabled = true;
            Debug.Log("활성화 되야함");
		}
        else
        {
            boxCollider.enabled = false;
        }
    }

}