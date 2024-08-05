using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
public class UpdateData : MonoBehaviour
{
    public TMP_Text UserNameText;
    public TMP_Text UserScoreText;
    int CurrentPlayerScore;
    public void Start()
    {
        var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
        string CurrentPlayerUsername = CurrentPlayer.GetComponent<CurrentPlayer>().Username;
        UserNameText.text = "Welcome " + CurrentPlayerUsername;
       
    }
    private void Update()
    {
        var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
      
        CurrentPlayerScore = CurrentPlayer.GetComponent<CurrentPlayer>().Score;

        UserScoreText.text = CurrentPlayerScore.ToString();
    }
}
