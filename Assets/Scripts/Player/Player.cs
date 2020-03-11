using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy == null)
        {
            return;
        }
        
        enemy.gameObject.SetActive(false);

        //Restart Game
        //Application.LoadLevel(0);
    }
}
