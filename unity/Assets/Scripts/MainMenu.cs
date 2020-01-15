using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// controls buttons within the main menu
/// this is a separate script as it is isolated to the menu scene
/// </summary>

public class MainMenu : MonoBehaviour
{
    // define variables
    public string playGameLevel;

    // define UI variables
    public Button playButton;
    public Button scoreboardButton;
    public Button quitButton;

    // define managers
    public DataManager dataManager;

    private void Start()
    {
        // add listeners to buttons, this will call the function when the button is pressed
        playButton.onClick.AddListener(delegate { PlayGame(); });
        scoreboardButton.onClick.AddListener(delegate { dataManager.OpenHighScore(); });
        quitButton.onClick.AddListener(delegate { QuitGame(); });
    }

    // function to change from main menu to game scene
    public void PlayGame()
    {
        SceneManager.LoadScene(playGameLevel);
    }

    // function to close game
    public void QuitGame()
    {
        Application.Quit();
    }
}
