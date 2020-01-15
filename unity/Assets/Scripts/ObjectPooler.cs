using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// creates the object pool list that objects are pulled from
/// this is used for platforms, enemies and collectibles
/// </summary>

public class ObjectPooler : MonoBehaviour
{
    // define public variables
    public GameObject pooledObject;
    public int pooledAmount;

    // create list for platform
    List<GameObject> pooledObjects;

    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>(); // create list of game objects

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject); // (GameObject) casts pooledObject into a GameObject so that it can instantiate
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    // define function to search pooledObjects list for inactive object
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy) // when inactive (!) object is found, return said object
            {
                return pooledObjects[i];
            }
        }

        GameObject obj = (GameObject)Instantiate(pooledObject); // cast pooledObject into game object obj
        obj.SetActive(false);
        pooledObjects.Add(obj); // add to list of objects
        return obj; // return obj
    }
}
