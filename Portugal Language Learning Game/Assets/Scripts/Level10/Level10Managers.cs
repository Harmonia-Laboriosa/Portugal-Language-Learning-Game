using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Level10Managers : MonoBehaviour
{
    public GameObject[] questions;
    public GameObject EndgamePanel;
    public TMP_Text scoreText;
    public Button[] answerButtons; // Changed to an array of buttons
    private int currentQuestion;
    public GameObject Answerbuttons;
    int fromSlot;
    int iScore;

    void Start()
    {
        fromSlot = 0;
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
        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].SetActive(i == currentQuestion);
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

        if (currentQuestion + 1 < questions.Length)
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
        EnableAnswerButton();
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

        // Shake the selected button
        //StartCoroutine(ShakeButton(selectedButton.gameObject, 1f, 20f));

        // Disable all answer buttons after an answer is selected
        // DisableAnswerButtons();

        // Pause the game and show the pause panel
        StartCoroutine(DelayBeforeNextQuestion());
    }

    //IEnumerator ShakeButton(GameObject buttonObject, float duration, float magnitude)
    //{
    //    Vector3 originalPosition = buttonObject.transform.position;
    //    float elapsed = 0.0f;

    //    while (elapsed < duration)
    //    {
    //        float x = originalPosition.x + Random.Range(-1f, 1f) * magnitude;
    //        /*float y = originalPosition.y + Random.Range(-1f, 1f) * magnitude;*/

    //        buttonObject.transform.position = new Vector3(x, originalPosition.y, originalPosition.z);

    //        elapsed += Time.deltaTime;

    //        yield return null;
    //    }

    //    buttonObject.transform.position = originalPosition;
    //}

    void UpdateScoreText()
    {
        // Update the score text using ScoreManager
        scoreText.text = "Score: " + SManage.instance.GetScore().ToString();
    }

    IEnumerator DelayBeforeNextQuestion()
    {
        DisableAnswerButton();
        // Delay for a short time before moving to the next question
        yield return new WaitForSeconds(0.5f);

        NextQuestion();
    }

    public void OpenAnswerPanel()
    {
        StartCoroutine(OpenAnswerPanelWithDelay());
    }

    private IEnumerator OpenAnswerPanelWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Answerbuttons.SetActive(true);
    }

    void EnableAnswerButton()
    {
        foreach (Button button in answerButtons)
        {
            button.interactable = true;
        }
    }

    void DisableAnswerButton()
    {
        foreach (Button button in answerButtons)
        {
            button.interactable = false;
        }
    }

    public void Next(int temp)
    {
        
        fromSlot = fromSlot + temp;
        Debug.Log(fromSlot);
        if(fromSlot == 2)
        {
            // Update the score using ScoreManager
            StartCoroutine(DelayBeforeNextQuestion());

            fromSlot = 0;
        }
    }

    public void IncScore(int temp)
    {

        iScore = iScore + temp;
        Debug.Log(iScore);
        if (iScore == 2)
        {
            // Update the score using ScoreManager
            SManage.instance.IncreaseScore(0);
            iScore = 0;
        }
    }

}
