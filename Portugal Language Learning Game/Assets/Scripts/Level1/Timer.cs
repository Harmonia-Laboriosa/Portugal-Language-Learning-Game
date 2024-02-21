using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] float totalRemainingTime = 360f; // Total time limit in seconds (6 minutes)
    [SerializeField] float timeInterval = 30f; // Time interval to change question (30 seconds)
    float remainingTime; // Current remaining time
    int currentQuestionIndex = 0; // Index of the current question
    public GameObject[] questionPanels; // Array to store references to all question panels

    void Start()
    {
        remainingTime = totalRemainingTime;
        StartCoroutine(UpdateTime());
        StartCoroutine(ChangeQuestion());
    }

    IEnumerator UpdateTime()
    {
        while (remainingTime > 0)
        {
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
            UpdateTimerText();

            if (remainingTime <= 0)
            {
                // Handle end of time here
                Debug.Log("Time's up!");
            }
        }
    }

    IEnumerator ChangeQuestion()
    {
        while (remainingTime > 0)
        {
            yield return new WaitForSeconds(timeInterval);
            ChangeToNextQuestion();
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void ChangeToNextQuestion()
    {
        // Deactivate the current question panel
        if (currentQuestionIndex < questionPanels.Length)
        {
            questionPanels[currentQuestionIndex].SetActive(false);
        }

        // Increment the question index
        currentQuestionIndex++;

        // Activate the next question panel
        if (currentQuestionIndex < questionPanels.Length)
        {
            questionPanels[currentQuestionIndex].SetActive(true);
        }
        else
        {
            // Handle end of questions here
            Debug.Log("No more questions!");
        }
    }
}
