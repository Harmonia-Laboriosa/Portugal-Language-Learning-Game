using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] float totalRemainingTime = 300f; // Total time limit in seconds (5 minutes)
    [SerializeField] GameObject failedPanel;

    void Update()
    {
        if (totalRemainingTime < 0)
        {
            // If time runs out, activate the failed panel
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
    }
}
