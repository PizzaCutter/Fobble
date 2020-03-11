using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> ProjectilePrefabs = new List<GameObject>();
    [SerializeField]
    private int AmountToSpawn = 1;

    private Dictionary<GameObject, List<GameObject>> AvailableProjectiles = new Dictionary<GameObject, List<GameObject>>();

    void Start()
    {
        foreach (GameObject gameObject in ProjectilePrefabs)
        {
            SpawnNewPoolObjects(gameObject, AmountToSpawn);
        }
    }

    void SpawnNewPoolObjects(GameObject objectToSpawn, int count)
    {
        List<GameObject> spawnedObjects = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            GameObject spawnedGo = Instantiate(objectToSpawn, transform);
            spawnedGo.SetActive(false);
            spawnedObjects.Add(spawnedGo);
        }

        if (AvailableProjectiles.ContainsKey(objectToSpawn))
        {   
            AvailableProjectiles[objectToSpawn].AddRange(spawnedObjects);
        }
        else
        {
            AvailableProjectiles.Add(objectToSpawn, spawnedObjects);
        }
    }

    public GameObject GetProjectileFromPool(GameObject go)
    {
        List<GameObject> objectsInPool;
        if(AvailableProjectiles.TryGetValue(go, out objectsInPool))
        {
           foreach(GameObject pooledObject in objectsInPool)
           {
                if(pooledObject.activeSelf == false)
                {
                    return pooledObject;
                }
           }
        }

        SpawnNewPoolObjects(go, AmountToSpawn);

        return GetProjectileFromPool(go);
    }
}
