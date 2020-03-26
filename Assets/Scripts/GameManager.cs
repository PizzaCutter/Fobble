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
    private Animator StartTextAnimator = null;

    [SerializeField]
    private Animator CounterAnimator = null;

    [SerializeField]
    private EnemySpawner EnemySpawner = null;

    [SerializeField]
    private Player Player = null;

    [SerializeField]
    private float GameOverTime = 1.0f;

    [SerializeField] private UIGameOver GameOverUI = null;


    void Start()
    {
        Time.timeScale = 0.0f;
        CounterAnimator.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1.0f;
        EnemySpawner.StopSpawning = false;

        OverlayButton.SetActive(false);

        StartTextAnimator.Play("FadeOut");
        GameOverUI.DisableUI();

        CounterAnimator.Play("FadeIn");
        CounterAnimator.gameObject.SetActive(true);
        
        Player.ResetPlayer();
    }

    public void GameOver()
    {
        CounterAnimator.Play("FadeOut");
        EnemySpawner.StopSpawning = true;
        EnemySpawner.KillAll();
        Player.Kill();

        OverlayButton.SetActive(true);
        StartTextAnimator.Play("FadeIn");
        GameOverUI.EnableUI();

        StartCoroutine(GameOverMenu());
    }

    IEnumerator GameOverMenu()
    {
        yield return new WaitForSeconds(GameOverTime);
       
        Time.timeScale = 0.0f;

    }
}
