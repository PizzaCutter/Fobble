using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Animator Animator = null;
    [SerializeField]
    private AnimationClip IdleAnimation = null;
    [SerializeField]
    private AnimationClip DieAnimation = null;

    private Rigidbody2D rigidBody2D = null;
    private BoxCollider2D boxCollider2D = null;
    private MoveTowardsPlayer moveTowardsScript = null;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        moveTowardsScript = GetComponent<MoveTowardsPlayer>();
    }

    private void OnEnable()
    {
        if (rigidBody2D == null)
        {
            rigidBody2D = GetComponent<Rigidbody2D>();
        }
        if (boxCollider2D == null)
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
        }
        if(moveTowardsScript == null)
        {
            moveTowardsScript = GetComponent<MoveTowardsPlayer>();
        }

        boxCollider2D.enabled = true;
        moveTowardsScript.enabled = true;

        Animator.Play(IdleAnimation.name);
    }

    public void Kill()
    {
        Animator.Play(DieAnimation.name);

        boxCollider2D.enabled = false;
        moveTowardsScript.enabled = false;

        StartCoroutine(AnimationFinished());
    }

    IEnumerator AnimationFinished()
    {
        yield return new WaitForSeconds(DieAnimation.length);

        gameObject.SetActive(false);
    }
}
