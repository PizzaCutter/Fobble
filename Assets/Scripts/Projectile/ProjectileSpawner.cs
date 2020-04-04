using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public delegate void ProjectileCountChanged(int projectileCountChanged);
    public event ProjectileCountChanged ProjectileCountChangedEvent;

    [SerializeField] private Transform ProjectileSpawnTransform = null;

    [SerializeField] private GameObject ProjectileToSpawn = null;

    private ProjectileManager projectileManager;

    [SerializeField] private int ConcurrentProjectileAmount = 3;

    private int CurrentProjectileCount; 

    [SerializeField] private float ShotDelayTime = 0.1f;
    private float ShotDelayTimer = 0.0f;

    [SerializeField] private AudioClip SpawnProjectileSoundEffect = null;
    [SerializeField] private AudioClip AmmoReplentishedSoundEffect = null;
    
    private AudioSource AudioComponent = null;

    void Start()
    {
        projectileManager = FindObjectOfType<ProjectileManager>();
        CurrentProjectileCount = ConcurrentProjectileAmount;
        AudioComponent = GetComponent<AudioSource>();
    }

    void Update()
    {
        ShotDelayTimer += Time.deltaTime;
        if(Time.timeScale < 0.1f || CurrentProjectileCount <= 0 || ShotDelayTimer < ShotDelayTime)
        {
            return;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                SpawnProjectile();
                return;
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            SpawnProjectile();
            return;
        }
    }

    private void SpawnProjectile()
    {
        ShotDelayTimer = 0.0f;

        GameObject go = projectileManager.GetProjectileFromPool(ProjectileToSpawn);
        go.transform.position = ProjectileSpawnTransform.position;
        go.transform.rotation = ProjectileSpawnTransform.rotation;
        go.SetActive(true);
        go.GetComponent<Projectile>().ProjectileIsDestroyed += ProjectileDestroyed;
        CurrentProjectileCount--;
        ProjectileCountChangedEvent(CurrentProjectileCount);
    
        AudioComponent.clip = SpawnProjectileSoundEffect;
        AudioComponent.Play();
    }

    private void ProjectileDestroyed(Projectile projRef)
    {
        projRef.ProjectileIsDestroyed -= ProjectileDestroyed;
        CurrentProjectileCount++;
        
        ProjectileCountChangedEvent(CurrentProjectileCount);
    
        AudioComponent.clip = AmmoReplentishedSoundEffect;
        AudioComponent.Play();
    }
}
