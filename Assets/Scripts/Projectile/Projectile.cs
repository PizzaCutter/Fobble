using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float MovementSpeed = 10.0f;
    private Player player = null;

    private float ScreenWidth = 0.0f;
    float ScreenHeight = 0.0f;

    [SerializeField] 
    private ParticleSystem destroyedParticleSystem = null;

    [SerializeField]
    private TrailRenderer trailRenderer = null;

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

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        trailRenderer.Clear();
    }

    private void OnDisable()
    {
    }

    void FixedUpdate()
    {
        transform.position += transform.up * MovementSpeed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x) > ScreenWidth / 2.0f)
        {
            Instantiate(destroyedParticleSystem, transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
        }
        else if (Mathf.Abs(transform.position.y) > ScreenHeight * 2.25f)
        {
            SpawnDestroyedParticleSystem();
            this.gameObject.SetActive(false);
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
        SpawnDestroyedParticleSystem();

        gameObject.SetActive(false);
        player.AddScore();
    }

    private void SpawnDestroyedParticleSystem()
    {
        ParticleSystem go = Instantiate(destroyedParticleSystem, transform.position, Quaternion.identity);
        Destroy(go, destroyedParticleSystem.main.duration);
    }
}
