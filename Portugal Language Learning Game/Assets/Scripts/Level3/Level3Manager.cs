 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class Level3Manager : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject EndgamePanel;
    public GameObject failedPanel;
    public GameObject victoryPanel;
    public TMP_Text scoreText;
    public Button[] answerButtons; // Changed to an array of buttons
    private int currentQuestion;
    public GameObject Answerbuttons;
    public GameObject bg2;
    public GameObject bg1;
    public GameObject player;
    public GameObject[] bars;
    public GameObject[] unAnimatedbars;
    int barcount = 6;
    public Level3animation PlayerAnim;
    int animCount = 0;

    public TMP_Text UserNameText;
    public TMP_Text UserScoreText;
    int CurrentPlayerScore;

    // Start is called before the first frame update
    void Start()
    {
        //Player
       /* var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
        string CurrentPlayerUsername = CurrentPlayer.GetComponent<CurrentPlayer>().Username;
        CurrentPlayerScore = CurrentPlayer.GetComponent<CurrentPlayer>().Score;
        UserNameText.text = CurrentPlayerUsername;*/


        StartQuiz();
        Answerbuttons.SetActive(false);
        player.SetActive(true);
        bg2.SetActive(false);
        bg1.SetActive(true);
        EndgamePanel.SetActive(false);
        failedPanel.SetActive(false);
        victoryPanel.SetActive(false);
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
        enablebuttons();
        for (int i = 0; i < levels.Length; i++)
        {
            if (currentQuestion == 5)
            {
                Answerbuttons.SetActive(true);
                bg2.SetActive(true);
                bg1.SetActive(false);
                player.SetActive(false);
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
            if(SManage.instance.score<11)
            {
                failedPanel.SetActive(true);
            }
            else 
            {
                var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
                //Error 
                if (CurrentPlayer != null || CurrentPlayer == null)
                {
                    if (SManage.instance.score == 11)
                    {
                        Debug.Log("complete");
                        victoryPanel.SetActive(true);
                        EndgamePanel.SetActive(true);
                        if (CurrentPlayer.GetComponent<CurrentPlayer>().Score == 2)
                        {
                            Debug.Log("Victory Card 3 and level 4 Unlocked ");
                            CurrentPlayer.GetComponent<CurrentPlayer>().Score = 3;
                            SManage.instance.StartCoroutine("SavePlayerScore");
                        }
                        else
                        {
                            Debug.Log("Victory Card 3 was already unlocked");
                        }
                    }

                }
            }
            
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
        animCount += 1;
        PlayerAnim.RightJump(animCount);
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
        animCount += 1;
        PlayerAnim.RightJump(animCount);
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
        disablebuttons();
        // Delay for a short time before moving to the next
        yield return new WaitForSeconds(1f);
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
      
        yield return new WaitForSeconds(2f*Time.deltaTime);
    }

    public void Increasescore()
    {
        Debug.Log("Animated");
        SManage.instance.IncreaseScore(1);
        barcount = barcount-1;
        if(barcount>=0)
        {
            unAnimatedbars[barcount].SetActive(false);
            bars[barcount].SetActive(true);
        }

    }

    public void disablebuttons()
    {
        foreach (Button button in answerButtons)
        {
            button.interactable = false;
        }
    }

    public void enablebuttons()
    {
        foreach (Button button in answerButtons)
        {
            button.interactable = true;
        }
    }
}


