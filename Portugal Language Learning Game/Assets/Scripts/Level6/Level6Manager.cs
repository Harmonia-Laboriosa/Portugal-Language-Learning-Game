using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Level6Manager : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject EndgamePanel;
    public GameObject FailedPanel;
    public GameObject dialoguePanel; // Dialogue panel reference
    public TMP_Text scoreText;
    public Button[] answerButtons;
    private int currentQuestion;

    void Start()
    {
        StartQuiz();
        Debug.Log(levels.Length);
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
        EnableAnswerButtons();
    }

    public void NextQuestion()
    {
        if (currentQuestion + 1 < levels.Length)
        {
            currentQuestion++;

            if (currentQuestion == 3) // Activate dialogue panel after question 3
            {
                ActivateDialoguePanel();
            }
            else
            {
                dialoguePanel.SetActive(false);
                ActivateCurrentQuestion();
            }
        }
        else
        {
            Debug.Log("Quiz completed!");
            // Display end game panel
            EndgamePanel.SetActive(true);
        }
        EnableAnswerButtons();
    }

    void ActivateDialoguePanel()
    {
        // Deactivate all question panels
        foreach (GameObject level in levels)
        {
            level.SetActive(false);
        }

        // Activate the dialogue panel
        dialoguePanel.SetActive(true);

        // Disable answer buttons during dialogue
        DisableAnswerButtons();
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
        //FailedPanel.SetActive(false);
        /*
        Button selectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        // Shake the selected button
        //StartCoroutine(ShakeButton(selectedButton.gameObject, 1f, 20f));

        // Disable all answer buttons after an answer is selected
        // DisableAnswerButtons();
        */
        // Pause the game and show the pause panel
        StartCoroutine(DelayBeforeNextQuestion());

        //DisableAnswerButtons();
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
