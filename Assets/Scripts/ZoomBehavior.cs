using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomBehavior : MonoBehaviour
{
    Vector3 scrollDirection = new Vector3(0, -1, 1);
    float sensitivity = 10f;

    void Update()
    {
        // Move
        float xAxisValue = Input.GetAxis("Horizontal");
        float zAxisValue = Input.GetAxis("Vertical");
        this.transform.position += new Vector3(xAxisValue, 0.0f, zAxisValue);

        // Zoom
        this.transform.position += (scrollDirection * Input.GetAxis("Mouse ScrollWheel") * sensitivity);
    }
}
