using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// creates the scoreboard when required
/// this sorts the high score list and sets up the text so that it is displayed correctly
/// </summary>

public class Scoreboard : MonoBehaviour
{
    // declare UI variables
    public Button closeButton;
    public Text scoreboardRankText, scoreboardNameText, scoreboardScoreText, scoreboardHouseText;

    // declare managers
    public GameManager gameManager;
    public DataManager dataManager;

    private void Start()
    {
        // add listener to button, this will set the menu inactive when the button is pressed
        closeButton.onClick.AddListener(delegate { this.gameObject.SetActive(false); });
    }

    private void OnEnable()
    {
        SetupBoard();
    }

    // create scoreboard when called (formatting text for score, name, house and position)
    private void SetupBoard()
    {
        scoreboardRankText.text = "";
        scoreboardNameText.text = "";
        scoreboardScoreText.text = "";
        scoreboardHouseText.text = "";
        List<HighScoreEntry> highScoreList = new List<HighScoreEntry>();

        // Load HighScore Entries 
        //Debug.Log("Setup board using text box");
        highScoreList = dataManager.LoadScoreboard(); // read List from file 
        highScoreList = SortScoreboard(highScoreList); // sort List 
        dataManager.SaveScoreboard(highScoreList); // write List back to file 

        if (highScoreList.Count > 10)
        {
            highScoreList.RemoveRange(10, highScoreList.Count - 10);  // remove values beyond the first ten in highScoreList
        }

        for (int i = 0; i < highScoreList.Count; i++) // limit scoreboard display to top 10 values via adjusted highScoreList.Count
        {
            scoreboardRankText.text = scoreboardRankText.text + (i + 1).ToString() + "\n";
            scoreboardNameText.text = scoreboardNameText.text + highScoreList[i].name + "\n";
            scoreboardScoreText.text = scoreboardScoreText.text + highScoreList[i].score.ToString() + "\n";
            scoreboardHouseText.text = scoreboardHouseText.text + highScoreList[i].house + "\n";
        }
    }

    // sort highScoreList (HighScoreEntryList) 
    public List<HighScoreEntry> SortScoreboard(List<HighScoreEntry> highScoreList)
    {
        Debug.Log("Scoreboard sorted");
        HighScoreEntry temp;

        for (int i = 0; i < highScoreList.Count; i++)  // repeat until i is greater than the length of the high score list
        {
            for (int j = i + 1; j < highScoreList.Count; j++) // sort values from highest to lowest score for correct display on the scoreboard
            {
                if (highScoreList[j].score > highScoreList[i].score)
                {
                    // swap 
                    temp = highScoreList[i];
                    highScoreList[i] = highScoreList[j];
                    highScoreList[j] = temp;
                }
            }
        }
        return highScoreList;
    }
}

