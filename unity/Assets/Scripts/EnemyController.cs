using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// when objects with this class are triggered by contact with the player game object
/// stamina will be decreased and the object will be set inactive - this is used for enemies and any object that should reduce stamina
/// </summary>

public class EnemyController : MonoBehaviour
{
    // declare public variables
    public int staminaValue;

    // declare private variables
    private StaminaManager staminaManager;

    // declare audio variables
    private AudioSource deathSound;

    // Start is called before the first frame update
    void Start()
    {
        staminaManager = FindObjectOfType<StaminaManager>();
        deathSound = GameObject.Find("DeathSound").GetComponent<AudioSource>();
    }

    // decrease stamina, remove enemy object and trigger death sound effect when an enemy comes into contact with the player object
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            staminaManager.DecStamina(staminaValue);
            gameObject.SetActive(false);
            deathSound.Play();
        }
    }

}
