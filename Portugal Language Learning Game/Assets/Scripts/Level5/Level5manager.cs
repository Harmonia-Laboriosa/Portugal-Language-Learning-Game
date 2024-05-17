using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using Unity.VisualScripting;

public class Level5manager : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject EndgamePanel;
    public GameObject FailedPanel;
    public GameObject victoryPanel;
    public TMP_Text scoreText;
    public Button[] answerButtons; // Changed to an array of buttons
    public Button[] checkButtons; // Changed to an array of buttons
    private int currentQuestion;
    [HideInInspector] public bool tagFromCollissionHour;
    [HideInInspector] public bool tagFromCollissionMinute;

    private int correctObjectCount; // Variable to track the number of correct objects placed

    public bool allObjectsPlaced;
    public bool scoreIncreased;
    public GameObject rock;

    public Animator rockAnimator;
    public Animator player2Animator;

    public GameObject bg;
    public GameObject bg1;
    public GameObject playerAnim;
    public GameObject player2;
    public GameObject q11;

    private bool gameEnded = false;

    public TMP_Text UserNameText;
    public TMP_Text UserScoreText;
    int CurrentPlayerScore;

    public AudioClip clip;
    public AudioSource Asource;

    public AudioSource WrongAnswer;
    public AudioSource RightAnswer;

    public AudioSource sourceAudio;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        //Player
      /*  var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
        string CurrentPlayerUsername = CurrentPlayer.GetComponent<CurrentPlayer>().Username;
        CurrentPlayerScore = CurrentPlayer.GetComponent<CurrentPlayer>().Score;
        UserNameText.text = CurrentPlayerUsername;*/

        bg.SetActive(true);
        bg1.SetActive(false);
        player2.SetActive(false);
        playerAnim.SetActive(true);
        StartCoroutine("StartLevel");
    }
    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(6f);
        StartQuiz();
        //playerAnim.SetActive(false);
    }

    private void FixedUpdate()
    {
            for (int i = 0; i < levels.Length; i++)
            {
                if (i==levels.Length - 1)
                {
                CheckAllObjectsinLsstPlacedInPanel(levels[i], i);

                //CheckAllObjectsPlacedInPanel(levels[i], i);
                 }
                else
                {

                }
            }
    }

    void StartQuiz()
    {
        // Reset the score when starting the quiz
        SManage.instance.ResetScore();
        currentQuestion = 0;
        ActivateCurrentQuestion();
        rock.SetActive(false);
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
            if (currentQuestion == 5)
            {
                bg1.SetActive(true);

                

            }
            if (currentQuestion == 10)
            {
                playerAnim.SetActive(false);
                CheckAllObjectsinLsstPlacedInPanel(levels[currentQuestion], currentQuestion);
            }
        }
        else
        {
            Debug.Log("Quiz completed!");
            rockAnimator.SetBool("MoveBoulder", true);
            // Display end game panel
            StartCoroutine("endPanelActive");
        }
        EnableAnswerButtons();
    }
    IEnumerator endPanelActive()
    {
        SManage.instance.IncreaseScore(1);
        yield return new WaitForSeconds(2f);
        if (SManage.instance.score < 11)
        {
            FailedPanel.SetActive(true);
        }
        else
        {
            var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
            if (CurrentPlayer != null || CurrentPlayer == null)
            {
                if (SManage.instance.score == 11)
                {
                    StartCoroutine("TimeDelayEndpanel");
                    victoryPanel.SetActive(true);
                    EndgamePanel.SetActive(true);
                    if (CurrentPlayer.GetComponent<CurrentPlayer>().Score == 4)
                    {
                        Debug.Log("Victory Card 5 and level 6 Unlocked ");
                        CurrentPlayer.GetComponent<CurrentPlayer>().Score = 5;
                        SManage.instance.StartCoroutine("SavePlayerScore");
                    }
                    else
                    {
                        Debug.Log("Victory Card 5 was already unlocked");
                    }
                }
            }


        }
    }

    IEnumerator TimeDelayEndpanel()
    {
        yield return new WaitForSeconds(2f);
        
    }

    public void CorrectAnswer(int correctButtonIndex)
    {
        RightAnswer.Play();
        //Destroy(shatter);
        SManage.instance.IncreaseScore(1);
        UpdateScoreText();
        // Shatter all answer buttons after an answer is selected
        StartCoroutine(DelayBeforeNextQuestion());
    }

    public void IncorrectAnswer(int correctButtonIndex)
    {
        WrongAnswer.Play();
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
        scoreText.text = "Points: " + SManage.instance.GetScore().ToString();
        sourceAudio.PlayOneShot(audioClip, 0.75f);
    }

    IEnumerator DelayBeforeNextQuestion()
    {
        DisableAnswerButtons();
        // Delay for a short time before moving to the next question
        yield return new WaitForSeconds(1f);
        // Disable all buttons during the delay
        

        NextQuestion();
    }

    public void RockA()
    {
        StartCoroutine("RockActive");
    }
    public IEnumerator RockActive()
    { 
        yield return new WaitForSeconds(1f);
        
        rock.SetActive(true);   
        player2.SetActive(true);
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
        RightAnswer.Play();
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
            DragDropLevel5 drag = slot.GetComponentInChildren<DragDropLevel5>();
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

        allObjectsPlaced = allPlaced;
        scoreIncreased = true;
        if (allPlaced && allCorrect)
        {
            // Activate next panel if all objects are placed and all are correct for the last question
            Debug.Log("All");
            
            q11.SetActive(false);
            rockAnimator.SetBool("MoveBoulder", true);
            
            player2Animator.SetBool("move", true);

            // Display end game panel
            EndGameScore();
        }
        else if (allPlaced && !allCorrect)
        {
            // Reset all objects to their original position if not all correct for the last question
            ResetObjects(panel);
        }
    }

    private void EndGameScore()
    {
        Asource.PlayOneShot(clip, 0.15f);
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
                    EndgamePanel.SetActive(true);
                }
            }

            gameEnded = true; // Set the flag to true to indicate that the game has ended
        }
    }
    private void ResetObjects(GameObject panel)
    {
        foreach (Transform slot in panel.transform)
        {
            DragDropLevel5 drag = slot.GetComponentInChildren<DragDropLevel5>();
            if (drag != null)
            {
                drag.ResetToOriginalPosition();
            }
        }
        CheckAllObjectsinLsstPlacedInPanel(levels[levels.Length - 1], levels.Length - 1);
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
