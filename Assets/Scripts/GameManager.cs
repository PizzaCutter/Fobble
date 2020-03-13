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

    void Start()
    {
        Time.timeScale = 0.0f;
        CounterAnimator.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1.0f;
        StartTextAnimator.Play("FadeOut");

        CounterAnimator.Play("FadeIn");
        CounterAnimator.gameObject.SetActive(true);

        Debug.Log("StartGame");
    }
}
