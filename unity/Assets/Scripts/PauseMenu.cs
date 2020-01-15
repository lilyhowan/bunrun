using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// controls buttons within the pause menu and the pause button itself
/// the pause button can be opened via mouse click OR the escape key
/// when the pause menu is active, the time scale is set to 0 which will pause all movement/score/stamina changes
/// </summary>

public class PauseMenu : MonoBehaviour
{
    // define public variables
    public string mainMenuLevel;

    // define managers
    public DataManager dataManager;

    // define UI variables
    public GameObject pauseMenu;
    public DeathMenu deathScreen;
    public GameObject submitMenu;
    public Button resumeButton;
    public Button restartButton;
    public Button scoreboardButton;
    public Button quitButton;
    public Button pauseButton;

    private void Start()
    {
        // add listeners to buttons, this will call the function when the button is pressed
        restartButton.onClick.AddListener(delegate { RestartGame();});
        scoreboardButton.onClick.AddListener(delegate { dataManager.OpenHighScore();});
        quitButton.onClick.AddListener(delegate { QuitToMain();});
        resumeButton.onClick.AddListener(delegate { ResumeGame(); });
        pauseButton.onClick.AddListener(delegate { PauseGame(); });
    }

    // checks each frame to see if the escape key has been pressed - if it has, call function
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // open pause menu when esc key is pressed (alternative to pause button)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (!deathScreen.isActiveAndEnabled && !submitMenu.activeInHierarchy) // only allow pause menu to open when death menu and submit menu are NOT active
        {
            Time.timeScale = 0f; // pause time (score) while pause menu is active
            pauseMenu.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // resume time when pause menu is deactivated
        pauseMenu.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        FindObjectOfType<GameManager>().Reset(); // find GameManager within scene and run Reset function
    }

    public void QuitToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuLevel); // quit to main menu by switching scenes
    }
}
