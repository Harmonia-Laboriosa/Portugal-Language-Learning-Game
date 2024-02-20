using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public SlotManager slotManager;
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject panel4;
    public GameObject panel5;
    public GameObject panel6;
    public GameObject panel7;
    public GameObject panel8;
    public GameObject panel9;
    public GameObject panel10;
    public GameObject panel11;
    public GameObject panel12;

    public GameObject Endpanel;

    // Update is called once per frame
    void Update()
    {
        Question1Active();
    }

    private void Question1Active()
    {
        
    if (slotManager.allObjectsPlacedinQuestion1)
        {
            panel1.SetActive(false);
            panel2.SetActive(true);
            panel2.SetActive(false);
            panel4.SetActive(false);
            panel5.SetActive(false);
            panel6.SetActive(false);
            panel7.SetActive(false);
            panel8.SetActive(false);
            panel9.SetActive(false);
            panel10.SetActive(false);
            panel11.SetActive(false);
            panel12.SetActive(false);
        }
    }
}
