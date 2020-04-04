using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public delegate void ProjectileDestroyed(Projectile projRef);
    public event ProjectileDestroyed ProjectileIsDestroyed;

    public float MovementSpeed = 10.0f;

    private float ScreenWidth = 0.0f;
    private float ScreenHeight = 0.0f;
    private bool Destroyed = false;

    private Player player = null;

    private Collider2D AttachedCollider2D = null;
    [SerializeField] 
    private GameObject visualsChildObject = null;

    [SerializeField]
    private TrailRenderer trailRenderer = null;

    [SerializeField] 
    private ParticleSystem destroyedParticleSystem = null;

    private ParticleSystem CachedDestroyedParticleSystem = null;

    [SerializeField] private AudioClip HitWallAudioEffect = null;
    [SerializeField] private AudioClip HitEnemyAudioEffect = null;

    private AudioSource AudioComponent = null;

    void Awake()
    {
        Camera camera = Camera.main;
        if (camera == null)
        {
            return;
        }

        ScreenWidth = (float)camera.scaledPixelWidth / (float)camera.scaledPixelHeight * 10.0f;
        ScreenHeight = (float)camera.scaledPixelHeight / (float)camera.scaledPixelWidth;

        AudioComponent = GetComponent<AudioSource>();
    }

    private void Start()
    {
        AttachedCollider2D = GetComponent<Collider2D>();
        player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        Destroyed = false;

        if (AttachedCollider2D != null)
        {
            AttachedCollider2D.enabled = true;
        }

        trailRenderer.Clear();
        visualsChildObject.SetActive(true);
    }
    
    void FixedUpdate()
    {
        if(Destroyed == true)
        {
            return;
        }

        transform.position += transform.up * MovementSpeed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x) > ScreenWidth / 2.0f)
        {
            AudioComponent.clip = HitWallAudioEffect;
            AudioComponent.Play();
            DestroyProjectile();
        }
        else if (Mathf.Abs(transform.position.y) > (ScreenHeight * ScreenWidth * 0.5f))
        {
            AudioComponent.clip = HitWallAudioEffect;
            AudioComponent.Play();
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy == null)
        {
            return;
        }
        enemy.Kill();
        player.AddScore();

        AudioComponent.clip = HitEnemyAudioEffect;
        AudioComponent.Play();

        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        Destroyed = true;

        AttachedCollider2D.enabled = false;
        visualsChildObject.SetActive(false);

        if (CachedDestroyedParticleSystem == null)
        {
            CachedDestroyedParticleSystem = Instantiate(destroyedParticleSystem, transform.position, Quaternion.identity, transform.parent);
        }
        else
        {
            CachedDestroyedParticleSystem.transform.position = transform.position;
            CachedDestroyedParticleSystem.gameObject.SetActive(true);
            CachedDestroyedParticleSystem.Play();
        }

        ProjectileIsDestroyed(this);
        StartCoroutine(DisableGameObjectAfterParticleEffect());
    }

    IEnumerator DisableGameObjectAfterParticleEffect()
    {
        float waitTime = CachedDestroyedParticleSystem.main.duration > AudioComponent.clip.length ? CachedDestroyedParticleSystem.main.duration : AudioComponent.clip.length;
        yield return new WaitForSeconds(CachedDestroyedParticleSystem.main.duration);
        CachedDestroyedParticleSystem.gameObject.SetActive(false);
        gameObject.SetActive(false);
        AudioComponent.clip = null;
    }

}
