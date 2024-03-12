using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{
    public float rotationSpeed = 100f;
    [SerializeField]public float snapAngle;
    private float currentRotation = 0f;
    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if the space bar is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            // Rotate the wheel
            RotateImage(rotationSpeed*Time.deltaTime);
        }
        //keyboard input
        //float rotationInput = Input.GetAxis("Horizontal");
        //RotateImage(rotationInput*rotationSpeed*Time.deltaTime);
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

    void RotateImage(float rotationAmount)
    {
        //currentRotation += rotationAmount;
        //float smoothRotation = Mathf.LerpAngle(transform.eulerAngles.z, currentRotation, Time.deltaTime * rotationSpeed);
        //float targetAngle = Mathf.Round(smoothRotation / snapAngle) * snapAngle;
        //transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
        transform.Rotate(0f, 0f, rotationAmount);
    }
}
