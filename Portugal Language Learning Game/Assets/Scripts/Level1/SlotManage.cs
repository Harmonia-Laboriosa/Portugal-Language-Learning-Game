using System.Collections;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        victoryPanel.SetActive(false);
        failedPanel.SetActive(false);
        EndPanel.SetActive(false);

        allObjectsPlaced = new bool[questionPanels.Length];
        scoreIncreased = new bool[questionPanels.Length];
        if (questionPanels.Length > 0)
        {
            isLastQuestion = true;
        }

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
                        TempScore = 0;
                    }

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
            ResetObjects(panel);
        }
    }

    private void EndGameScore()
    {
        if (!gameEnded) // Check if the game has not ended yet
        {
            if (scoreManager.score <= 6)
            {
                failedPanel.SetActive(true);
            }
            else
            {
                EndPanel.SetActive(true);
                if (scoreManager.score >= 9)
                {
                    victoryPanel.SetActive(true);
                }
            }

            gameEnded = true; // Set the flag to true to indicate that the game has ended
        }
    }

    private void ResetObjects(GameObject panel)
    {
        foreach (Transform slot in panel.transform)
        {
            DragDrop dragDrop = slot.GetComponentInChildren<DragDrop>();
            if (dragDrop != null)
            {
                dragDrop.ResetToOriginalPosition();
            }
        }
        CheckAllObjectsinLsstPlacedInPanel(questionPanels[questionPanels.Length-1], questionPanels.Length-1);
    }
}
