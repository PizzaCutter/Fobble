using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve RotationSpeedCurve = null;

    private float timeSinceStart = 0.0f;
    private float currentRotationSpeed = 0.0f;

    private bool stopRotate = false;

    public void StartRotate()
    {
        stopRotate = false;
    }

    public void StopRotation()
    {
        stopRotate = true;
        timeSinceStart = 0.0f;
    }

    void Update()
    {
        if(stopRotate == false)
        {
            timeSinceStart += Time.deltaTime;
            currentRotationSpeed = RotationSpeedCurve.Evaluate(timeSinceStart);

            transform.Rotate(new Vector3(0.0f, 0.0f, currentRotationSpeed * Time.deltaTime));
        }
    }
}
