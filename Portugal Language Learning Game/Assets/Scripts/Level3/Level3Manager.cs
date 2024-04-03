using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Level3Manager : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject EndgamePanel;
    public TMP_Text scoreText;
    public Button[] answerButtons; // Changed to an array of buttons
    private int currentQuestion;
    public GameObject Answerbuttons;
    //public GameObject bg2;
    public GameObject player;
    public GameObject[] bars;
    public GameObject[] unAnimatedbars;
    int barcount = 5;
    public Level3animation Player;

    void Start()
    {
        StartQuiz();
        Answerbuttons.SetActive(false);
        player.SetActive(true);
      //  bg2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

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
            if (currentQuestion == 5)
            {
                Answerbuttons.SetActive(true);
            }
            levels[i].SetActive(i == currentQuestion);
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

        if (currentQuestion + 1 < levels.Length)
        {
            
            currentQuestion++;

            ActivateCurrentQuestion();
        }
        else
        {
            Answerbuttons.SetActive(false);
            Debug.Log("Quiz completed!");
            // Display end game panel
            EndgamePanel.SetActive(true);
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
        // Delay for a short time before moving to the next
        Player.RightJump();
        yield return new WaitForSeconds(2f);


        NextQuestion();
    }

    public void OpenAnswerPanel()
    {
        StartCoroutine(OpenAnswerPanelWithDelay());
    }

    private IEnumerator OpenAnswerPanelWithDelay()
    {
        yield return new WaitForSeconds(1f);
        
    }

    public void disableMC()
    {
        StartCoroutine("disableplayer");
    }

    public IEnumerator disableplayer()
    {
        //player.SetActive(false);
        yield return new WaitForSeconds(2f*Time.deltaTime);
        //bg2.SetActive(true);



    }

    public void Increasescore()
    {
        SManage.instance.IncreaseScore(1);
        barcount -= 1;
        unAnimatedbars[barcount].SetActive(false);
        bars[barcount].SetActive(true);
    }
}


