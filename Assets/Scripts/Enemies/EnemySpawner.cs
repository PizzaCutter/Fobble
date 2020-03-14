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

    struct EnemyObj
    {
        public GameObject go;
        public Enemy enemyScript;
    }

    [SerializeField]
    private List<SpawnZone> SpawnZones = new List<SpawnZone>();

    [SerializeField]
    private List<GameObject> Enemies = new List<GameObject>();

    [SerializeField]
    private AnimationCurve SpawnTimeInterval = null;

    private float SpawnTimer = 0.0f;
    private float timeSinceStart = 0.0f;

    private Dictionary<GameObject, List<EnemyObj>> EnemyPool = new Dictionary<GameObject, List<EnemyObj>>();

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
                if(e.go == null)
                {
                    continue;
                }
                if(e.go.activeSelf == false)
                {
                    return e.go;
                }
            }
        }
        else
        {
            EnemyPool.Add(enemy, new List<EnemyObj>());
        }

        if(retrievedEnemy == null)
        {
            retrievedEnemy = Instantiate(enemy, transform);
            EnemyObj enemyObj = new EnemyObj();
            enemyObj.go = retrievedEnemy;
            enemyObj.enemyScript = retrievedEnemy.GetComponent<Enemy>();
            EnemyPool[enemy].Add(enemyObj);
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
        go.transform.position = spawnLocation;
        go.SetActive(true);
    }

    public void KillAll()
    {
        foreach(var enemy in EnemyPool)
        {
            foreach(var enemyObj in enemy.Value)
            {
                if(enemyObj.enemyScript != null)
                {
                    enemyObj.enemyScript.Kill();
                }
            }
        }

        SpawnTimer = 0.0f;
        timeSinceStart = 0.0f;
    }
}
