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
    private List<GameObject> Enemies = new List<GameObject>();

    [SerializeField]
    private AnimationCurve SpawnTimeInterval = null;

    private float SpawnTimer = 0.0f;
    private float timeSinceStart = 0.0f;

    private Dictionary<GameObject, List<EnemyObj>> EnemyPool = new Dictionary<GameObject, List<EnemyObj>>();

    private float ScreenWidth = 1.0f;
    private float ScreenHeight = 1.0f;

    public bool StopSpawning = true;

    void Awake()
    {
        Camera camera = Camera.main;
        if (camera == null)
        {
            return;
        }

        ScreenWidth = (float)camera.scaledPixelWidth / (float)camera.scaledPixelHeight * 10.0f;
        ScreenHeight = (float)camera.scaledPixelHeight / (float)camera.scaledPixelWidth;

    }

    void Update()
    {
        if (StopSpawning)
        {
            return;
        }

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
        bool spawnTop = Random.Range(0, 2) == 1;

        float xOffset = 0.0f;
        float yOffset = 0.0f;

        if (spawnTop)
        {
            yOffset = ScreenHeight * 2.5f;

        }
        else
        {
            yOffset = -ScreenHeight * 2.5f;
        }

        xOffset = Random.Range(-ScreenWidth, ScreenWidth);
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
