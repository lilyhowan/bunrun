using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controls camera movement within the game
/// as the camera moves a distance equal to the distance moved by the player each frame,
/// the perceieved movement of the camera is static while the platforms/player appear to be moving
/// </summary>
public class CameraController : MonoBehaviour
{
    // set public variables
    public PlayerController playerCharacter;

    // set private variables
    private Vector3 lastPlayerPosition;
    private float distanceToMove;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = FindObjectOfType<PlayerController>();
        lastPlayerPosition = playerCharacter.transform.position;
    }

    // each frame, the camera moves a distance equal to the distance moved by the player
    // this means that the perceieved movement of the camera is static while the platforms/player appear to be moving
    void Update()
    {
        // distance to move equal to current position minus previous position
        distanceToMove = playerCharacter.transform.position.x - lastPlayerPosition.x;

        // move camera position on x axis, keep y and z axis static
        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);

        // take xyz values from playerCharacter and set as lastPlayerPosition
        lastPlayerPosition = playerCharacter.transform.position;
    }
}
