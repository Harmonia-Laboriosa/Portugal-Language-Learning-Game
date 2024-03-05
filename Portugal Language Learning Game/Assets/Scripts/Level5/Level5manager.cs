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

    private int correctObjectCount; // Variable to track the number of correct objects placed
    private bool isLastQuestion; // Flag to check if it's the last question

    public bool[] allObjectsPlaced;
    public bool[] scoreIncreased;


    void Start()
    {
        StartQuiz();

    }

    // Update is called once per frame
    void Update()
    {
        if (isLastQuestion)
        {
            for (int i = 0; i < levels.Length; i++)
            {
                if (i < levels.Length - 1)
                {
                   // CheckAllObjectsPlacedInPanel(questionPanels[i], i);
                }
                else
                {
                    CheckAllObjectsinLsstPlacedInPanel(levels[i], i);
                }

            }
        }
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


    //logic of Level 5 part

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

    public void CheckAllObjectsinLsstPlacedInPanel(GameObject panel, int panelIndex)
    {
        bool allPlaced = true;
        bool allCorrect = true; // Track if all placed objects are correct
        correctObjectCount = 0; // Reset correct object count for the panel

        foreach (Transform slot in panel.transform)
        {
            DragDropLevel5 dragDrop = slot.GetComponentInChildren<DragDropLevel5>();
            if (dragDrop != null && dragDrop.isDraggable)
            {
                allPlaced = false;
                break;
            }

            if (dragDrop != null)
            {
                if (dragDrop.isPlaceCorrect)
                {
                    correctObjectCount++;
                }
                else
                {
                    allCorrect = false;
                }
            }
        }

        allObjectsPlaced[panelIndex] = allPlaced;

        if (allPlaced && !scoreIncreased[panelIndex])
        {
            // Increase score if all objects are placed and the score has not been increased for this panel yet
            foreach (Transform slot in panel.transform)
            {
                DragDropLevel5 dragDrop = slot.GetComponentInChildren<DragDropLevel5>();
                if (dragDrop != null && dragDrop.isPlaceCorrect)
                {
                    SManage.instance.IncreaseScore(1);
                }
            }

            scoreIncreased[panelIndex] = true;
        }

        if (allPlaced && allCorrect)
        {
            // Activate next panel if all objects are placed and all are correct for the last question
            Debug.Log("All");
            EndgamePanel.SetActive(true);
        }
        else if (allPlaced && !allCorrect)
        {
            // Reset all objects to their original position if not all correct for the last question
            ResetObjects(panel);
        }
    }

    private void ResetObjects(GameObject panel)
    {
        foreach (Transform slot in panel.transform)
        {
            DragDropLevel5 dragDrop = slot.GetComponentInChildren<DragDropLevel5>();
            if (dragDrop != null)
            {
                dragDrop.ResetToOriginalPosition();
            }
        }
        CheckAllObjectsinLsstPlacedInPanel(levels[levels.Length - 1], levels.Length - 1);
    }

}
