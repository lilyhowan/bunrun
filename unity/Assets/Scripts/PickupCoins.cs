using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// if object with this class is triggered by the player object
/// score is added to total score and the object is set inactive
/// this is used for coins
/// </summary>

public class PickupCoins : MonoBehaviour
{
    // declare public variables
    public int scoreValue;

    // define managers
    private ScoreManager scoreManager;

    // define audio variables
    private AudioSource coinSound;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        coinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
    }

    // when object with this class is triggered by contract with the player gameobject, add score and set inactive
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            scoreManager.AddScore(scoreValue);
            gameObject.SetActive(false);
            coinSound.Play();
        }
    }
}
