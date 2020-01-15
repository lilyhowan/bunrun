using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// sets objects (platforms, enemies, collectibles) inactive when they are outside of the camera
/// this reduced CPU usage and keeps the game running smoothly
/// </summary>

public class PlatformDestroyer : MonoBehaviour
{
    // define public variables
    public GameObject platformDestructionPoint;

    // Start is called before the first frame update
    void Start()
    {
        platformDestructionPoint = GameObject.Find("PlatformDestructionPoint");
    }

    // check each frame to see if the position of the camera/player is greater than the destruction point - if true, set object inactive
    void Update()
    {
        if (transform.position.x < platformDestructionPoint.transform.position.x)
        {
            // using object pooling rather than Destroy(gameObject); reduces CPU usage as platforms are not constantly being generated and destroyed
            gameObject.SetActive(false);
        }
    }
}
