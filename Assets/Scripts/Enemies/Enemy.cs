using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Animator Animator = null;
    [SerializeField]
    private AnimationClip IdleAnimation = null;
    [SerializeField]
    private AnimationClip DieAnimation = null;

    private Rigidbody2D rigidBody2D = null;
    private Collider2D collider2D = null;
    private MoveTowardsPlayer moveTowardsScript = null;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        moveTowardsScript = GetComponent<MoveTowardsPlayer>();
    }

    private void OnEnable()
    {
        if (rigidBody2D == null)
        {
            rigidBody2D = GetComponent<Rigidbody2D>();
        }
        if (collider2D == null)
        {
            collider2D = GetComponent<Collider2D>();
        }
        if(moveTowardsScript == null)
        {
            moveTowardsScript = GetComponent<MoveTowardsPlayer>();
        }

        collider2D.enabled = true;
        moveTowardsScript.enabled = true;

        Animator.Play(IdleAnimation.name);
    }

    public void Kill()
    {
        Animator.Play(DieAnimation.name);

        collider2D.enabled = false;
        moveTowardsScript.enabled = false;

        if (this.gameObject.activeSelf)
        {
            StartCoroutine(AnimationFinished());
        }
    }

    IEnumerator AnimationFinished()
    {
        yield return new WaitForSeconds(DieAnimation.length);

        gameObject.SetActive(false);
    }
}
