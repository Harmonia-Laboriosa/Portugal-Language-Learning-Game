using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ShowScore : MonoBehaviour
{
    public SManage scoreShow;
    public TMP_Text scoreText; // Reference to the UI text displaying the score 
    public int showScore;


    // Update is called once per frame
    void Update()
    {
        showScore = scoreShow.GetScore();
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + showScore.ToString(); // Update the text to display the current score
        }
    }
}
