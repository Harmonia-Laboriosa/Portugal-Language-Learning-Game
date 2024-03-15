using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Level8Manager : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject EndgamePanel;
    public GameObject FailedPanel;
    public TMP_Text scoreText;
    public Button[] answerButtons; // Changed to an array of buttons
    public Button[] checkButtons; // Changed to an array of buttons
    private int currentQuestion;
    private int correctObjectCount; // Variable to track the number of correct objects placed
    public bool[] allObjectsPlaced;


    void Start()
    {
        StartQuiz();
        allObjectsPlaced = new bool[levels.Length];
    }

    /*
    private void FixedUpdate()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            CheckAllObjectsInPanel(levels[i], i);
        }
    }
    */

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
            /*
            if (i==levels.Length-1)
            {
                Debug.Log(i);
                Debug.Log("Called");
                CheckAllObjectsinLsstPlacedInPanel(levels[i], i);
                // CheckAllObjectsPlacedInPanel(questionPanels[i], i);
                //Debug.Log(" working");
            }
            */
        }
        EnableAnswerButtons();
    }

    public void NextQuestion()
    {

        if (currentQuestion + 1 < levels.Length)
        {
            currentQuestion++;
            Debug.Log("Question : " + currentQuestion);
            ActivateCurrentQuestion();
            
        }
        else
        {
            Debug.Log("Quiz completed!");
            // Display end game panel
            EndgamePanel.SetActive(true);
        }
        CheckAllObjectsInPanel(levels[currentQuestion], currentQuestion);
        EnableAnswerButtons();
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
        FailedPanel.SetActive(true);
        /*
        Button selectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        // Shake the selected button
        //StartCoroutine(ShakeButton(selectedButton.gameObject, 1f, 20f));

        // Disable all answer buttons after an answer is selected
        // DisableAnswerButtons();

        // Pause the game and show the pause panel
        StartCoroutine(DelayBeforeNextQuestion());
        */
        DisableAnswerButtons();
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
        DisableAnswerButtons();
        // Delay for a short time before moving to the next question
        yield return new WaitForSeconds(1f);
        // Disable all buttons during the delay

        NextQuestion();
    }

    //logic of Level 5 part

    public void CheckAnswwer()
    {
        
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

    public void CheckAllObjectsInPanel(GameObject panel, int panelIndex)
    {
        bool allPlaced = true;
        bool allCorrect = true; // Track if all placed objects are correct
        correctObjectCount = 0; // Reset correct object count for the panel

        foreach (Transform slot in panel.transform)
        {
            DragDropLevel8 drag = slot.GetComponentInChildren<DragDropLevel8>();
            if (drag != null && drag.isDraggable)
            {
                allPlaced = false;
                break;
            }

            if (drag != null)
            {
                if (drag.isPlaceCorrect)
                {
                    correctObjectCount++;
                    Debug.Log("Placed one");
                }
                else
                {
                    allCorrect = false;
                }
            }
        }

        allObjectsPlaced[panelIndex] = allPlaced;

        if (allPlaced && allCorrect)
        {
            // Activate next panel if all objects are placed and all are correct for the last question
            Debug.Log("All");
            SManage.instance.IncreaseScore(1);
            StartCoroutine(DelayBeforeNextQuestion());
        }
        else if (allPlaced && !allCorrect)
        {
            // Reset all objects to their original position if not all correct for the last question
            ResetObjects(panel, panelIndex);
        }
    }

    private void ResetObjects(GameObject panel, int Index)
    {
        foreach (Transform slot in panel.transform)
        {
            DragDropLevel8 drag = slot.GetComponentInChildren<DragDropLevel8>();
            if (drag != null)
            {
                drag.ResetToOriginalPosition();
            }
        }
        CheckAllObjectsInPanel(levels[Index], Index);
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
}