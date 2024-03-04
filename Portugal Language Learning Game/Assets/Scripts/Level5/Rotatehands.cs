using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatehands : MonoBehaviour
{
    public float rotationSpeed = 100f;

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
        RotateHand(rotationInput);
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
    void RotateHand(float rotationInput)
    {
        float rotationAngle = rotationInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, 0f, rotationAngle);
    }

    // Method to switch the selected clock hand
    public void SwitchHand()
    {
        if (selectedHand == ClockHand.Hour)
            selectedHand = ClockHand.Minute;
        else
            selectedHand = ClockHand.Hour;
    }
}
