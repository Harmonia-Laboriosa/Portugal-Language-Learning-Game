using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Level3Manager : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject EndgamePanel;
    public TMP_Text scoreText;
    public Button[] answerButtons; // Changed to an array of buttons
    private int currentQuestion;
    public GameObject Answerbuttons;

    void Start()
    {
        StartQuiz();
        Answerbuttons.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartQuiz()
    {
        // Reset the score when starting the quiz
        SManage.instance.ResetScore();
        currentQuestion = 0;
        ActivateCurrentQuestion();
        //StartTimer();
    }

    void ActivateCurrentQuestion()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(i == currentQuestion);
        }

        // Enable all answer buttons for the current question
        foreach (Button button in answerButtons)
        {
            button.interactable = true;
            button.GetComponent<Image>().color = Color.white; // Reset button color
        }
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
            Answerbuttons.SetActive(false);
            Debug.Log("Quiz completed!");
            // Display end game panel
            EndgamePanel.SetActive(true);
        }
    }



    public void CorrectAnswer(int correctButtonIndex)
    {
        // Change the selected (incorrect) answer button color to green
        Button selectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        selectedButton.GetComponent<Image>().color = Color.green;

        // Update the score using ScoreManager
        SManage.instance.IncreaseScore(1);
        UpdateScoreText();

        // Disable all answer buttons after an answer is selected
        // DisableAnswerButtons();

        StartCoroutine(DelayBeforeNextQuestion());
    }

    public void IncorrectAnswer(int correctButtonIndex)
    {
        // Change the selected (incorrect) answer button color to red
        Button selectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        selectedButton.GetComponent<Image>().color = Color.red;

        // Disable all answer buttons after an answer is selected
        // DisableAnswerButtons();

        // Pause the game and show the pause panel
        StartCoroutine(DelayBeforeNextQuestion());
    }

    void UpdateScoreText()
    {
        // Update the score text using ScoreManager
        scoreText.text = "Score: " + SManage.instance.GetScore().ToString();
    }

    IEnumerator DelayBeforeNextQuestion()
    {
        // Delay for a short time before moving to the next question
        yield return new WaitForSeconds(0.5f);


        NextQuestion();
    }

    public void OpenAnswerPanel()
    {
        Answerbuttons.SetActive(true);
    }

}
