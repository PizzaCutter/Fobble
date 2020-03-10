using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable
public class Proto_projectile : MonoBehaviour
{
    [SerializeField]
    private float ProjectileSpeed = 100.0f;
    [SerializeField]
    private float ProjectileSize = 0.2f;

    // Update is called once per frame
    void Update()
    {
        
        transform.position += transform.right * (ProjectileSpeed * Time.deltaTime);

        if(Mathf.Abs(transform.position.x) >= 5.0f || Mathf.Abs(transform.position.y) >= 5.0f)
        {
            Destroy(this.gameObject);
        }

        Proto_MoveToPlayer[] enemies = FindObjectsOfType<Proto_MoveToPlayer>();
        for (int i = 0; i < enemies.Length; i++)
        {
            Vector3 distanceBetween = enemies[i].transform.position - transform.position;
            float distanceForCollision = enemies[i].EnemySize + ProjectileSize;
            if(distanceBetween.magnitude <= distanceForCollision)
            {
                Destroy(enemies[i].gameObject);
                Destroy(this.gameObject);
                break;
            }
        }
    }
}
#pragma warning restore