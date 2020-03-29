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

    void Awake()
    {
        Animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();     
    }

    public void EnableUI()
    {
        newScoreText.text = player.GetScore().ToString();
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        Animator.Play(EnableAnimation.name);
    }

    public void DisableUI()
    {
        Animator.Play(DisableAnimation.name);
    }

}
