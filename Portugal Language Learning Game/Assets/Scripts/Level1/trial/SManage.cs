using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SManage : MonoBehaviour
{
    public int score = 0; // Current score
    public TMP_Text scoreText; // Reference to the UI text displaying the score

    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreText(); // Update the score text on start
    }

    // Method to increase the score by a specified amount
    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText(); // Update the score text after increasing the score
    }

    // Method to decrease the score by a specified amount
    public void DecreaseScore(int amount)
    {
        score -= amount;
        // Ensure the score doesn't go below zero
        score = Mathf.Max(score, 0);
        UpdateScoreText(); // Update the score text after decreasing the score
    }

    // Method to update the score text UI
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString(); // Update the text to display the current score
        }
    }
}
