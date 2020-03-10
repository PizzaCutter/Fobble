using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable
public class Proto_Rotator : MonoBehaviour
{
    [SerializeField]
    private float RotationSpeed = 1.0f;

    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, RotationSpeed * Time.deltaTime));
    }
}
#pragma warning restore