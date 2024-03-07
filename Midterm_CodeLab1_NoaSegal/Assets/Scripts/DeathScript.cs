using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if you hit a spike, go to the endScene immediately:
        GameManager.instance.isInGame = false;
        SceneManager.LoadScene("EndScene");
        GameManager.instance.SetHighScore();

    }
}
