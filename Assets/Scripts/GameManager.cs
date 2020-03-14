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


    void Start()
    {
        Time.timeScale = 0.0f;
        CounterAnimator.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1.0f;

        OverlayButton.SetActive(false);

        StartTextAnimator.Play("FadeOut");

        CounterAnimator.Play("FadeIn");
        CounterAnimator.gameObject.SetActive(true);
        
        Player.ResetPlayer();
    }

    public void GameOver()
    {
        CounterAnimator.Play("FadeOut");
        EnemySpawner.KillAll();
        Player.Kill();

        OverlayButton.SetActive(true);
        StartTextAnimator.Play("FadeIn");
        StartCoroutine(GameOverMenu());
    }

    IEnumerator GameOverMenu()
    {
        yield return new WaitForSeconds(GameOverTime);
        Debug.Log("GAME OVER MENU");
       
        Time.timeScale = 0.0f;

    }
}
