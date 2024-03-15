/*using System.Collections;
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
            ActivateCurrentQuestion();
        }
        else
        {
            Debug.Log("Quiz completed!");
            // Display end game panel
            EndgamePanel.SetActive(true);
        }
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
        //FailedPanel.SetActive(false);
       
        Button selectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        // Shake the selected button
        //StartCoroutine(ShakeButton(selectedButton.gameObject, 1f, 20f));

        // Disable all answer buttons after an answer is selected
        // DisableAnswerButtons();
       
        // Pause the game and show the pause panel

        int clickedButtonIndex = -1;
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (EventSystem.current.currentSelectedGameObject == answerButtons[i].gameObject)
            {
                clickedButtonIndex = i;
                break;
            }
        }

        // Disable the clicked incorrect button
        if (clickedButtonIndex != correctButtonIndex)
        {
            answerButtons[clickedButtonIndex].gameObject.SetActive(false);
        }

        //(DelayBeforeNextQuestion());

        //DisableAnswerButtons();
    }

    IEnumerator ShakeButton(GameObject buttonObject, float duration, float magnitude)
    {
        Vector3 originalPosition = buttonObject.transform.position;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = originalPosition.x + Random.Range(-1f, 1f) * magnitude;
            *//*float y = originalPosition.y + Random.Range(-1f, 1f) * magnitude;*//*

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


*/

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
    public TMP_Text scoreText;
    public Button[] answerButtons;
    private int currentQuestion;

    // List to keep track of correct answers for each question
    private List<int> correctAnswerIndices = new List<int>();

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
            EndgamePanel.SetActive(true);
        }
        EnableAnswerButtons();
    }

    public void CorrectAnswer(int correctButtonIndex)
    {
        // Change color of the correct answer button to green
        answerButtons[correctButtonIndex].image.color = Color.green;

        // Add the selected correct answer index to the list
        correctAnswerIndices.Add(correctButtonIndex);

        // Check if all correct answers are selected
        if (correctAnswerIndices.Count == 5) // Assuming there are 5 correct answers
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
        DisableAnswerButtons();
        // Delay for a short time before moving to the next question
        yield return new WaitForSeconds(1f);
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
}
