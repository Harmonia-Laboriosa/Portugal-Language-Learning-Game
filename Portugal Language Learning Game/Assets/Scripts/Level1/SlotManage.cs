using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class SlotManage : MonoBehaviour
{
    public GameObject[] questionPanels; // Array to store references to all question panels
    public bool[] allObjectsPlaced; // Array to store whether all objects are placed in each question panel
    public bool[] scoreIncreased; // Array to store whether the score has been increased for each question panel
    public SManage scoreManager;
    public int TempScore =0;
    public QManage questionManager;
    public float waitForNextQuestion;
    public GameObject EndPanel;
    public GameObject victoryPanel;
    public GameObject failedPanel;

    private int correctObjectCount; // Variable to track the number of correct objects placed
    private bool isLastQuestion; // Flag to check if it's the last question

    private bool gameEnded = false;

    //public TMP_Text UserNameText;
    //public TMP_Text UserScoreText;

    public AudioSource sourceAudio;
    public AudioClip[] audioClip;

    public GameObject BackgroundSound;
    

    // Start is called before the first frame update
    void Start()
    {
        //Player
        //CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
        //string CurrentPlayerUsername = CurrentPlayer.GetComponent<CurrentPlayer>().Username;
        //CurrentPlayerScore = CurrentPlayer.GetComponent<CurrentPlayer>().Score;
        //UserNameText.text = CurrentPlayerUsername;

        victoryPanel.SetActive(false);
        failedPanel.SetActive(false);
        EndPanel.SetActive(false);

        allObjectsPlaced = new bool[questionPanels.Length];
        scoreIncreased = new bool[questionPanels.Length];
        if (questionPanels.Length > 0)
        {
            isLastQuestion = true;
        }

        BackgroundSound.SetActive(true);

    }

    private void FixedUpdate()
    {

        if (isLastQuestion)
        {
            for (int i = 0; i < questionPanels.Length; i++)
            {
                if(i<questionPanels.Length-1)
                {
                    CheckAllObjectsPlacedInPanel(questionPanels[i], i);
                }
                else
                {
                    CheckAllObjectsinLsstPlacedInPanel(questionPanels[i], i);
                }

            }
        }
    }

    public void CheckAllObjectsPlacedInPanel(GameObject panel, int panelIndex)
    {
        bool allPlaced = true;
        bool allPlacedcorrect = false;
        foreach (Transform slot in panel.transform)
        {
            DragDrop dragDrop = slot.GetComponentInChildren<DragDrop>();
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
                DragDrop dragDrop = slot.GetComponentInChildren<DragDrop>();
                if (dragDrop != null && dragDrop.isPlaceCorrect)
                {
                    TempScore = TempScore + 1;
                    if (TempScore == 3 || TempScore ==6)
                    {
                        
                        scoreManager.IncreaseScore(1);
                        sourceAudio.PlayOneShot(audioClip[0], 0.75f);
                        TempScore = 0;
                        allPlacedcorrect = true;
                    }
                    

                }

            }
            if(!allPlacedcorrect)
            {
                   sourceAudio.PlayOneShot(audioClip[1], 0.75f);
            }

            scoreIncreased[panelIndex] = true; // Mark that the score has been increased for this panel
        }

        // Activate next panel if all objects are placed
        if (allPlaced)
        {
            ActivateNextPanel(panelIndex);
                
        }
    }




    private void ActivateNextPanel(int currentPanelIndex)
    {
        
        // Activate the panel for the next question if available
        if (currentPanelIndex < questionPanels.Length - 1)
        {
            StartCoroutine(ActivatePanelWithDelay(currentPanelIndex + 1));
            
        }
        else
        {
            // Activate end panel if all questions are completed
            questionManager.endPanel.SetActive(true);
        }
    }

    private IEnumerator ActivatePanelWithDelay(int nextPanelIndex)
    {
        
        // Wait for 2 seconds
        yield return new WaitForSeconds(waitForNextQuestion);

        // Activate the next panel after the delay
        questionManager.panels[nextPanelIndex].SetActive(true);
        questionManager.panels[nextPanelIndex - 1].SetActive(false);

    }


    // Add other methods as needed

    public void CheckAllObjectsinLsstPlacedInPanel(GameObject panel, int panelIndex)
    {
        bool allPlaced = true;
        bool allCorrect = true; // Track if all placed objects are correct
        correctObjectCount = 0; // Reset correct object count for the panel

        foreach (Transform slot in panel.transform)
        {
            DragDrop dragDrop = slot.GetComponentInChildren<DragDrop>();
            if (dragDrop != null && dragDrop.isDraggable)
            {
                allPlaced = false;
                break;
            }

            if (dragDrop != null)
            {
                if (dragDrop.isPlaceCorrect)
                {
                    correctObjectCount++;
                }
                else
                {
                    allCorrect = false;
                }
            }
        }

        allObjectsPlaced[panelIndex] = allPlaced;

        if (allPlaced && !scoreIncreased[panelIndex])
        {
            // Increase score if all objects are placed and the score has not been increased for this panel yet
            foreach (Transform slot in panel.transform)
            {
                DragDrop dragDrop = slot.GetComponentInChildren<DragDrop>();
                if (dragDrop != null && dragDrop.isPlaceCorrect)
                {
                    TempScore = TempScore + 1;
                    if (TempScore == 3 || TempScore == 6)
                    {
                        sourceAudio.PlayOneShot(audioClip[0], 0.75f);
                        scoreManager.IncreaseScore(1);
                        TempScore = 0;
                    }
                }
            }

            scoreIncreased[panelIndex] = true;
        }

        if (allPlaced && allCorrect)
        {
            EndGameScore();
        }
        else if (allPlaced && !allCorrect)
        {
            // Reset all objects to their original position if not all correct for the last question
            sourceAudio.PlayOneShot(audioClip[1], 0.75f);
            ResetObjects(panel);
        }
    }

    private void EndGameScore()
    {
        BackgroundSound.SetActive(false);
        SManage.instance.totalScore=SManage.instance.totalScore+SManage.instance.score;
        /*
        if(SManage.instance.totalScore>=0 && SManage.instance.totalScore<=12)
        {

        }
        CurrentPlayer.GetComponent<CurrentPlayer>().Score = SManage.instance.totalScore;
        */
        if (!gameEnded) // Check if the game has not ended yet
        {
            CheckVictoryCard();
            if (scoreManager.score < 13)
            {
                failedPanel.SetActive(true);
            }
            else
            {
                /*
                if (SManage.instance.score > SManage.instance.Level1Score)
                {
                    CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
                    SManage.instance.Level1Score = SManage.instance.score;
                    CurrentPlayer.GetComponent<CurrentPlayer>().Score += SManage.instance.Level1Score;
                    StartCoroutine(SavePlayerScore());
                }
                */
                EndPanel.SetActive(true);
                if (scoreManager.score==13)
                {
                    victoryPanel.SetActive(true);
                }
            }

            gameEnded = true; // Set the flag to true to indicate that the game has ended
        }
    }

    IEnumerator SavePlayerScore()
    {
        var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");

        string username = CurrentPlayer.GetComponent<CurrentPlayer>().Username;
        string scoreFromPlayer = CurrentPlayer.GetComponent<CurrentPlayer>().Score.ToString();
        WWWForm scoreForm = new WWWForm();
        scoreForm.AddField("apppassword", "thisisfromtheapp");
        scoreForm.AddField("username", username);
        scoreForm.AddField("score", scoreFromPlayer);
        UnityWebRequest updatePlayerRequest = UnityWebRequest.Post("http://ec2-54-172-175-103.compute-1.amazonaws.com/cruds/updateplayerscore.php", scoreForm);
        yield return updatePlayerRequest.SendWebRequest();
        if (updatePlayerRequest.error == null)
        {

            string result = updatePlayerRequest.downloadHandler.text;
            Debug.Log(result);
            if (result == "0")
            {
                //FindObjectOfType<SceneSwitch>().LoadGameScene();
            }
            else
            {
                Debug.Log("error");
            }
        }
        else
        {
            Debug.Log(updatePlayerRequest.error);
        }

    }
    public void CheckVictoryCard()
    {
        var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
        if (CurrentPlayer != null)
        {
            if (SManage.instance.score == 13)
            {
                if (CurrentPlayer.GetComponent<CurrentPlayer>().Score == 0)
                {
                    Debug.Log("Victory 1 and level 2 Unlocked");
                    CurrentPlayer.GetComponent<CurrentPlayer>().Score = 1;
                    StartCoroutine(SavePlayerScore());
                }
                else
                {
                    Debug.Log("Already Victory 1 was uncloked so unable to increase the score.");
                }
            }
        }
    }
    public void ResetObjects(GameObject panel)
    {
        foreach (Transform slot in panel.transform)
        {
            DragDrop dragDrop = slot.GetComponentInChildren<DragDrop>();
            if (dragDrop != null)
            {
                dragDrop.ResetToOriginalPosition();
            }
        }
        CheckAllObjectsPlacedInPanel(questionPanels[questionPanels.Length-1], questionPanels.Length-1);
        //CheckAllObjectsPlacedInPanel(questionPanels[questionPanels.Length - 1], questionPanels.Length - 1);
    }
    public void Reset(GameObject panel)
    {
        foreach (Transform slot in panel.transform)
        {
            DragDrop dragDrop = slot.GetComponentInChildren<DragDrop>();
            if (dragDrop != null)
            {
                dragDrop.ResetToOriginalPos();
            }
        }
        CheckAllObjectsPlacedInPanel(questionPanels[questionPanels.Length - 1], questionPanels.Length - 1);
    }
}
   
