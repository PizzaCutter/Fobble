using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Background : MonoBehaviour
{
    void Awake()
    {
        Camera camera = Camera.main;
        if(camera == null)
        {
            return;
        }

        float widthDivHeight = (float)camera.scaledPixelWidth / (float)camera.scaledPixelHeight * 10.0f;
        float heightDivWidth = (float)camera.scaledPixelHeight / (float)camera.scaledPixelWidth;
        transform.localScale = new Vector3(widthDivHeight, heightDivWidth * widthDivHeight, 1);
    }
}
