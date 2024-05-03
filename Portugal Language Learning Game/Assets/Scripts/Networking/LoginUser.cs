using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class LoginUser : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public Button loginButton;
    public TMP_Text loginButtonText;

    public GameObject currenPlayerObject;

    private void Awake()
    {
        var CurrentPlayers = GameObject.FindGameObjectsWithTag("CurrentPlayer");
        foreach (var item in CurrentPlayers)
        {
            Destroy(item);
        }
        
    }

    public void Login()
    {
        loginButton.interactable = false;
        loginButtonText.text = "Sending....";
        if (usernameInput.text.Length < 3)
        {
            ErrorLoginMessage("Ckeck Username");
        }
        else if (passwordInput.text.Length < 3)
        {
            ErrorLoginMessage("Check Password");
        }
        else
        {
            StartCoroutine(SendLoginForm());
        }

    }

    public void ErrorLoginMessage(string message)
    {
        loginButton.GetComponent<Image>().color = Color.red;
        loginButtonText.text = message;
        loginButtonText.fontSize = 20;
    }

    public void ResetLoginButton(string message)
    {
        loginButton.GetComponent <Image>().color = Color.white;
        loginButtonText.text = "Login";
        loginButtonText.fontSize = 25;
        loginButton.interactable = true;
    }

    IEnumerator SendLoginForm()
    {
        WWWForm LoginInfo = new WWWForm();
        LoginInfo.AddField("apppassword", "thisisfromtheapp");
        LoginInfo.AddField("username", usernameInput.text);
        LoginInfo.AddField("password", passwordInput.text);
        UnityWebRequest loginRequest = UnityWebRequest.Post("https://ec2-54-172-175-103.compute-1.amazonaws.com/cruds/loginuser.php", LoginInfo);
        //UnityWebRequest loginRequest = UnityWebRequest.Post("http://ec2-54-172-175-103.compute-1.amazonaws.com/cruds/loginuser.php", LoginInfo);
        //UnityWebRequest loginRequest = UnityWebRequest.Post("https://gamingpll1.s3.amazonaws.com/PHPFIles/cruds/loginuser.php", LoginInfo);
        yield return loginRequest.SendWebRequest();
        if(loginRequest.error == null)
        {
            //1,2,5 are server error
            //3 username is wrong
            //4 password is wrong
            string result = loginRequest.downloadHandler.text;
            Debug.Log(result);
            if(result == "1" || result == "2" || result == "5")
            {
                ErrorLoginMessage("Server Error");
            }
            else if(result == "3")
            {
                ErrorLoginMessage("Check Username");
            }
            else if(result == "4")
            {
                ErrorLoginMessage("Check Password");
            }
            else
            {
                var currentPlayer = Instantiate(currenPlayerObject, new Vector3(0, 0, 0), Quaternion.identity);
                currentPlayer.GetComponent<CurrentPlayer>().Username = result.Split(':')[0];
                currentPlayer.GetComponent<CurrentPlayer>().Score = int.Parse(result.Split(':')[1]);
                loginButton.GetComponent<Image>().color = Color.green;
                loginButtonText.text = "Logged in!";
                FindObjectOfType<SceneSwitch>().LoadGameScene();
            }
        }
        else
        {
            Debug.Log(loginRequest.error); 

        }
    }

}
