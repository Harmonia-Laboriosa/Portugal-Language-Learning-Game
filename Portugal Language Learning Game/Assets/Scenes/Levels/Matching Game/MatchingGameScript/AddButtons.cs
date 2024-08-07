using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour
{
    [SerializeField] private Transform puzzleField;
    [SerializeField] private GameObject[] btn;

    private void Awake()
    {
        for (int i = 0; i < 16; i++)
        {
            GameObject button = Instantiate(btn[i]);
            button.name = "" + i;
            button.transform.SetParent(puzzleField, false);
        }
    }
}
