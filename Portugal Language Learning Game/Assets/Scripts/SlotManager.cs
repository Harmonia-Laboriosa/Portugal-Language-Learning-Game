using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public GameObject[] slots; // Array to store references to all slots
    
    private void Update()
    {
        //run the CheckAllObjectsPlaced funtion
        CheckAllObjectsPlaced();
    }

    public void CheckAllObjectsPlaced()
    {
        //make allobjects bool true
        bool allObjectsPlaced = true;
        
        //check whether each gameobjects are placed or not
        foreach (GameObject slot in slots)
        {
            DragDrop dragDrop = slot.GetComponentInChildren<DragDrop>();
            //checks the all isDraggable bool is tru
            if (dragDrop.isDraggable)
            {
                allObjectsPlaced = false;
                break; // Exit the loop early if any object is not placed
            }
        }
        //if all objects are placed than print and load next scence and add a score
        if (allObjectsPlaced)
        {
            Debug.Log("All objects are placed in slots!");
            //Load Next Scene 
            //Add a score

        }
        else
        {
            Debug.Log("Not all objects are placed in slots.");
        }
    }
}
