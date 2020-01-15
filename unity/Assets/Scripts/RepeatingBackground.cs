using UnityEngine;
using System.Collections;

/// <summary>
/// controls movement of the background
/// if the camera reaches a certain point, the background will scroll to the right
/// this class is on each individual part of the background so by setting a speed, a parallax effect is created
/// </summary>

public class RepeatingBackground : MonoBehaviour
{
    // declare public variables
    public float backgroundSize;

    // declare private variables
    private Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;

    // declare parallax variables
    public float parallaxSpeed;
    private float lastCameraX;

    // set initial variables so that they can be reset when the game restarts
    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }
        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    // each frame, check camera position and move background right if greater than rightIndex - viewZone
    private void Update()
    {
        float deltaX = cameraTransform.position.x - lastCameraX;
        transform.position += Vector3.right * (deltaX * parallaxSpeed);
        lastCameraX = cameraTransform.position.x;

        if (cameraTransform.position.x > layers[rightIndex].transform.position.x - viewZone)
        {
            ScrollRight();
        }
    }

    // function to scroll background toward the right side of the screen - left scroll is not required as the game only moves in one direction
    private void ScrollRight()
    {
        int lastLeft = leftIndex;
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        rightIndex = leftIndex;
        leftIndex ++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }

    // function to reset position when the game ends
    public void ResetPosition()
    {
        layers[leftIndex].position = new Vector3((-lastCameraX), 0f, 0f);
        layers[rightIndex].position = new Vector3((-lastCameraX), 0f, 0f);
    }
}
