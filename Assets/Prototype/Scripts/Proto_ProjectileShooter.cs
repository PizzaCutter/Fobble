using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable
public class Proto_ProjectileShooter : MonoBehaviour
{
    [SerializeField]
    private Transform Barrel;
    [SerializeField]
    private GameObject Projectile;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GameObject go = Instantiate(Projectile, Barrel.position, Barrel.rotation);
            go.transform.Rotate(new Vector3(0.0f, 0.0f, 90.0f));
        }
    }
}
#pragma warning restore
