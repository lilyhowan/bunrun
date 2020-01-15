using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// the stamina manager controls the addition/removal of stamina
/// stamina is removed from the player each second if the game is not paused/stamina value above 0
/// AddStamina() is for potions while DecStamina() is for enemies
/// </summary>

public class StaminaManager : MonoBehaviour
{
    // declare UI variables
    public Text staminaText;
    public Image staminaBar;

    // declare public variables
    public float staminaCount;
    public float staminaInitialValue;
    public float staminaMaxValue;
    public float staminaPerSecond;
    public bool staminaDecreasing;

    // Start is called before the first frame update
    void Start()
    {
        staminaCount = staminaInitialValue; // set value so that it can be reset when the game restarts
    }

    // check each frame if staminaDecreasing is true and greater/equal to 0 - if true, continue to remove stamina each second, otherwise end the game
    void Update()
    {
        if (staminaDecreasing && staminaCount >= 0)
        {
            staminaCount -= staminaPerSecond * Time.deltaTime;
            //staminaText.text = "Stamina " + Mathf.Round(staminaCount);
            staminaBar.transform.localScale = new Vector3(staminaCount, 1.0f, 1.0f); // transform stamina bar in accordance with how much stamina the player currently has
        }
        else
        {
            FindObjectOfType<GameManager>().RestartGame();
        }
           
    }

    // function to remove stamina when called
    public void DecStamina(int staminaToTake)
    {
        staminaCount -= staminaToTake;
    }

    // function to add stamina when called - if over max stamina value, the stamina count will be set to the max stamina value to ensure it doesn't overflow
    public void AddStamina(int staminaToGive)
    {
        if ((staminaCount + staminaToGive) > staminaMaxValue)
        {
            staminaCount = staminaMaxValue;
        }
        else
        {
            staminaCount += staminaToGive;
        }

    }
}
