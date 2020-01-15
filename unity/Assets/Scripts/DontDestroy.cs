using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// calls DontDestroyOnLoad on the music game object
/// this means that the audio will continue playing through scenes without restarting
/// </summary>

public class DontDestroy : MonoBehaviour
{
    // awake function will run as soon as the game begins
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject); // gameObject with music tag will not be destroyed upon scene change - this allows the background music to continue playing seamlessly
    }

}
