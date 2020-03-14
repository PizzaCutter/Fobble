using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private GameManager GameManager = null;
    private ProjectileSpawner ProjectileSpawner = null;
    private Rotator Rotator = null;

    [SerializeField]
    private Text scoreText = null;

    private int score = 0;

    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        ProjectileSpawner = GetComponent<ProjectileSpawner>();
        Rotator = GetComponent<Rotator>();
        SetScoreUI();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy == null)
        {
            return;
        }

        GameManager.GameOver();
    }
    
    public void Kill()
    {
        Rotator.StopRotation();
        ProjectileSpawner.enabled = false;
    }

    public void ResetPlayer()
    {
        Rotator.StartRotate();
        ProjectileSpawner.enabled = true;
        transform.rotation = Quaternion.identity;
        score = 0;
    }

    public void AddScore()
    {
        score++;
        SetScoreUI();
    }

    private void SetScoreUI()
    {
        scoreText.text = "SCORE: " + score;

    }
}
