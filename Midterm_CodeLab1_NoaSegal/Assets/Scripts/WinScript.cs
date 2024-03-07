using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
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
        //when the player runs out of levels:
        //when player hits FinalCube, go to the EndScene
        GameManager.instance.isInGame = false;
        SceneManager.LoadScene("EndScene");
        GameManager.instance.SetHighScore();
    }

    
}
