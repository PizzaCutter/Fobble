using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnZone
    {
        [SerializeField]
        public BoxCollider2D colliderZone;
    }

    [SerializeField]
    private List<SpawnZone> SpawnZones;

    [SerializeField]
    private List<GameObject> Enemies;

    [SerializeField]
    private AnimationCurve SpawnTimeInterval = null;

    private float SpawnTimer = 0.0f;
    private float timeSinceStart = 0.0f;

    private Dictionary<GameObject, List<GameObject>> EnemyPool = new Dictionary<GameObject, List<GameObject>>();

    void Update()
    {
        timeSinceStart += Time.deltaTime;

        if (SpawnTimer <= 0.0f)
        {
            SpawnTimer = SpawnTimeInterval.Evaluate(timeSinceStart);
            SpawnEnemy();
        }
        else
        {
            SpawnTimer -= Time.deltaTime;
        }
    }

    GameObject GetEnemy(GameObject enemy)
    {
        GameObject retrievedEnemy = null;

        if(EnemyPool.ContainsKey(enemy))
        {
            foreach (var e in EnemyPool[enemy])
            {
                if(e == null)
                {
                    continue;
                }
                if(e.activeSelf == false)
                {
                    return e;
                }
            }
        }
        else
        {
            EnemyPool.Add(enemy, new List<GameObject>());
        }

        if(retrievedEnemy == null)
        {
            retrievedEnemy = Instantiate(enemy, transform);
            EnemyPool[enemy].Add(retrievedEnemy);
        }
        return retrievedEnemy;
    }
    
    void SpawnEnemy()
    {
        SpawnZone randomSpawnZone = SpawnZones[Random.Range(0, SpawnZones.Count)];
        float xOffset = Random.Range(0.0f, randomSpawnZone.colliderZone.size.x) - randomSpawnZone.colliderZone.size.x / 2.0f;
        float yOffset = Random.Range(0.0f, randomSpawnZone.colliderZone.size.y) - randomSpawnZone.colliderZone.size.y / 2.0f;
        xOffset += randomSpawnZone.colliderZone.transform.position.x;
        yOffset += randomSpawnZone.colliderZone.transform.position.y;
        Vector2 spawnLocation = new Vector2(xOffset, yOffset);

        GameObject go = GetEnemy(Enemies[Random.Range(0, Enemies.Count)]);
        //go.transform.position = spawnLocation;
        //go.GetComponent<Rigidbody2D>().position = spawnLocation;
        go.transform.position = spawnLocation;
        go.SetActive(true);
    }
}
