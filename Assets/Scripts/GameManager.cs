using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

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

    [SerializeField] private int PlayAdInterval = 3;
    [SerializeField] private int InitialAdInterval = 3;
    int gamesPlayed = 0;

    string gameId = "3544699";
    bool testMode = false;

    bool shouldPlayAd = false;
    bool playedAd = false;

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);

        AudioSourceComponent = GetComponent<AudioSource>();
        Time.timeScale = 0.0f;
    }

    public void StartGame()
    {
        StopCoroutine(GameOverMenu());
        CheckAdRun();

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
        gamesPlayed = PlayerPrefs.GetInt("GamesPlayed");
        gamesPlayed++;
        PlayerPrefs.SetInt("GamesPlayed", gamesPlayed);

        if(gamesPlayed == InitialAdInterval)
        {
            shouldPlayAd = true;
            playedAd = false;
        }else if(gamesPlayed % PlayAdInterval == 0)
        {
            shouldPlayAd = true;
            playedAd = false;
        }

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
       
        CheckAdRun();
    }

    private void CheckAdRun()
    {
        if(shouldPlayAd && playedAd == false)
        {
            shouldPlayAd = false;
            playedAd = true;
            Advertisement.Show();
        }
    }
}
