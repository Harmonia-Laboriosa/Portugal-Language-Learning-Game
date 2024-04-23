using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CreatePlayer : MonoBehaviour
{
    public TMP_InputField UserNameInput;
    public TMP_InputField Email;
    public TMP_InputField Password;
    public Button RegisterButton;
    public TMP_Text RegisterText;

    public void RegisterNewPlayer()
    {
        RegisterButton.interactable = false;
        if (UserNameInput.text.Length < 5) // Changed to Length (case-sensitive) and added a missing parenthesis
        {
            ErrorMessage("UserName is not filled out");
        }
        else if (Email.text.Length < 5) // Changed to Length (case-sensitive) and added a missing parenthesis
        {
            ErrorMessage("Email is not filled out");
        }
       else  if (Password.text.Length < 5) // Changed to Length (case-sensitive) and added a missing parenthesis
        {
            ErrorMessage("Password is not filled out");
        }
        else
        {
            SetButtonToSending();
            StartCoroutine(CreatePlayerPostRequest());
        }
    }

    public void ErrorMessage(string message)
    {
        RegisterButton.GetComponent<Image>().color = Color.red;
        RegisterText.text = message;
        RegisterText.fontSize = 24; // Corrected to fontSize (case-sensitive)
        Debug.Log(message);
    }
    public void ResetRegister()
    {
        RegisterButton.GetComponent<Image>().color = Color.white;
        RegisterText.text = "Register";
        RegisterText.fontSize = 72; // Corrected to fontSize (case-sensitive)
        RegisterButton.interactable = true;
    }

    public void SetButtonToSending()
    {
        RegisterButton.GetComponent<Image>().color = Color.grey;
        RegisterText.text = "Sending";
        RegisterText.fontSize = 72;
    }

    public void SetButtonToSuccess()
    {
        RegisterButton.GetComponent<Image>().color = Color.green;
        RegisterText.text = "Success";
        RegisterText.fontSize = 72;
    }

    IEnumerator CreatePlayerPostRequest()
    {
        WWWForm newPlayerInfo = new WWWForm();
        newPlayerInfo.AddField("appPassword", "thisisfromtheapp!");
        newPlayerInfo.AddField("username", UserNameInput.text);
        newPlayerInfo.AddField("email", Email.text);
        newPlayerInfo.AddField("password", Password.text);


        UnityWebRequest CreatePostRequest = UnityWebRequest.Post("http://ec2-54-172-175-103.compute-1.amazonaws.com/cruds/newplayer.php", newPlayerInfo);
        yield return CreatePostRequest.SendWebRequest();

        if (CreatePostRequest.error == null)
        {
            string response = CreatePostRequest.downloadHandler.text;
            if(response=="1" || response == "2"|| response == "4"|| response == "6")
            {
                ErrorMessage("Server Error");
            }
      
            else if (response == "3")
            {
                ErrorMessage("UserName Already exists");
            }
            else if (response == "5")
            {
                ErrorMessage("Email Already Exists");
            }
            
            else
            {
                Debug.Log("Good to go!");
                SetButtonToSuccess();
            }
           
        }
        else
        {
            Debug.Log(CreatePostRequest.error);
        }
    }
}
