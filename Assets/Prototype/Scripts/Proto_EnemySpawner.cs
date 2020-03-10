using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable

public class Proto_EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyObject;

    [SerializeField]
    private float SpawnRadius = 5;

    [SerializeField]
    private float SpawnTime = 0.5f;
    private float SpawnTimer = 0.0f;


    void Update()
    {
        if(SpawnTimer <= 0.0f)
        {
            float randomAngle = Random.Range(0.0f, 360.0f);
            SpawnTimer = SpawnTime;

            Vector3 location = new Vector3(Mathf.Sin(randomAngle) * SpawnRadius, Mathf.Cos(randomAngle) * SpawnRadius);
            Instantiate(EnemyObject, location, Quaternion.identity);
        }

        SpawnTimer -= Time.deltaTime;
    }
}
#pragma warning restore