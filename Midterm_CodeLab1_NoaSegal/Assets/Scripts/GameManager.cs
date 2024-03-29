using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //create the singleton:
    public static GameManager instance;
    
    //variable for text display:
    public TextMeshProUGUI displayText;
    
    //initialize score:
    public int score;
    
    //create file path:
    //folder where the high score text file will go:
    const string FILE_DIR = "/DATA/";
    //text file where the high scores will go:
    const string DATA_FILE = "highScores.txt";
    //define the file path (operating system agnostic):
    string FILE_FULL_PATH;
    
    //property for setting the score:
    public int Score
    {
        get
        {
            return score;
            //pulls the value of lowercase score
        }
        set
        {
            score = value;
        }
    }
    
    //set up scene manager:
    //timer:
    public float timeLeft = 3f;
    public float timeToPlay = 3f;
    
    //check if out of levels:
    public bool isInGame = true;
    //public so I can access it in WinScript
    
    //empty string where scores will go in the text file:
    public string highScoresString = "";
    //made it public so I can access it in WinScript
    
    //create list of high scores:
    List<int> highScores;
    
    //property for the list of high scores:
    public List<int> HighScores
    {
        get
        {
            //if empty, make a new list:
            if (highScores == null)
            {
                highScores = new List<int>();
                /*
                //initialize values so the file generates:
                highScores.Add(0);
                highScores.Insert(0,3);
                highScores.Insert(1,2);
                highScores.Insert(2,1);
                */
                
                //if the file exists, read its contents:
                if (File.Exists(FILE_FULL_PATH))
                {
                    //pull the string from the text file and assign it to highScoresString:
                    highScoresString = File.ReadAllText(FILE_FULL_PATH);
                    //trim white space:
                    highScoresString = highScoresString.Trim();
                    //split based on new lines and make an array of those scores:
                    string[] highScoreArray = highScoresString.Split("\n");
                
                    //iterate through the array and translate it into ints
                    //then add the current score to the high scores list
                    for (int i = 0; i < highScoreArray.Length; i++)
                    {
                        int currentScore = Int32.Parse(highScoreArray[i]);
                        highScores.Add(currentScore);
                    }
                }
            }

            return highScores;
        }
    }
    
    //destroy duplicate singletons and keep the original:
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

    // Start is called before the first frame update
    void Start()
    {
        //define full path based on operating system:
        FILE_FULL_PATH = Application.dataPath + FILE_DIR + DATA_FILE;

    }

    // Update is called once per frame
    void Update()
    {
       if (isInGame)
       {
           GameObject LevelObjects = GameObject.Find("Level Objects");
           if (LevelObjects == null)
           {
               DelayedLoad();
           }
           //if still playing, update the text with the score and countdown timer until game is over
           displayText.text = "Score: " + score + "\nTimer: " + ((int)timeLeft);
       }
       
       else
       {
           //if the last level is completed, change the displayText to reflect the victory:
           if (ASCIILevelLoader.Instance.currentLevel >= 4)
           {
               displayText.text = "YOU WIN! \nFinal score: " + score + "\nHigh scores: \n" + highScoresString;
           }
           else
           {
               displayText.text = "GAME OVER \nFinal score: " + score + "\nHigh scores: \n" + highScoresString;
           }
           
       }
       
       //update timer:
       timeLeft -= Time.deltaTime;
       
       
       //if time is up, go to end screen:
       if (timeLeft <= 0 && isInGame)
       {
           isInGame = false;
           SceneManager.LoadScene("EndScene");
           //check if it's a high score
           SetHighScore();
       }
       
    }

    bool isHighScore(int score)
    {
        for (int i = 0; i < HighScores.Count; i++)
        {
            //iterate through the high scores list and check against the current value
            if (highScores[i] < score)
            {
                return true;
                //the score is higher so it's a high score
            }
            
        }

        return false;
    }

    //public so I can access it in WinScript
    public void SetHighScore()
    {
        //check if the current score is a high score, and put it in the list:
        if (isHighScore(score))
        {
            //include slot 0 in the loop:
            int highScoreSlot = -1;
            
            //iterate through the slots in the list and check if the score is higher:
            for (int i = 0; i < HighScores.Count; i++)
            {
                if (score > highScores[i])
                {
                    highScoreSlot = i;
                    break;
                }
            }
            //put the value in the list:
            highScores.Insert(highScoreSlot, score);
            
            //for display, only show the top 3 scores:
            highScores = highScores.GetRange(0, 3);
            
            //set up score board text for the high scores:
            string scoreBoardText = "";
            
            //put each high score in the scoreboardtext string with a separator:
            foreach (var highScore in highScores)
            {
                scoreBoardText += highScore + "\n";
            }
            
            //set string equal to scoreboardtext:
            highScoresString = scoreBoardText;
            
            //check if directory exists and if it doesn't, make one:
            if (!Directory.Exists(Application.dataPath + FILE_DIR))
            {
                Directory.CreateDirectory(Application.dataPath + FILE_DIR);
            }

            
            //write all the scores to the text file to save them:
            File.WriteAllText(FILE_FULL_PATH, highScoresString);
        }
    }

    public void ResetTime()
    {
        timeLeft = timeToPlay;
    }

    public void DelayedLoad()
    {
        ASCIILevelLoader.Instance.currentLevel = 0;
        ASCIILevelLoader.Instance.LoadLevel();
        GameObject.Find("Canvas").SetActive(true);
        ResetTime();
    }

    
    public void ResetLevel()
    {
        //Invoke("DelayedLoad", 0.5f);
        isInGame = true; //eventually put this into reset function
        score = 0;
        
    }
    
}
