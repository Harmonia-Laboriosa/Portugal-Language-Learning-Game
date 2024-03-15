using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatehands : MonoBehaviour
{
    public float rotationSpeed = 100f;
    [SerializeField] public float snapAngle;
    private float currentRotation = 0f;

    // Enum to define different clock hands
    public enum ClockHand
    {
        Hour,
        Minute
    }

    // Current selected clock hand
    public ClockHand selectedHand = ClockHand.Hour;

    // Update is called once per frame
    void Update()
    {
        // Check which hand is selected
        switch (selectedHand)
        {
            case ClockHand.Hour:
                RotateSelectedHand("Horizontal");
                break;
            case ClockHand.Minute:
                RotateSelectedHand("Vertical");
                break;
        }
    }

    // Rotate the selected clock hand based on input axis
    void RotateSelectedHand(string inputAxis)
    {
        // Keyboard input
        float rotationInput = Input.GetAxis(inputAxis);
        RotateHand(rotationInput * rotationSpeed * Time.deltaTime);
        /*
        // Mobile input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float rotationInput = touch.deltaPosition.x;
            RotateHand(rotationInput);
        }
        */
    }

    // Rotate the clock hand based on input
    public void RotateHand(float rotationAmount)
    {

        currentRotation += rotationAmount;

        float targetAngle = Mathf.Round(currentRotation / snapAngle) * snapAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, -targetAngle);
        
    }
    /*
     * public void RotateHand(float rotationAmount)
{
    currentRotation += rotationAmount * rotationSpeed * Time.deltaTime;

    // Calculate the target angle using modulo to ensure it stays within 360 degrees
    float targetAngle = currentRotation % 360f;

    // Apply smooth rotation using Quaternion.Lerp
    Quaternion targetRotation = Quaternion.Euler(0f, 0f, -targetAngle);
    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
}
     */

    // Method to switch the selected clock hand
    public void SwitchHand()
    {
        if (selectedHand == ClockHand.Hour)
            selectedHand = ClockHand.Minute;
        else
            selectedHand = ClockHand.Hour;
    }
}
