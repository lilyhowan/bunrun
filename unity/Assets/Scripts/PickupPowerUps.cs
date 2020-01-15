using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// when an object with this class is triggered by the player object
/// stamina is added and the object is set inactive
/// this is used for potions
/// </summary>

public class PickupPowerUps : MonoBehaviour
{
    // declare public variables
    public int staminaValue;

    // define managers
    private StaminaManager staminaManager;

    /*public bool doublePoints;
    public bool safeMode;

    public float powerupLength;*/

    // Start is called before the first frame update
    void Start()
    {
        staminaManager = FindObjectOfType<StaminaManager>();
    }

    // when object with this class is triggered by the player object, add stamina and set inactive
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            staminaManager.AddStamina(staminaValue);
            gameObject.SetActive(false);
        }

    }
}
