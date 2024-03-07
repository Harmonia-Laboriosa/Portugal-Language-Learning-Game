using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{
    public float rotationSpeed = 100f;
    [SerializeField]public float snapAngle;
    private float currentRotation = 0f;
    // Update is called once per frame
    void Update()
    {
        //keyboard input
        float rotationInput = Input.GetAxis("Horizontal");
        RotateImage(rotationInput*rotationSpeed*Time.deltaTime);
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
        currentRotation += rotationAmount;

        float targetAngle = Mathf.Round(currentRotation / snapAngle) * snapAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
    }
}
