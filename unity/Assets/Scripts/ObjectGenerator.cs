using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// used to generate objects (platforms, enemies, collectibles)
/// the position/amount of each object is determined by Random.Range within a specific range
/// </summary>

public class ObjectGenerator : MonoBehaviour
{
    // define public variables
    public GameObject gamePlatform;
    public Transform generationPoint;
    public float platformDistance;
    public float platformDistanceMin;
    public float platformDistanceMax;

    public ObjectPooler[] objectPools;
    //public GameObject[] platformArray;

    // define private variables
    private int platformSelector;
    private float[] platformWidth; // to prevent platforms generating on top of each other

    // define platform height variables
    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightDifference;
    private float heightDifference;

    // define coin generation variables
    private CoinGenerator coinGenerator;
    //public float randomCoinThreshold;

    // define enemy generation variables
    public float randomEnemyThreshold;
    public ObjectPooler enemyPool;

    // define potion generation variables
    public float randomPotionThreshold;
    public ObjectPooler potionPool;

    public float randomCollectibleThreshold;

    // Start is called before the first frame update
    void Start()
    {
        //platformWidth = gamePlatform.GetComponent<BoxCollider2D>().size.x;

        platformWidth = new float[objectPools.Length];
        for (int i = 0; i < objectPools.Length; i++)
        {
            platformWidth[i] = objectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x; // get platform length for each platform in objectPools
        }

        minHeight = transform.position.y; // minimum platform height
        maxHeight = maxHeightPoint.position.y; // maximum platform height

        coinGenerator = FindObjectOfType<CoinGenerator>();
    }

    // randomly generates objects each frame - the exception to this is that platforms will generate when the camera has moved a certain distance
    void Update()
    {
        if (transform.position.x < generationPoint.position.x) // boundary for platform
        {
            platformDistance = Random.Range(platformDistanceMin, platformDistanceMax); // randomly select distance between platforms
            platformSelector = Random.Range(0, objectPools.Length); // select platform from range between 0 and amount of platforms within array

            heightDifference = transform.position.y + Random.Range(maxHeightDifference, -maxHeightDifference); // height difference randomly selected from between maxHeightDifference and negative maxHeightDifference value

            // to prevent platforms from going outside of the camera range using minHeight and maxHeight floats
            if (heightDifference > maxHeight) // boundary for platform height
            {
                heightDifference = maxHeight;
            }
            else if (heightDifference < minHeight)
            {
                heightDifference = minHeight;
            }

            transform.position = new Vector3(transform.position.x + (platformWidth[platformSelector] / 2) /* divide by two to prevent platforms overlapping */ + platformDistance, heightDifference, transform.position.z);

            // using object pooling rather than instantiate reduces CPU usage by reusing a limited amount of platforms
            // rather than constantly creating and destroying platforms throughout the entire game
            // Instantiate(/*gamePlatform*/ platformArray[platformSelector], transform.position, transform.rotation);

            GameObject newPlatform = objectPools[platformSelector].GetPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            // randomly generating coins within randomCoinThreshold range
            if (Random.Range(0f, 100f) < randomCollectibleThreshold)
            {
                if (Random.Range(0f, 100f) < randomPotionThreshold)
                {
                    GameObject newPotion = potionPool.GetPooledObject();

                    newPotion.transform.position = transform.position + new Vector3(0f, 3f, 0f); // set position + Vector3 value
                    newPotion.transform.rotation = transform.rotation; // set rotation
                    newPotion.SetActive(true);
                }
                else
                {
                    coinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
                }
            }

            // randomly generating enemies within randomEnemyThreshold range
            if (Random.Range(0f, 100f) < randomEnemyThreshold)
            {
                GameObject newEnemy = enemyPool.GetPooledObject();

                /* choose from platform width array based on current platform (negative for left side)
                1f ensures that the enemies are always on the platform (1 space in)
                this changes the position of the enemies on the platform */
                float enemyXPosition = Random.Range(-platformWidth[platformSelector] / 2f + 1f, platformWidth[platformSelector] / 2f - 1f);  

                Vector3 enemyPosition = new Vector3(enemyXPosition, 0.5f, 0f); // Vector3 has x, y and z values, used to place enemy in centre of platform

                newEnemy.transform.position = transform.position + enemyPosition; // set position + Vector3 value
                newEnemy.transform.rotation = transform.rotation; // set rotation
                newEnemy.SetActive(true);
            }

            transform.position = new Vector3(transform.position.x + (platformWidth[platformSelector] / 2), transform.position.y, transform.position.z);

        }
    }
}
