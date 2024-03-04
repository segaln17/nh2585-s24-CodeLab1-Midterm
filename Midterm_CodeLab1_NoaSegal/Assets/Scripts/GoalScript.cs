using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //change levels when hitting this object:
    private void OnTriggerEnter(Collider other)
    {
        ASCIILevelLoader.Instance.CurrentLevel++;
        //increment the current level by 1 by using the singleton to call the level loader property
    }
}
