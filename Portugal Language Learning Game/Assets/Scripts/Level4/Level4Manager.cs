using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Level4Manager : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject EndgamePanel;
    public TMP_Text scoreText;
    public Button[] answerButtons; // Changed to an array of buttons
    private int currentQuestion;
    public string tagFromCollission;
    public TMP_Text letter;
    public GameObject boulder;
    public GameObject background;
    public GameObject rocks;
    public int animationscore=0;
    public Level4Animation animations;

    void Start()
    {
        StartQuiz();
        boulder.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        rocks.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        letter.text = tagFromCollission;
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
            if(currentQuestion == 6)
            {
                background.gameObject.SetActive(true);
                boulder.gameObject.SetActive(true);
                rocks.gameObject.SetActive(true);
            }
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
        //Instantiate(shattered, shatter.transform.position, Quaternion.identity);
        //Destroy(shatter);
        SManage.instance.IncreaseScore(1);
        UpdateScoreText();
        // Shatter all answer buttons after an answer is selected
        StartCoroutine(DelayBeforeNextQuestion());
        animationscore += 1;
        animations.playAnimation(animationscore);
       
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
        DisableAnswerButtons();
        // Delay for a short time before moving to the next question
        yield return new WaitForSeconds(1f);


        NextQuestion();
    }


    //logic of Level 4 part

    public void CheckAnswwer()
    {
        Debug.Log(tagFromCollission);
        Debug.Log(levels[currentQuestion].gameObject.tag);
        if (levels[currentQuestion].gameObject.tag==tagFromCollission)
        {
            CorrectAnswerPart1();
        }
        else
        {
            IncorrectAnswerPart1();
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
