using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform ProjectileSpawnTransform = null;

    [SerializeField]
    private GameObject ProjectileToSpawn = null;

    private ProjectileManager projectileManager;

    void Start()
    {
        projectileManager = FindObjectOfType<ProjectileManager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject go = projectileManager.GetProjectileFromPool(ProjectileToSpawn);
            go.transform.position = ProjectileSpawnTransform.position;
            go.transform.rotation = ProjectileSpawnTransform.rotation;
            go.SetActive(true);
        }
    }
}
