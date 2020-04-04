using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    private Player player = null;

    [SerializeField] private Text newScoreText = null;
    [SerializeField] private Text highScoreTitleText = null;
    [SerializeField] private Text highScoreText = null;
    [SerializeField] private Color NewHighScoreColor = new Color();
    [SerializeField] private AnimationClip EnableAnimation = null;
    [SerializeField] private AnimationClip DisableAnimation = null;
    [SerializeField] private AnimationClip CurrentScoreNumberChanged = null;
    [SerializeField] private AnimationClip ScoreNumbersChanged = null;
    [SerializeField] private float ScoreLerpTime = 2.0f;
    [SerializeField] private ParticleSystem NewHighScoreParticlesystem = null;

    [SerializeField] private AudioClip SFX_ScoreTick = null;

    [SerializeField] private AudioClip SFX_NewHighScore = null;

    private AudioSource AudioSourceComponent = null;

    private float scoreTimer = 0.0f;
    private int prevLerpedScore = -1;
    private bool reachedNewHighscore = false;
    private Animator Animator = null;

    void Awake()
    {
        Animator = GetComponent<Animator>();
        AudioSourceComponent = GetComponent<AudioSource>();
        player = FindObjectOfType<Player>();     
        scoreTimer = ScoreLerpTime;
    }

    private void Update()
    {
        if(scoreTimer < ScoreLerpTime)
        {
            scoreTimer += Time.deltaTime;
            int newLerpScore =  Mathf.CeilToInt(Mathf.Lerp(0.0f, player.GetScore(), scoreTimer / ScoreLerpTime));
            newScoreText.text = newLerpScore.ToString();

            if(newLerpScore > player.GetPrevHighScore())
            {
                highScoreTitleText.color = NewHighScoreColor;
                highScoreText.color = NewHighScoreColor;
                highScoreText.text = newLerpScore.ToString();
            }

            if(prevLerpedScore != newLerpScore && newLerpScore > player.GetPrevHighScore())
            {
                Animator.Play(ScoreNumbersChanged.name,0,0);
                AudioSourceComponent.Play();
                reachedNewHighscore = true;
            }else if(prevLerpedScore != newLerpScore)
            {
                Animator.Play(CurrentScoreNumberChanged.name,0,0);
                AudioSourceComponent.Play();
            }

            prevLerpedScore = newLerpScore;
        }else if(scoreTimer >= ScoreLerpTime && reachedNewHighscore == true)
        {
            reachedNewHighscore = false;
            NewHighScoreParticlesystem.gameObject.SetActive(true);
            NewHighScoreParticlesystem.Play();

            AudioSourceComponent.clip = SFX_NewHighScore;
            AudioSourceComponent.Play();
        }
    }

    public void EnableUI()
    {
        scoreTimer = 0.0f;
        prevLerpedScore = 0;

        newScoreText.text = player.GetScore().ToString();
        highScoreText.text = player.GetPrevHighScore().ToString();
        
        highScoreTitleText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        highScoreText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        Animator.Play(EnableAnimation.name);

        AudioSourceComponent.clip = SFX_ScoreTick;
    }

    public void DisableUI()
    {
        if(prevLerpedScore == -1)
        {
            //otherwise will trigger animation when starting for first time
            return;
        }

        Animator.Play(DisableAnimation.name);
        NewHighScoreParticlesystem.gameObject.SetActive(false);
    }

}
