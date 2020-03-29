using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameIntro : MonoBehaviour
{
    [SerializeField]
    private AnimationClip DisableAnimation = null;
    [SerializeField]
    private AnimationClip EnableAnimation = null;

    private Animator GameStartAnimator = null;

    private void Start() {
        GameStartAnimator = GetComponent<Animator>();    
    }

    public void EnableUI()
    {
        GameStartAnimator.Play(EnableAnimation.name);
    }

    public void DisableUI()
    {
        GameStartAnimator.Play(DisableAnimation.name);
    }
}
