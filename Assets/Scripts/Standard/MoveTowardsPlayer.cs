using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public float Speed;

    private Player PlayerReference = null;
    private Rigidbody2D Rigidbody = null;
    private Vector3 direction = new Vector3();

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        PlayerReference = FindObjectOfType<Player>();
        if (PlayerReference == null)
        {
            Debug.LogError("Could not find player");
        }

        CalcDir();
    }

    void OnEnable()
    {
        if(PlayerReference != null)
        {
            CalcDir();
        }
    }

    void CalcDir()
    {
        direction = PlayerReference.transform.position - transform.position;
        direction.Normalize();
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    }

    void Update()
    {
        Vector2 newPosition = transform.position + direction * (Speed * Time.deltaTime);
        transform.position = newPosition;
    }
}
