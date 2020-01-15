using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// the score manager controls the addition of points to the total score
/// each second a set amount of points will be added, and if AddScore() is called pointsToAdd will be added
/// AddScore() is used by coins
/// </summary>

public class ScoreManager : MonoBehaviour
{
    // declare UI variables
    public Text scoreText;
    public Text highScoreText;

    // declare public variables
    public float scoreCount;
    //public float highScoreCount;
    public float pointsPerSecond;
    public bool scoreIncreasing;

    // Start is called before the first frame update
    /*void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreCount = PlayerPrefs.GetFloat("HighScore");
        }  
    }*/

    // each frame check if scoreIncreasing is true, if yes add points to score (per second - value determined by pointsPerSecond)
    void Update()
    {
        if (scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime; // increasing scoreCount by pointsPerSecond every 1 second while scoreIncreasing is true
        }

        /*if (scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", highScoreCount); // saving high score via player prefs
        }*/

        scoreText.text = "" + Mathf.Round(scoreCount);
        //highScoreText.text = "High Score: " + Mathf.Round(highScoreCount);
    }

    // function will add points to scoreCount when called
    public void AddScore(int pointsToAdd)
    {
        scoreCount += pointsToAdd;
    }

}

