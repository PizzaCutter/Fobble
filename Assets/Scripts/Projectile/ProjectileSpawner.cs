using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public delegate void ProjectileCountChanged(int projectileCountChanged);
    public event ProjectileCountChanged ProjectileCountChangedEvent;

    [SerializeField]
    private Transform ProjectileSpawnTransform = null;

    [SerializeField]
    private GameObject ProjectileToSpawn = null;

    private ProjectileManager projectileManager;

    [SerializeField]
    private int ConcurrentProjectileAmount = 3;

    private int CurrentProjectileCount; 

    void Start()
    {
        projectileManager = FindObjectOfType<ProjectileManager>();
        CurrentProjectileCount = ConcurrentProjectileAmount;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && Time.timeScale > 0.1f && CurrentProjectileCount > 0)
        {
            GameObject go = projectileManager.GetProjectileFromPool(ProjectileToSpawn);
            go.transform.position = ProjectileSpawnTransform.position;
            go.transform.rotation = ProjectileSpawnTransform.rotation;
            go.SetActive(true);
            go.GetComponent<Projectile>().ProjectileIsDestroyed += ProjectileDestroyed;
            CurrentProjectileCount--;
            ProjectileCountChangedEvent(CurrentProjectileCount);
        }
    }

    private void ProjectileDestroyed(Projectile projRef)
    {
        projRef.ProjectileIsDestroyed -= ProjectileDestroyed;
        CurrentProjectileCount++;
        ProjectileCountChangedEvent(CurrentProjectileCount);
    }
}
