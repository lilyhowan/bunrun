using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// generates coins by setting them active in the specific position when SpawnCoins() is called
/// </summary>

public class CoinGenerator : MonoBehaviour
{
    // declare public variables
    public ObjectPooler coinPool;
    public float coinDistanceX;
    public float coinDistanceY;

    // function to create coins within game
    public void SpawnCoins(Vector3 startPosition)
    {
        GameObject coin1 = coinPool.GetPooledObject(); // pull coin from object pool
        coin1.transform.position = new Vector3(startPosition.x, startPosition.y + coinDistanceY, startPosition.z); // centre coin (no X distance change)
        coin1.SetActive(true);

        GameObject coin2 = coinPool.GetPooledObject();
        coin2.transform.position = new Vector3 (startPosition.x - coinDistanceX, startPosition.y + coinDistanceY, startPosition.z); // left coin (- X axis)
        coin2.SetActive(true);

        GameObject coin3 = coinPool.GetPooledObject();
        coin3.transform.position = new Vector3 (startPosition.x + coinDistanceX, startPosition.y + coinDistanceY, startPosition.z); // right coin (+ X axis)
        coin3.SetActive(true);
    }
}
