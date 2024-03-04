using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Level5manager : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject EndgamePanel;
    public TMP_Text scoreText;
    public Button[] answerButtons; // Changed to an array of buttons
    private int currentQuestion;
    [HideInInspector] public bool tagFromCollissionHour;
    [HideInInspector] public bool tagFromCollissionMinute;

    void Start()
    {
        StartQuiz();

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
            EndgamePanel.SetActive(true);
        }
    }



    public void CorrectAnswer(int correctButtonIndex)
    {
        //Destroy(shatter);
        SManage.instance.IncreaseScore(1);
        UpdateScoreText();
        // Shatter all answer buttons after an answer is selected
        StartCoroutine(DelayBeforeNextQuestion());
    }

    public void IncorrectAnswer(int correctButtonIndex)
    {
        Button selectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        // Shake the selected button
        //StartCoroutine(ShakeButton(selectedButton.gameObject, 1f, 20f));

        // Disable all answer buttons after an answer is selected
        // DisableAnswerButtons();

        // Pause the game and show the pause panel
        StartCoroutine(DelayBeforeNextQuestion());
    }

    IEnumerator ShakeButton(GameObject buttonObject, float duration, float magnitude)
    {
        Vector3 originalPosition = buttonObject.transform.position;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = originalPosition.x + Random.Range(-1f, 1f) * magnitude;
            /*float y = originalPosition.y + Random.Range(-1f, 1f) * magnitude;*/

            buttonObject.transform.position = new Vector3(x, originalPosition.y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        buttonObject.transform.position = originalPosition;
    }


    void UpdateScoreText()
    {
        // Update the score text using ScoreManager
        scoreText.text = "Score: " + SManage.instance.GetScore().ToString();
    }

    IEnumerator DelayBeforeNextQuestion()
    {
        // Delay for a short time before moving to the next question
        yield return new WaitForSeconds(1f);


        NextQuestion();
    }


    //logic of Level 4 part

    public void CheckAnswwer()
    {
        Debug.Log(tagFromCollissionHour);
        Debug.Log(tagFromCollissionMinute);
        if (tagFromCollissionHour && tagFromCollissionMinute)
        {
            CorrectAnswerPart1();
            //tagFromCollissionHour = false;
            //tagFromCollissionMinute = false;
        }
        else
        {
            IncorrectAnswerPart1();
            //tagFromCollissionHour = false;
            //tagFromCollissionMinute=false;
        }
    }
    public void CorrectAnswerPart1()
    {
        SManage.instance.IncreaseScore(1);
        UpdateScoreText();
        StartCoroutine(DelayBeforeNextQuestion());
    }

    public void IncorrectAnswerPart1()
    {
        StartCoroutine(DelayBeforeNextQuestion());
    }


}