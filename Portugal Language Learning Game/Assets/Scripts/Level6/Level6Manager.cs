using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class Level6Manager : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject EndgamePanel;
    public GameObject FailedPanel;
    public GameObject victoryPanel;
    public GameObject dialoguePanel; // Dialogue panel reference
    public TMP_Text scoreText;
    public Button[] answerButtons;
    private int currentQuestion;
    public ConversationLevel6 conversation;
    public Animator playerLevel6;
    public GameObject Q4;

    private bool gameEnded = false;

    //public TMP_Text UserNameText;
    //public TMP_Text UserScoreText;
    //int CurrentPlayerScore;

    // Start is called before the first frame update
    void Start()
    {
        //Player
        //var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
        //string CurrentPlayerUsername = CurrentPlayer.GetComponent<CurrentPlayer>().Username;
        //CurrentPlayerScore = CurrentPlayer.GetComponent<CurrentPlayer>().Score;
        //UserNameText.text = CurrentPlayerUsername;

        //StartQuiz();
        conversation.ConversationStart();
        Debug.Log(levels.Length);
    }

    void StartQuiz()
    {
        // Reset the score when starting the quiz
        SManage.instance.ResetScore();
        currentQuestion = 0;
        ActivateDialoguePanel();
        EndgamePanel.SetActive(false);
        FailedPanel.SetActive(false);
        victoryPanel.SetActive(false);
        //StartTimer();
    }

    public void ActivateCurrentQuestion()
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

    public void walk()
    {
        StartCoroutine("WalkAndRead");
    }
    IEnumerator WalkAndRead()
    {
        if (playerLevel6 != null)
        {
            playerLevel6.SetBool("walk", true);
            yield return new WaitForSeconds(6.5f);
            Debug.Log("true");
            Q4.SetActive(true);
            Debug.Log("false");
        }
    }

}
