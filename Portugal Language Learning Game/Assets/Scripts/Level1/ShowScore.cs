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
    public int DialogueLevelScore;
    public GameObject ScoreOutOf;


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
            if (DialogueLevelScore > 0)
            {
                if (showScore == DialogueLevelScore)
                {
                    if (ScoreOutOf != null)
                        ScoreOutOf.SetActive(false);
                    scoreText.text = "Wrong Converstation";
                }
                else
                {
                    scoreText.text = "Score: " + showScore.ToString(); // Update the text to display the current score
                }

            }
            else
            {
                scoreText.text = "Score: " + showScore.ToString(); // Update the text to display the current score
            }

        }
        //scoreText.text = "Score: " + showScore.ToString();
    }
}
