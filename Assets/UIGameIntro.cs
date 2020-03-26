using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameIntro : MonoBehaviour
{
    [SerializeField] 
    private Text TextTitle = null;

    [SerializeField]
    private Text TextStartGame = null;

    [SerializeField]
    private Animator StartTextAnimator = null;

    public void EnableUI()
    {
        TextTitle.gameObject.SetActive(true);
        StartTextAnimator.Play("FadeIn");
    }

    public void DisableUI()
    {
        TextTitle.gameObject.SetActive(false);
        StartTextAnimator.Play("FadeOut");
    }
}
