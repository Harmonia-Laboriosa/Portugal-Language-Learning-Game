using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

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


    public TMP_Text UserNameText;
    public TMP_Text UserScoreText;
    int CurrentPlayerScore;

    public AudioSource WrongAnswer;
    public AudioSource RightAnswer;

    public AudioSource sourceAudio;
    public AudioClip audioClip;

    public GameObject MarketSound;

    public GameObject backgroundsound;

    // Start is called before the first frame update
    void Start()
    {
        //Player
        var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
        string CurrentPlayerUsername = CurrentPlayer.GetComponent<CurrentPlayer>().Username;
        CurrentPlayerScore = CurrentPlayer.GetComponent<CurrentPlayer>().Score;
        UserNameText.text = CurrentPlayerUsername;

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
            if(currentQuestion == 5)
            {
                MarketSound.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Quiz completed!");
            // Display end game panel
            StartCoroutine("Ending");
        }
        EnableAnswerButtons();
    }

    private IEnumerator Ending()
    {
        yield return new WaitForSeconds(2f);
        EndGameScore();
    }
    private void EndGameScore()
    {
        backgroundsound.SetActive(false);
        Debug.Log("Level ended");
        if (SManage.instance.score < 10)
        {
            FailedPanel.SetActive(true);
        }
        else
        {
            var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
            if (CurrentPlayer != null || CurrentPlayer == null)
            {
                if (SManage.instance.score == 10)
                {
                    victoryPanel.SetActive(true);
                    EndgamePanel.SetActive(true);
                    if (CurrentPlayer.GetComponent<CurrentPlayer>().Score == 6)
                    {
                        Debug.Log("Victory Card 7 and level 8 Unlocked ");
                        CurrentPlayer.GetComponent<CurrentPlayer>().Score = 7;
                        SManage.instance.StartCoroutine("SavePlayerScore");
                    }
                    else
                    {
                        Debug.Log("Victory Card 7 was already unlocked");
                    }
                }
            }

        }
    }
    public void CorrectAnswer(int correctButtonIndex)
    {
        RightAnswer.Play();
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
        WrongAnswer.Play();
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
        sourceAudio.PlayOneShot(audioClip, 0.75f);
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
