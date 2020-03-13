using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float MovementSpeed = 10.0f;


    //physics update
    void FixedUpdate()
    {
        transform.position += transform.up * MovementSpeed * Time.deltaTime;        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy == null)
        {
            return;
        }
        enemy.Kill();
       
        gameObject.SetActive(false);
    }
}
