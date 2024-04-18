using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Level7Manager : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject EndgamePanel;
    public GameObject FailedPanel;
    public GameObject victoryPanel;
    public TMP_Text scoreText;
    public Button[] answerButtons;
    private int currentQuestion;

    public GameObject part2;

    // List to keep track of correct answers for each question
    [SerializeField]
    private List<int> correctAnswerIndices = new List<int>();

    private bool gameEnded = false;

    void Start()
    {
        //StartQuiz();
        Debug.Log(levels.Length);
        part2.SetActive(false);
    }

    public void StartQuiz()
    {
        // Reset the score when starting the quiz
        SManage.instance.ResetScore();
        currentQuestion = 0;
        ActivateCurrentQuestion();
    }

    void ActivateCurrentQuestion()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(i == currentQuestion);
        }
        EnableAnswerButtons();
    }

    public void NextQuestion()
    {
        if (currentQuestion + 1 < levels.Length)
        {
            currentQuestion++;
            ActivateCurrentQuestion();
        }
        else
        {
            Debug.Log("Quiz completed!");
            // Display end game panel
            EndGameScore();
        }
        EnableAnswerButtons();
    }

    private void EndGameScore()
    {
        if (!gameEnded) // Check if the game has not ended yet
        {
            if (SManage.instance.score <= 2)
            {
                FailedPanel.SetActive(true);
            }
            else
            {
                if (SManage.instance.score >= 4)
                {
                    victoryPanel.SetActive(true);
                }
                else
                {
                    EndgamePanel.SetActive(true);
                }
            }

            gameEnded = true; // Set the flag to true to indicate that the game has ended
        }
    }
    public void CorrectAnswer(int correctButtonIndex)
    {
        // Change color of the correct answer button to green
        answerButtons[correctButtonIndex].image.color = Color.green;
        answerButtons[correctButtonIndex].interactable= false;

        // Add the selected correct answer index to the list
        correctAnswerIndices.Add(correctButtonIndex);

        // Check if all correct answers are selected
        if (correctAnswerIndices.Count == 4) // Assuming there are 5 correct answers
        {
            // Increase score only when all correct answers are selected
            SManage.instance.IncreaseScore(1);
            UpdateScoreText();
            StartCoroutine(DelayBeforeNextQuestion());
        }
    }

    public void IncorrectAnswer()
    {
        // Disable the clicked incorrect button
        int clickedButtonIndex = GetClickedButtonIndex();
        answerButtons[clickedButtonIndex].gameObject.SetActive(false);
        
    }

    int GetClickedButtonIndex()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (EventSystem.current.currentSelectedGameObject == answerButtons[i].gameObject)
            {
                return i;
            }
        }
        return -1;
    }

    IEnumerator DelayBeforeNextQuestion()
    {
        
        if(currentQuestion<5)
        {
            DisableAnswerButtons();
        }
        
        yield return new WaitForSeconds(1f);
        correctAnswerIndices.Clear();
        // Delay for a short time before moving to the next question
        
        // Disable all buttons during the delay
        NextQuestion();
    }

    void UpdateScoreText()
    {
        // Update the score text using ScoreManager
        scoreText.text = "Score: " + SManage.instance.GetScore().ToString();
    }

    void DisableAnswerButtons()
    {
        foreach (Button button in answerButtons)
        {
            button.interactable = false;
        }
    }

    void EnableAnswerButtons()
    {
        foreach (Button button in answerButtons)
        {
            button.interactable = true;
        }
    }

    public void CorrectAnswer2()
    {
            SManage.instance.IncreaseScore(1);
            UpdateScoreText();
            StartCoroutine(DelayBeforeNextQuestion());
    }

    public void IncorrectAnswer2()
    {
        StartCoroutine(DelayBeforeNextQuestion());
    }
}
