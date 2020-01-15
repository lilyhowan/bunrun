using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the game manager is crucial for large amount of tasks within the game to function
/// it contains the RestartGame() and Reset() functions, which reset the game by moving each object back to the original starting position and resetting scores/stamina to defaults
/// backgrounds will also be moved back to the original position and any object outside of the camera will be set inactive
/// </summary>

public class GameManager : MonoBehaviour
{
    // declare platform variables
    public Transform platformGenerator;
    private Vector3 platformStartPoint;
    private PlatformDestroyer[] platformList; // for destroying remaining platforms when player dies

    // declare player variables
    public PlayerController playerController;
    private Vector3 playerStartPoint;

    // declare managers
    private ScoreManager scoreManager;
    private StaminaManager staminaManager;

    // declare UI variables
    public DeathMenu deathScreen;
    public PauseMenu pauseMenu;
    public GameObject submitMenu;
    public GameObject invalidInputText;

    // declare background game objects
    public RepeatingBackground repeatingBackground;
    public RepeatingBackground repeatingForeground;
    public RepeatingBackground repeatingCloudBackground;

    // Start is called before the first frame update
    void Start()
    {
        // reset player and platform position when the game starts
        platformStartPoint = platformGenerator.position;
        playerStartPoint = playerController.transform.position;
        scoreManager = FindObjectOfType<ScoreManager>();
        staminaManager = FindObjectOfType<StaminaManager>();
    }

    // function to restart game, this is public so that it can be called from scripts other than the GameManager
    public void RestartGame()
    {
        scoreManager.scoreIncreasing = false;
        staminaManager.staminaDecreasing = false;
        playerController.gameObject.SetActive(false);

        //deathScreen.gameObject.SetActive(true);
        submitMenu.gameObject.SetActive(true);

        //StartCoroutine("RestartGameCoroutine");
    }

    // function to reset all aspects of game back to their original position/value
    public void Reset()
    {
        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false); // set each object in platformList inactive
        }
        playerController.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        playerController.gameObject.SetActive(true);
        deathScreen.gameObject.SetActive(false);
        submitMenu.gameObject.SetActive(false);
        invalidInputText.SetActive(false);
        repeatingBackground.ResetPosition();
        repeatingForeground.ResetPosition();
        repeatingCloudBackground.ResetPosition();
        scoreManager.scoreCount = 0;
        staminaManager.staminaCount = staminaManager.staminaInitialValue;
        scoreManager.scoreIncreasing = true;
        staminaManager.staminaDecreasing = true;
    }

    /*public IEnumerator RestartGameCoroutine()
    {
        scoreManager.scoreIncreasing = false;
        playerController.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f); // add a slight delay to allow player to understand that they have died
        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }
        playerController.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        playerController.gameObject.SetActive(true);
        scoreManager.scoreCount = 0;
        scoreManager.scoreIncreasing = true;
    }*/
}
