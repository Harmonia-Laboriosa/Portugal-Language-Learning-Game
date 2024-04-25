using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class Level10Managers : MonoBehaviour
{
    public GameObject[] questions;
    public GameObject EndPanel;
    public GameObject FailedPanel;
    public GameObject victoryPanel;
    public TMP_Text scoreText;
    public Button[] answerButtons; // Changed to an array of buttons
    private int currentQuestion;
    public GameObject Answerbuttons;
    int fromSlot;
    public int iScore=0;
    public bool[] allObjectsPlaced;
    public int TempScore = 0;
    public SManage scoreManager;
    public bool[] scoreIncreased;
    private bool gameEnded = false;
    public Level10animation animations;

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

        allObjectsPlaced = new bool[questions.Length];
        scoreIncreased = new bool[questions.Length];
        fromSlot = 0;
        
        StartCoroutine("startAnimation");
        EndPanel.SetActive(false);
        FailedPanel.SetActive(false);
        victoryPanel.SetActive(false);
        Answerbuttons.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator startAnimation()
    {
        yield return new WaitForSeconds(3.5f);
        StartQuiz();
    }
    void StartQuiz()
    {
        // Reset the score when starting the quiz
        SManage.instance.ResetScore();
        currentQuestion = 0;
        ActivateCurrentQuestion();
        //StartTimer();
    }
    private void FixedUpdate()
    {
        // Iterate over question panels for questions 6, 7, and 8 only if they are active
        for (int i = 5; i <= 7; i++)
        {
            if (questions[i].activeSelf)
            {
                CheckAllObjectsPlacedInPanel(questions[i], i);
            }
        }
    }

    public void CheckAllObjectsPlacedInPanel(GameObject panel, int panelIndex)
    {
        bool allPlaced = true;
        foreach (Transform slot in panel.transform)
        {
            Level10DragDrop dragDrop = slot.GetComponentInChildren<Level10DragDrop>();
            if (dragDrop != null && dragDrop.isDraggable)
            {
                allPlaced = false;
                break;
            }
        }
        allObjectsPlaced[panelIndex] = allPlaced;

        // Increase score if all objects are placed and the score has not been increased for this panel yet
        if (allPlaced && !scoreIncreased[panelIndex])
        {
            foreach (Transform slot in panel.transform)
            {
                Level10DragDrop dragDrop = slot.GetComponentInChildren<Level10DragDrop>();
                if (dragDrop != null && dragDrop.isPlaceCorrect)
                {

                    TempScore = TempScore + 1;

                    Debug.Log(TempScore);
                    if (TempScore == 9)
                    {
                        scoreManager.IncreaseScore(1);
                        StartCoroutine(DelayBeforeNextQuestion());
                        TempScore = 0;
                    }

                }
                if (dragDrop != null && dragDrop.isPlaceCorrect && slot.GetComponentInChildren<VerticalLayoutGroup>())
                {
                    scoreManager.IncreaseScore(1);
                }

            }
            scoreIncreased[panelIndex] = true; // Mark that the score has been increased for this panel
        }
        // Activate next panel if all objects are placed

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
            StartCoroutine("endAnimation");
        }
        EnableAnswerButton();
    }

    public IEnumerator endAnimation()
    {
        animations.end();
        yield return new WaitForSeconds(2f);
        EndGameScore();
    }

    private void EndGameScore()
    {
        if (!gameEnded) // Check if the game has not ended yet
        {
            if (SManage.instance.score <= 6)
            {
                FailedPanel.SetActive(true);
            }
            else
            {
                if (SManage.instance.score >= 9)
                {
                    victoryPanel.SetActive(true);
                }
                else
                {
                    EndPanel.SetActive(true);
                }
            }

            gameEnded = true; // Set the flag to true to indicate that the game has ended
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

        // Shake the selected button
        //StartCoroutine(ShakeButton(selectedButton.gameObject, 1f, 20f));

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
            if(iScore == 2)
            {
                SManage.instance.IncreaseScore(1);
                iScore = 0;
            }
            else
            {
                iScore = 0;
            }
            fromSlot = 0;
        }
    }
}
