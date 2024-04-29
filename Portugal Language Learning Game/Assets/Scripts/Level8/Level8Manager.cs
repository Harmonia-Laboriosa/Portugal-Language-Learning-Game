using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class Level8Manager : MonoBehaviour
{
    public GameObject[] questionPanels; // Array to store references to all question panels
    public bool[] allObjectsPlaced; // Array to store whether all objects are placed in each question panel
    public bool[] scoreIncreased; // Array to store whether the score has been increased for each question panel
    public SManage scoreManager;
    public int TempScore = 0;
    public Level8Manage questionManager;
    public float waitForNextQuestion;
    public GameObject EndPanel;
    public GameObject FailedPanel;
    public GameObject victoryPanel;
    public int currentQuestion = 0;

    private bool gameEnded = false;

    public Animator player1;
    public GameObject bg1;
    public GameObject bg2;

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

        allObjectsPlaced = new bool[questionPanels.Length];
        scoreIncreased = new bool[questionPanels.Length];

        EndPanel.SetActive(false);
        FailedPanel.SetActive(false);
        victoryPanel.SetActive(false);
        bg1.SetActive(false);
        bg2.SetActive(false);   
    }
    private void FixedUpdate()
    {
        for (int i = 2; i < questionPanels.Length; i++)
        {       
            CheckAllObjectsPlacedInPanel(questionPanels[i], i);       
        }    
    }
    public void CheckAllObjectsPlacedInPanel(GameObject panel, int panelIndex)
    {
        bool allPlaced = true;
        foreach (Transform slot in panel.transform)
        {
            DragDropLevel8 dragDrop = slot.GetComponentInChildren<DragDropLevel8>();
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
                DragDropLevel8 dragDrop = slot.GetComponentInChildren<DragDropLevel8>();
                if (dragDrop != null && dragDrop.isPlaceCorrect)
                {
                    
                    TempScore = TempScore + 1;
                    
                    Debug.Log(TempScore);
                    if (TempScore == 4)
                    {
                        scoreManager.IncreaseScore(1);
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
        if (allPlaced)
        {
            ActivateNextPanel(panelIndex);
        }
    }
    public void ActivateNextPanel(int currentPanelIndex)
    {
        
        // Activate the panel for the next question if available
        if (currentPanelIndex+1<questionPanels.Length)
        {

            StartCoroutine(ActivatePanelWithDelay(currentPanelIndex + 1));
            if(currentPanelIndex==6)
            {
                bg2.SetActive(true);
            }
        }
        else
        {
            // Activate end panel if all questions are completed
            EndGameScore();

        }
    }

    private void EndGameScore()
    {
        if (SManage.instance.score < 12)
        {
            FailedPanel.SetActive(true);
        }
        else
        {
            var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
            if (CurrentPlayer != null)
            {
                if (SManage.instance.score == 12)
                {
                    victoryPanel.SetActive(true);
                    EndPanel.SetActive(true);
                    if (CurrentPlayer.GetComponent<CurrentPlayer>().Score == 7)
                    {
                        Debug.Log("Victory Card 8 and level 9 Unlocked ");
                        CurrentPlayer.GetComponent<CurrentPlayer>().Score = 8;
                        SManage.instance.StartCoroutine("SavePlayerScore");
                    }
                    else
                    {
                        Debug.Log("Victory Card 8 was already unlocked");
                    }
                }
            }


        }
    }
    private IEnumerator ActivatePanelWithDelay(int nextPanelIndex)
    {
        // Wait for few seconds
        yield return new WaitForSeconds(waitForNextQuestion);

        // Activate the next panel after the delay
        
        questionManager.panels[nextPanelIndex].SetActive(true);
        questionManager.panels[nextPanelIndex - 1].SetActive(false);     
    }

    public void StartWalkAwayanim()
    {
        player1.SetBool("walkAway", true);
    }
    public void correctAnswer()
    {
        SManage.instance.IncreaseScore(1);
    }
   
}
