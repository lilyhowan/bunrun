using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// controls player input within the death menu
/// player must submit their name and house - input will be change to uppercase with white space removed from the edges of the string
/// if name is not blank and contains only alphabetical characters, it will be valid
/// house must be 3 alphabetical letters with the string starting with B, M, J, P or D to be valid
/// a message is displayed within the game if invalid to ensure that the player knows why their score is not submitting
/// </summary>

public class UserInputManager : MonoBehaviour
{
    // declare UI variables
    public InputField nameInputField;
    public InputField houseInputField;
    public GameObject submitMenu;
    public GameObject deathMenu;
    public Button submitButton;
    public Button restartButton;
    public Button quitButton;
    public PauseMenu pauseMenu;

    // declare private variables
    private string userName = "";
    private string userHouse = "";

    // declare managers
    public GameManager gameManager;
    public DataManager dataManager;
    private ScoreManager scoreManager;
    private UserInputManager userInputManager;

    // declare public variables
    public GameObject invalidInputText;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();

        // add listeners to buttons, this will call the function when the button is pressed
        submitButton.onClick.AddListener(delegate { GetUserInput(); });
        restartButton.onClick.AddListener(delegate { pauseMenu.RestartGame(); });
        quitButton.onClick.AddListener(delegate { pauseMenu.QuitToMain(); });
    }


    // function to check if user input is alphabetical, return true if true (boolean value) - validating user input
    public static bool IsAllLetters(string s)
    {
        foreach (char c in s) // run following if loop for every character in the string s
        {
            if (!char.IsLetter(c)) // if character is not alphabetical, continue to next if loop else return true
            {
                if (!char.IsWhiteSpace(c)) // allow white space for names with spaces in the middle - return turn if character is not alphabetical or white space
                {
                    return false;
                }
            }

        }
        return true;
    }

    // function to check if userHouse string begins with a house letter, return true if true
    public static bool IsValidHouse(string s)
    {
        if (s[0] == 'J' || s[0] == 'M' || s[0] == 'B' || s[0] == 'P' || s[0] == 'D') // if the first letter in string s is J, M, B, P or D, return true (valid houses)
        {
            return true;
        }
        return false;
    }

    // function called when user presses the submit button - checks that user input is valid using various conditions
    public void GetUserInput()
    {

        userName = nameInputField.text.Trim().ToUpper();  // .Trim() will remove any white space at the start or end of the string before it is checked
        userHouse = houseInputField.text.Trim().ToUpper(); // .ToUpper() is used to ensure consistency between values

        if (userName != "" && (IsAllLetters(userName)) && (IsAllLetters(userHouse)) && userHouse != "" && (userHouse.Length == 3) && IsValidHouse(userHouse)) // boundary length - no longer than 3 characters
        {
            List<HighScoreEntry> tempHighScoreList = new List<HighScoreEntry>(); // create temporary list to store data 

            tempHighScoreList = dataManager.LoadScoreboard(); // read List from file

            tempHighScoreList.Add(new HighScoreEntry() { name = userName, house = userHouse, score = (int)scoreManager.scoreCount }); // add new entry to temp list

            dataManager.SaveScoreboard(tempHighScoreList); // write List to file

            invalidInputText.SetActive(false); // hide error message if input is valid
            submitMenu.SetActive(false); // hide submit menu
            deathMenu.SetActive(true); // open death/game over menu
        }
        else
        {
            invalidInputText.SetActive(true); // display text telling the user their input is invalid
        }
    }

}
