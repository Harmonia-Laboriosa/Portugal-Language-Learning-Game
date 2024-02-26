using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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


    // Start is called before the first frame update
    void Start()
    {
        allObjectsPlaced = new bool[questionPanels.Length];
        scoreIncreased = new bool[questionPanels.Length];
    }

    private void FixedUpdate()
    {
        // Check all question panels for placed objects
        for (int i = 0; i < questionPanels.Length; i++)
        {
            CheckAllObjectsPlacedInPanel(questionPanels[i], i);
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
}
