using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable
public class Proto_MoveToPlayer : MonoBehaviour
{
    public float speed;

    public float EnemySize = 0.2f;

    Vector3 direction;
    
    void Start()
    {
        Proto_Rotator player = FindObjectOfType<Proto_Rotator>();    
        if(player != null)
        {
            direction = player.transform.position - transform.position;
            direction.Normalize();
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
            //transform.Rotate(new Vector3(0.0f, 0.0f, 35.0f));
        }
    }

    void Update()
    {
        transform.position += direction * (speed * Time.deltaTime);
        if(Mathf.Abs(transform.position.x) <= EnemySize && Mathf.Abs(transform.position.y) <= EnemySize)
        {
            Application.LoadLevel("SampleScene");
        }
    }
}
#pragma warning restore
