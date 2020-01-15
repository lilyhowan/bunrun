using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; // serialise data 
using UnityEngine.SceneManagement;

/// <summary> 
/// open and save data to file (converting from binary to txt format as necessary)
/// </summary> 

public class DataManager : MonoBehaviour
{
    // declare constant variables
    public const string scoreboardFileName = "/scoreboard.dat";

    // declare UI objects
    public GameObject scoreboardMenu;
    //public GameObject deathMenu;
    //public GameObject pauseMenu;

    // enable menu by setting scoreboardMenu active - this will leave any menus already open so that when the scoreboard is closed they will be in the same state
    public void OpenHighScore()
    {
        scoreboardMenu.SetActive(true);
    }

    // disable menu by setting scoreboardMenu inactive
    public void CloseHighScore()
    {
        scoreboardMenu.SetActive(false);
    }

    /*private void Start()
    {
        //Get the path of the Game data folder 
        //Debug.Log("Persistent path: " + Application.persistentDataPath);  // save to device itself

        //Output the Game data path to the console 
        //Debug.Log("Path: " + Application.dataPath);  // save to build/game folder
    }*/

    public void SaveScoreboard(List<HighScoreEntry> highscoreList)
    {
        Debug.Log("Save " + scoreboardFileName);

        //create binary object to save data to game folder 
        BinaryFormatter bf = new BinaryFormatter();

        //create|save file to C:\Users\username\AppData\Local\company\game using persistentDataPath 
        FileStream file = File.Create(Application.persistentDataPath + scoreboardFileName);

        // change file to binary format -  this makes it much harder to edit/reverse engineer the data stored within it
        bf.Serialize(file, highscoreList); // change file from txt to binary format
        file.Close();
    }

    // load high score data, opposite process to saving data
    public List<HighScoreEntry> LoadScoreboard()
    {
        List<HighScoreEntry> highscoreList = new List<HighScoreEntry>();

        if (File.Exists(Application.persistentDataPath + scoreboardFileName))
        {
            Debug.Log("File found");
            BinaryFormatter bf = new BinaryFormatter();

            //open file from C:\Users\username\AppData\Local\company\game 
            FileStream file = File.Open(Application.persistentDataPath + scoreboardFileName, FileMode.Open);

            // decode data to PlayerData format, ie change from binary to basic txt format
            List<HighScoreEntry> data = (List<HighScoreEntry>)bf.Deserialize(file); // change file from binary to txt format
            file.Close();
            highscoreList = data;
        }
        return highscoreList;
    }
}


// create own class rather than using a basic class from Unity such as MonoBehaviour
[Serializable]
public class HighScoreEntry
{
    public int score;
    public string name;
    public string house;
}

