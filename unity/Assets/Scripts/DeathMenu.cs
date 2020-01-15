using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// controls aspects of the game over/death menu
/// when RestartGame() or QuitToMain() are called on buttons they will perform their respective functions
/// </summary>

public class DeathMenu : MonoBehaviour
{
    // define public variables
    public string mainMenuLevel;

    // define UI variables
    public Button restartButton;
    public Button scoreboardButton;
    public Button quitButton;

    // define managers
    public DataManager dataManager;

    //public GameObject scoreboardMenu;
    //public GameObject deathMenu;

    private void Start()
    {
        // add listeners to buttons, this will call the function when the button is pressed
        restartButton.onClick.AddListener(delegate { RestartGame(); });
        scoreboardButton.onClick.AddListener(delegate { dataManager.OpenHighScore(); });
        quitButton.onClick.AddListener(delegate { QuitToMain(); });
    }

    public void RestartGame()
    {
        FindObjectOfType<GameManager>().Reset(); // find GameManager within scene and run Reset function
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenuLevel); // close main game scene and open main menu scene
    }
}
