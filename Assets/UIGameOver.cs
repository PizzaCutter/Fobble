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

    void Awake()
    {
        player = FindObjectOfType<Player>();
        
    }

    public void EnableUI()
    {
        newScoreText.text = player.GetScore().ToString();
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        this.gameObject.SetActive(true);
    }

    public void DisableUI()
    {
        this.gameObject.SetActive(false);
    }

}
