using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;
using Unity.VisualScripting;
using File = System.IO.File;

public class ASCIILevelLoader : MonoBehaviour
{
    public static ASCIILevelLoader Instance; //singleton
    private GameObject level;
    int currentLevel = 0;

    //property to change levels:
    public int CurrentLevel
    {
        get
        {
            return currentLevel;
        }

        set
        {
            currentLevel = value;
            LoadLevel(); //change levels
        }
    }
    
    string FILE_PATH; //not a constant because depends on operating system

    //check for existence of instance in the scene:
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //set FILE_PATH:
        FILE_PATH = Application.dataPath + "/Levels/LevelNum.txt";
        //Load in the level:
        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadLevel()
    {
        //destroy the previous:
        Destroy(level);
        
        //create a new gameObject to hold everything in the level:
        level = new GameObject("Level Objects");
        
        //create an array of strings to contain the contents of the file
        //and replace "Num" with the current level
        string[] lines = File.ReadAllLines(FILE_PATH.Replace("Num", currentLevel + ""));
        
        //loop through the array of strings:
        for (int yLevelPos = 0; yLevelPos < lines.Length; yLevelPos++)
        {
            Debug.Log(lines[0]);
            
            //get a single line:
            string line = lines[yLevelPos].ToUpper(); //makes them uppercase
            
            //make the line into an array of characters so each character has a unique position:
            char[] characters = line.ToCharArray();
            
            //loop through the characters on each line:
            for (int xLevelPos = 0; xLevelPos < characters.Length; xLevelPos++)
            {
                char c = characters[xLevelPos];
                Debug.Log(c); //print out the character

                GameObject newObject = null;

                switch (c) //different cases for loading the prefab
                {
                    //if a wall:
                    case 'W':
                        newObject = Instantiate(Resources.Load<GameObject>("Prefabs/Wall"));
                        break;
                    
                }

                if (newObject != null)
                {
                    //parent the newObject to the level so everything is contained neatly:
                    newObject.transform.parent = level.transform;

                    //build the level based on the position of the characters in the array:
                    newObject.transform.position = new Vector3(xLevelPos, -yLevelPos, 0);
                    //-y because otherwise it builds it backwards
                }
            }
        }
    }
}
