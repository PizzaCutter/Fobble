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


    void Start()
    {
        Time.timeScale = 0.0f;
    }

    public void StartGame()
    {
        Time.timeScale = 1.0f;
        EnemySpawner.StopSpawning = false;

        OverlayButton.SetActive(false);

        GameIntroUI.DisableUI();
        GameOverUI.DisableUI();

        
        Player.ResetPlayer();
    }

    public void GameOver()
    {
        EnemySpawner.StopSpawning = true;
        EnemySpawner.KillAll();
        Player.Kill();

        OverlayButton.SetActive(true);
        GameIntroUI.EnableUI();
        GameOverUI.EnableUI();

        StartCoroutine(GameOverMenu());
    }

    IEnumerator GameOverMenu()
    {
        yield return new WaitForSeconds(GameOverTime);
       
        Time.timeScale = 0.0f;

    }
}
