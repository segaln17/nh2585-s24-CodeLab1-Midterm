using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDScript : MonoBehaviour
{
    //public static WASDScript instance;
        
    Rigidbody rb;
    
    //declare and initialize forceAmount:
    public float forceAmt = 5f;

    /*
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        //get access to rigidbody component of the thing this is on:
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //keycode inputs for directional movement:
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.up * forceAmt);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * forceAmt);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * forceAmt);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.down * forceAmt);
        }
    }
}
