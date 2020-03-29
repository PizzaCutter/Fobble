using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    private Player player = null;

    [SerializeField]
    private Text newScoreText = null;
    [SerializeField]
    private Text highScoreText = null;

    [SerializeField] 
    private AnimationClip EnableAnimation = null;

    [SerializeField]
    private AnimationClip DisableAnimation = null;

    private Animator Animator = null;

    [SerializeField]
    private float ScoreLerpTime = 2.0f;
    private float scoreTimer = 0.0f;

    void Awake()
    {
        Animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();     
    }

    private void Update() {
        
        if(scoreTimer <= ScoreLerpTime)
        {
            scoreTimer += Time.unscaledDeltaTime;
            Debug.Log(scoreTimer / ScoreLerpTime);
            int newLerpScore =  Mathf.CeilToInt(Mathf.Lerp(0.0f, player.GetScore(), scoreTimer / ScoreLerpTime));
            newScoreText.text = newLerpScore.ToString();

            if(newLerpScore > player.GetPrevHighScore())
            {
                highScoreText.text = newLerpScore.ToString();
            }
        }
    }

    public void EnableUI()
    {
        scoreTimer = 0.0f;
        newScoreText.text = player.GetScore().ToString();
        highScoreText.text = player.GetPrevHighScore().ToString();
        Animator.Play(EnableAnimation.name);
    }

    public void DisableUI()
    {
        Animator.Play(DisableAnimation.name);
    }

}
