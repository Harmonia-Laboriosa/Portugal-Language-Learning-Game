using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{
    public float rotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        //keyboard input
        float rotationInput = Input.GetAxis("Horizontal");
        RotateImage(rotationInput);
        /*
        // mobile input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float rotationInput = touch.deltaPosition.x;
            RotateImage(rotationInput);
        }
        */

    }

    void RotateImage(float rotationInput)
    {
        float rotationAngle = rotationInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, 0f, rotationAngle);
    }
}
