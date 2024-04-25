using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class PlayerGame : MonoBehaviour
{
    public TMP_Text UserNameText;
    public TMP_Text UserScoreText;
    int CurrentPlayerScore;
    private void Start()
    {
        var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
        string CurrentPlayerUsername = CurrentPlayer.GetComponent<CurrentPlayer>().Username;
        CurrentPlayerScore = CurrentPlayer.GetComponent<CurrentPlayer>().Score*5;

        UserNameText.text =  CurrentPlayerUsername  ;
        UserScoreText.text = CurrentPlayerScore.ToString(); 
    }

    public void StartGame()
    {  

        FindObjectOfType<SceneSwitch>().LoadPlayGameScene();
    }
    public void RestartGame()
    {
        var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
       
        CurrentPlayer.GetComponent<CurrentPlayer>().Score =0;
        CurrentPlayerScore = CurrentPlayer.GetComponent<CurrentPlayer>().Score;

        FindObjectOfType<SceneSwitch>().LoadPlayGameScene();


    }
  
    public void SignOut()
    {
        var CurrentPlayers = GameObject.FindGameObjectsWithTag("CurrentPlayer");
        foreach (var item in CurrentPlayers)
        {
            Destroy(item);
        }
        FindObjectOfType<SceneSwitch>().LoadWelcomeScene();
    }
}
