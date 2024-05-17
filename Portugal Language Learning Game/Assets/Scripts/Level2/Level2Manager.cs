using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class Level2Manager : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject DialoguePanel;
    
    public GameObject CanvasforQuestions;
    public TMP_Text scoreText;
    public Button[] answerButtons; // Changed to an array of buttons
    private int currentQuestion;
    public GameObject dialogue1;
    public GameObject dialogue2;
    public GameObject player;
    public AudioSource WrongAnswer;
    public AudioSource RightAnswer;
    public Level2Conversation conversation;

    public TMP_Text UserNameText;
    public TMP_Text UserScoreText;
    int CurrentPlayerScore;

    public AudioSource sourceAudio;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        //Player
        /*var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
        string CurrentPlayerUsername = CurrentPlayer.GetComponent<CurrentPlayer>().Username;
        CurrentPlayerScore = CurrentPlayer.GetComponent<CurrentPlayer>().Score;
        UserNameText.text = CurrentPlayerUsername;*/

        SManage.instance.ResetScore();
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogue1.active || dialogue2.active)
        {
            player.SetActive(true);
        }
        else
        {
            player.SetActive(false);
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

        // Enable all answer buttons for the current question
        foreach (Button button in answerButtons)
        {
            button.interactable = true;
            button.GetComponent<Image>().color = Color.white; // Reset button color
        }
    }

    void NextQuestion()
    {

        if (currentQuestion + 1 < levels.Length)
        {
            currentQuestion++;

            ActivateCurrentQuestion();
        }
        else
        {
            
            Debug.Log("Quiz completed! and conversation started");
            // Display end game panel
            CanvasforQuestions.SetActive(false);
            var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
            if (CurrentPlayer != null)
            {
                if (SManage.instance.score == 5)
                {
                    if (CurrentPlayer.GetComponent<CurrentPlayer>().Score == 1)
                    {
                        Debug.Log("Victory Card 2 and level 3 Unlocked ");
                        CurrentPlayer.GetComponent<CurrentPlayer>().Score = 2;
                        SManage.instance.StartCoroutine("SavePlayerScore");
                    }
                    else
                    {
                        Debug.Log("Victory Card 2 was already unlocked");
                    }
                }
            }
            
            DialoguePanel.SetActive(true);
            conversation.ConversationStart();

        }
    }



    public void CorrectAnswer(int correctButtonIndex)
    {
        RightAnswer.Play();
        // Change the selected (incorrect) answer button color to green
        Button selectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        //selectedButton.GetComponent<Image>().color = Color.green; 
         
        // Update the score using ScoreManager
        SManage.instance.IncreaseScore(1);
        UpdateScoreText();

        // Disable all answer buttons after an answer is selected
        // DisableAnswerButtons();

        StartCoroutine(DelayBeforeNextQuestion());
    }

    public void IncorrectAnswer(int correctButtonIndex)
    {
        WrongAnswer.Play();
        // Change the selected (incorrect) answer button color to red
        Button selectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        //selectedButton.GetComponent<Image>().color = Color.red;
        
        // Shake the selected button
        StartCoroutine(ShakeButton(selectedButton.gameObject, 1f, 20f));

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
          /*  float y = originalPosition.y + Random.Range(-1f, 1f) * magnitude;*/

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
      
        // Delay for a short time before moving to the next question
        yield return new WaitForSeconds(1f);


        NextQuestion();
    }

}
