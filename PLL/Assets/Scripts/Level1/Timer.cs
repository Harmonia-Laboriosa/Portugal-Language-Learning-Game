using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] float totalRemainingTime = 300f; // Total time limit in seconds (5 minutes)
    [SerializeField] GameObject failedPanel;

    private Coroutine blinkCoroutine;

    void Update()
    {
        if (totalRemainingTime < 0)
        {
            // If time runs out, activate the failed panel
            totalRemainingTime = 0;
            failedPanel.SetActive(true);
            return;
        }

        // Update the total remaining time
        totalRemainingTime -= Time.deltaTime;

        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(totalRemainingTime / 60);
        int seconds = Mathf.FloorToInt(totalRemainingTime % 60);

        // Update the timer text
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Change text color to red and start blinking if time is less than 10 seconds
        if (totalRemainingTime < 10)
        {
            timerText.color = Color.red;
            if (blinkCoroutine == null)
            {
                blinkCoroutine = StartCoroutine(BlinkText());
            }
        }
        else
        {
            // Reset text color to white and stop blinking if time is greater than 10 seconds
            timerText.color = Color.white;
            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);
                timerText.enabled = true; // Ensure text is visible
                blinkCoroutine = null;
            }
        }
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            timerText.enabled = !timerText.enabled; // Toggle visibility of the text
            yield return new WaitForSeconds(0.5f); // Wait for half a second
        }
    }
}
