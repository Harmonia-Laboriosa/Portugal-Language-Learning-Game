using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Key : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TMP_Text keyText;
    private char key;

    [Header("Settings")]
    [SerializeField] private bool isBackSpace;

    public void SetKey(char key)
    {
        this.key = key;
        keyText.text = key.ToString();
    }

    public Button GetButton()
    {
        return GetComponent<Button>();  
    }

    public bool  IsBackSpace()
    { 
        return isBackSpace; 
    }

}
