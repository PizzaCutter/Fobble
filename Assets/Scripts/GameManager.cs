using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
        MENU,
        PLAYING
    }

    [SerializeField]
    private GameObject OverlayButton = null;

    [SerializeField]
    private EnemySpawner EnemySpawner = null;

    [SerializeField]
    private Player Player = null;

    [SerializeField]
    private float GameOverTime = 1.0f;

    [SerializeField] private UIGameOver GameOverUI = null;
    [SerializeField] private UIGameIntro GameIntroUI = null;

    [SerializeField] private AudioClip SFX_StartGame = null;
    [SerializeField] private AudioClip SFX_GameOver = null;
    private AudioSource AudioSourceComponent = null;

    void Start()
    {
        AudioSourceComponent = GetComponent<AudioSource>();
        Time.timeScale = 0.0f;
    }

    public void StartGame()
    {
        StopCoroutine(GameOverMenu());

        Time.timeScale = 1.0f;
        EnemySpawner.StopSpawning = false;

        OverlayButton.SetActive(false);

        GameIntroUI.DisableUI();
        GameOverUI.DisableUI();

        
        Player.ResetPlayer();

        AudioSourceComponent.clip = SFX_StartGame;
        AudioSourceComponent.Play();
    }

    public void GameOver()
    {
        EnemySpawner.StopSpawning = true;
        EnemySpawner.KillAll();
        Player.Kill();

        OverlayButton.SetActive(true);
        
        GameIntroUI.EnableUI();
        GameOverUI.EnableUI();

        AudioSourceComponent.clip = SFX_GameOver;
        AudioSourceComponent.Play();
        
        StartCoroutine(GameOverMenu());
    }

    IEnumerator GameOverMenu()
    {
        yield return new WaitForSeconds(GameOverTime);
       
        //Time.timeScale = 0.0f;

    }
}
