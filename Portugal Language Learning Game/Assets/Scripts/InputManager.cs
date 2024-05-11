using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class InputManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TMP_InputField usernametextKey;
    [SerializeField] private TMP_InputField passwordtextKey;
    [SerializeField] private Keyboard keyboard;

    private TMP_InputField currentInputField;

    // Start is called before the first frame update
    void Start()
    {
        keyboard.onBackSpacePressed += BackSpacePressedCallBack;
        keyboard.onKeyPressed += keyPressedCallBack;

        currentInputField = usernametextKey;
    }
  
    
    public void BackSpacePressedCallBack()
    {
        if(currentInputField.text.Length>0)
        {
            currentInputField.text = currentInputField.text.Substring(0, currentInputField.text.Length-1);
        }
    }

    public void keyPressedCallBack(char key)
    {
        currentInputField.text += key.ToString();
    }

    public void SetCurrentInputField(TMP_InputField inputField)
    {
        currentInputField = inputField;
    }
}
