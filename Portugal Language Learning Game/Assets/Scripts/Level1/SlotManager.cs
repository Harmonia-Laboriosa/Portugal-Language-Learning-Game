using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public GameObject[] Question1; // Array to store references to all slots
    public GameObject[] Question2; // Array to store references to all slots
    public GameObject[] Question3; // Array to store references to all slots
    public GameObject[] Question4; // Array to store references to all slots
    public GameObject[] Question5; // Array to store references to all slots
    public GameObject[] Question6; // Array to store references to all slots
    public GameObject[] Question7; // Array to store references to all slots
    public GameObject[] Question8; // Array to store references to all slots
    public GameObject[] Question9; // Array to store references to all slots
    public GameObject[] Question10; // Array to store references to all slots
    public GameObject[] Question11; // Array to store references to all slots
    public GameObject[] Question12; // Array to store references to all slots

    public bool allObjectsPlacedinQuestion1;
    public bool allObjectsPlacedinQuestion2;
    public bool allObjectsPlacedinQuestion3;
    public bool allObjectsPlacedinQuestion4;
    public bool allObjectsPlacedinQuestion5;
    public bool allObjectsPlacedinQuestion6;
    public bool allObjectsPlacedinQuestion7;
    public bool allObjectsPlacedinQuestion8;
    public bool allObjectsPlacedinQuestion9;
    public bool allObjectsPlacedinQuestion10;
    public bool allObjectsPlacedinQuestion11;
    public bool allObjectsPlacedinQuestion12;

    public QuestionManager questionManager;

    private void Update()
    {
        //run the CheckAllObjectsPlaced funtion
        CheckAllObjectsPlacedinQuestion1();
        CheckAllObjectsPlacedinQuestion2();
        CheckAllObjectsPlacedinQuestion3();
        CheckAllObjectsPlacedinQuestion4();
        CheckAllObjectsPlacedinQuestion5();
        CheckAllObjectsPlacedinQuestion6();
        CheckAllObjectsPlacedinQuestion7();
        CheckAllObjectsPlacedinQuestion8();
        CheckAllObjectsPlacedinQuestion9();
        CheckAllObjectsPlacedinQuestion10();
        CheckAllObjectsPlacedinQuestion11();
        CheckAllObjectsPlacedinQuestion12();
    }

    public void CheckAllObjectsPlacedinQuestion1()
    {
        //make allobjects bool true
        allObjectsPlacedinQuestion1 = true;

        //check whether each gameobjects are placed or not
        foreach (GameObject slot in Question1)
        {
            DragDrop dragDrop = slot.GetComponentInChildren<DragDrop>();
            //checks the all isDraggable bool is tru
            if (dragDrop.isDraggable)
            {
                allObjectsPlacedinQuestion1 = false;
                break; // Exit the loop early if any object is not placed
            }
        }
        CheckingObjectsPlaced1(allObjectsPlacedinQuestion1);
    }
    public void CheckAllObjectsPlacedinQuestion2()
    {
        //make allobjects bool true
        allObjectsPlacedinQuestion2 = true;

        //check whether each gameobjects are placed or not
        foreach (GameObject slot in Question2)
        {
            DragDrop dragDrop = slot.GetComponentInChildren<DragDrop>();
            //checks the all isDraggable bool is tru
            if (dragDrop.isDraggable)
            {
                allObjectsPlacedinQuestion2 = false;
                break; // Exit the loop early if any object is not placed
            }
        }
        CheckingObjectsPlaced2(allObjectsPlacedinQuestion2);
    }
    public void CheckAllObjectsPlacedinQuestion3()
    {
        //make allobjects bool true
        allObjectsPlacedinQuestion3 = true;

        //check whether each gameobjects are placed or not
        foreach (GameObject slot in Question3)
        {
            DragDrop dragDrop = slot.GetComponentInChildren<DragDrop>();
            //checks the all isDraggable bool is tru
            if (dragDrop.isDraggable)
            {
                allObjectsPlacedinQuestion3 = false;
                break; // Exit the loop early if any object is not placed
            }
        }
        CheckingObjectsPlaced3(allObjectsPlacedinQuestion3);
    }
    public void CheckAllObjectsPlacedinQuestion4()
    {
        //make allobjects bool true
        allObjectsPlacedinQuestion4 = true;

        //check whether each gameobjects are placed or not
        foreach (GameObject slot in Question4)
        {
            DragDrop dragDrop = slot.GetComponentInChildren<DragDrop>();
            //checks the all isDraggable bool is tru
            if (dragDrop.isDraggable)
            {
                allObjectsPlacedinQuestion4 = false;
                break; // Exit the loop early if any object is not placed
            }
        }
        CheckingObjectsPlaced4(allObjectsPlacedinQuestion4);
    }
    public void CheckAllObjectsPlacedinQuestion5()
    {
        //make allobjects bool true
        allObjectsPlacedinQuestion5 = true;

        //check whether each gameobjects are placed or not
        foreach (GameObject slot in Question5)
        {
            DragDrop dragDrop = slot.GetComponentInChildren<DragDrop>();
            //checks the all isDraggable bool is tru
            if (dragDrop.isDraggable)
            {
                allObjectsPlacedinQuestion5 = false;
                break; // Exit the loop early if any object is not placed
            }
        }
        CheckingObjectsPlaced5(allObjectsPlacedinQuestion5);
    }
    public void CheckAllObjectsPlacedinQuestion6()
    {
        //make allobjects bool true
        allObjectsPlacedinQuestion6 = true;

        //check whether each gameobjects are placed or not
        foreach (GameObject slot in Question6)
        {
            DragDrop dragDrop = slot.GetComponentInChildren<DragDrop>();
            //checks the all isDraggable bool is tru
            if (dragDrop.isDraggable)
            {
                allObjectsPlacedinQuestion6 = false;
                break; // Exit the loop early if any object is not placed
            }
        }
        CheckingObjectsPlaced6(allObjectsPlacedinQuestion6);
    }
    public void CheckAllObjectsPlacedinQuestion7()
    {
        //make allobjects bool true
        allObjectsPlacedinQuestion7 = true;

        //check whether each gameobjects are placed or not
        foreach (GameObject slot in Question7)
        {
            DragDrop dragDrop = slot.GetComponentInChildren<DragDrop>();
            //checks the all isDraggable bool is tru
            if (dragDrop.isDraggable)
            {
                allObjectsPlacedinQuestion7 = false;
                break; // Exit the loop early if any object is not placed
            }
        }
        CheckingObjectsPlaced7(allObjectsPlacedinQuestion7);
    }
    public void CheckAllObjectsPlacedinQuestion8()
    {
        //make allobjects bool true
        allObjectsPlacedinQuestion8 = true;

        //check whether each gameobjects are placed or not
        foreach (GameObject slot in Question8)
        {
            DragDrop dragDrop = slot.GetComponentInChildren<DragDrop>();
            //checks the all isDraggable bool is tru
            if (dragDrop.isDraggable)
            {
                allObjectsPlacedinQuestion8 = false;
                break; // Exit the loop early if any object is not placed
            }
        }
        CheckingObjectsPlaced8(allObjectsPlacedinQuestion8);
    }
    public void CheckAllObjectsPlacedinQuestion9()
    {
        //make allobjects bool true
        allObjectsPlacedinQuestion9 = true;

        //check whether each gameobjects are placed or not
        foreach (GameObject slot in Question9)
        {
            DragDrop dragDrop = slot.GetComponentInChildren<DragDrop>();
            //checks the all isDraggable bool is tru
            if (dragDrop.isDraggable)
            {
                allObjectsPlacedinQuestion9 = false;
                break; // Exit the loop early if any object is not placed
            }
        }
        CheckingObjectsPlaced9(allObjectsPlacedinQuestion9);
    }
    public void CheckAllObjectsPlacedinQuestion10()
    {
        //make allobjects bool true
        allObjectsPlacedinQuestion10 = true;

        //check whether each gameobjects are placed or not
        foreach (GameObject slot in Question10)
        {
            DragDrop dragDrop = slot.GetComponentInChildren<DragDrop>();
            //checks the all isDraggable bool is tru
            if (dragDrop.isDraggable)
            {
                allObjectsPlacedinQuestion10 = false;
                break; // Exit the loop early if any object is not placed
            }
        }
        CheckingObjectsPlaced10(allObjectsPlacedinQuestion10);
    }
    public void CheckAllObjectsPlacedinQuestion11()
    {
        //make allobjects bool true
        allObjectsPlacedinQuestion11 = true;

        //check whether each gameobjects are placed or not
        foreach (GameObject slot in Question11)
        {
            DragDrop dragDrop = slot.GetComponentInChildren<DragDrop>();
            //checks the all isDraggable bool is tru
            if (dragDrop.isDraggable)
            {
                allObjectsPlacedinQuestion11 = false;
                break; // Exit the loop early if any object is not placed
            }
        }
        CheckingObjectsPlaced11(allObjectsPlacedinQuestion11);
    }
    public void CheckAllObjectsPlacedinQuestion12()
    {
        //make allobjects bool true
        allObjectsPlacedinQuestion12 = true;

        //check whether each gameobjects are placed or not
        foreach (GameObject slot in Question12)
        {
            DragDrop dragDrop = slot.GetComponentInChildren<DragDrop>();
            //checks the all isDraggable bool is tru
            if (dragDrop.isDraggable)
            {
                allObjectsPlacedinQuestion12 = false;
                break; // Exit the loop early if any object is not placed
            }
        }
        CheckingObjectsPlaced12(allObjectsPlacedinQuestion12);
    }
    private void CheckingObjectsPlaced1(bool allObjectsPlacedinQuestion1)
    {
        //if all objects are placed than print and load next scence and add a score
        if (allObjectsPlacedinQuestion1)
        {

            Debug.Log("All objects are placed in slots!");
            questionManager.panel2.SetActive(true);
            //Load Next Scene 
            //Add a score

        }
    }
    private void CheckingObjectsPlaced2(bool allObjectsPlacedinQuestion1)
    {
        //if all objects are placed than print and load next scence and add a score
        if (allObjectsPlacedinQuestion1)
        {

            Debug.Log("All objects are placed in slots!");
            questionManager.panel3.SetActive(true);
            //Load Next Scene 
            //Add a score

        }
    }
    private void CheckingObjectsPlaced3(bool allObjectsPlacedinQuestion1)
    {
        //if all objects are placed than print and load next scence and add a score
        if (allObjectsPlacedinQuestion3)
        {

            Debug.Log("All objects are placed in slots!");
            questionManager.panel4.SetActive(true);
            //Load Next Scene 
            //Add a score

        }
    }
    private void CheckingObjectsPlaced4(bool allObjectsPlacedinQuestion1)
    {
        //if all objects are placed than print and load next scence and add a score
        if (allObjectsPlacedinQuestion4)
        {

            Debug.Log("All objects are placed in slots!");
            questionManager.panel5.SetActive(true);
            //Load Next Scene 
            //Add a score

        }
    }
    private void CheckingObjectsPlaced5(bool allObjectsPlacedinQuestion1)
    {
        //if all objects are placed than print and load next scence and add a score
        if (allObjectsPlacedinQuestion5)
        {

            Debug.Log("All objects are placed in slots!");
            questionManager.panel6.SetActive(true);
            //Load Next Scene 
            //Add a score

        }
    }
    private void CheckingObjectsPlaced6(bool allObjectsPlacedinQuestion1)
    {
        //if all objects are placed than print and load next scence and add a score
        if (allObjectsPlacedinQuestion6)
        {

            Debug.Log("All objects are placed in slots!");
            questionManager.panel7.SetActive(true);
            //Load Next Scene 
            //Add a score

        }
    }
    private void CheckingObjectsPlaced7(bool allObjectsPlacedinQuestion1)
    {
        //if all objects are placed than print and load next scence and add a score
        if (allObjectsPlacedinQuestion7)
        {

            Debug.Log("All objects are placed in slots!");
            questionManager.panel8.SetActive(true);
            //Load Next Scene 
            //Add a score

        }
    }
    private void CheckingObjectsPlaced8(bool allObjectsPlacedinQuestion1)
    {
        //if all objects are placed than print and load next scence and add a score
        if (allObjectsPlacedinQuestion8)
        {

            Debug.Log("All objects are placed in slots!");
            questionManager.panel9.SetActive(true);
            //Load Next Scene 
            //Add a score

        }
    }
    private void CheckingObjectsPlaced9(bool allObjectsPlacedinQuestion1)
    {
        //if all objects are placed than print and load next scence and add a score
        if (allObjectsPlacedinQuestion9)
        {

            Debug.Log("All objects are placed in slots!");
            questionManager.panel10.SetActive(true);
            //Load Next Scene 
            //Add a score

        }
    }
    private void CheckingObjectsPlaced10(bool allObjectsPlacedinQuestion1)
    {
        //if all objects are placed than print and load next scence and add a score
        if (allObjectsPlacedinQuestion10)
        {

            Debug.Log("All objects are placed in slots!");
            questionManager.panel11.SetActive(true);
            //Load Next Scene 
            //Add a score

        }
    }
    private void CheckingObjectsPlaced11(bool allObjectsPlacedinQuestion1)
    {
        //if all objects are placed than print and load next scence and add a score
        if (allObjectsPlacedinQuestion11)
        {

            Debug.Log("All objects are placed in slots!");
            questionManager.panel12.SetActive(true);
            //Load Next Scene 
            //Add a score

        }
    }
    private void CheckingObjectsPlaced12(bool allObjectsPlacedinQuestion1)
    {
        //if all objects are placed than print and load next scence and add a score
        if (allObjectsPlacedinQuestion12)
        {

            Debug.Log("All objects are placed in slots!");
            questionManager.Endpanel.SetActive(true);
            //Load Next Scene 
            //Add a score

        }
    }

}
