using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QManager : MonoBehaviour
{
    public GameObject[] Questions;
    //public GameObject EndgamePanel;
    [SerializeField] TMP_Text timerText;
    [SerializeField] float remaingTime;
    public TMP_Text scoreText;
    //public GameObject pausePanel; // Reference to the pause panel

    private int currentQuestion;
    private float timePerQuestion = 10f;
    private float countdownTimer;
    private Coroutine timerCoroutine;

    public SlotManager slotManager;

    void Start()
    {
        //StartQuiz();
    }

    // Update is called once per frame
    void Update()
    {
        if (remaingTime < 0)
        {
            //EndgamePanel.SetActive(true);
            return;
        }
        remaingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remaingTime / 60);
        int seconds = Mathf.FloorToInt(remaingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        //CorrectPlaced();

    }
    /*
    void StartQuiz()
    {
        // Reset the score when starting the quiz
        //ScoreManager.instance.ResetScore();

        currentQuestion = 0;
        ActivateCurrentQuestion();
        //StartTimer();
    }

    void ActivateCurrentQuestion()
    {
        for (int i = 0; i < Questions.Length; i++)
        {
            Questions[i].SetActive(i == currentQuestion);
        }

    }

    void NextQuestion()
    {

        if (currentQuestion + 1 < Questions.Length)
        {
            currentQuestion++;
            ActivateCurrentQuestion();
        }
        else
        {
            Debug.Log("Quiz completed!");
            // Stop the timer

            // Display end game panel

        }
    }



    public void CorrectPlaced()
    {
        if (slotManager != null)
        {
            
            if (slotManager.allObjectsPlacedinQuestion1)
            {
                slotManager.allObjectsPlacedinQuestion1 = false;
                NextQuestion();
            }
            else
            {
                Debug.Log("NotPlaced");
            }
            
        }
    }
    */


    void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game

    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game

    }

    IEnumerator DelayBeforeNextQuestion()
    {
        // Delay for a short time before moving to the next question
        yield return new WaitForSeconds(0.5f);

        // Resume the game
        ResumeGame();

        //NextQuestion();
    }


}
