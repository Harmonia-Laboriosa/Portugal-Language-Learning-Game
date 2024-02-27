using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
public class Game : MonoBehaviour
{
    public TMP_Text ScoreText;
    public GameObject CurrentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
        ScoreText.text = "Score: " + CurrentPlayer.GetComponent<CurrentPlayer>().Score.ToString();

    }

    public void Add10Points()
    {
        CurrentPlayer.GetComponent<CurrentPlayer>().Score += 10;
        UpdateScoreText();
    }

    public void Add100Points()
    {
        CurrentPlayer.GetComponent<CurrentPlayer>().Score += 100;
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        ScoreText.text = "Score: " + CurrentPlayer.GetComponent <CurrentPlayer>().Score.ToString();
    }

    public void Endgame()
    {
        StartCoroutine(SavePlayerScore());
    }

    IEnumerator SavePlayerScore()
    {
        string username = CurrentPlayer.GetComponent<CurrentPlayer>().Username;
        string scoreFromPlayer = CurrentPlayer.GetComponent<CurrentPlayer>().Score.ToString();
        WWWForm scoreForm = new WWWForm();
        scoreForm.AddField("apppassword", "thisisfromtheapp");
        scoreForm.AddField("username", username);
        scoreForm.AddField("score", scoreFromPlayer);
        UnityWebRequest updatePlayerRequest = UnityWebRequest.Post("http://ec2-34-201-217-216.compute-1.amazonaws.com/cruds/updateplayerscore.php", scoreForm);
        yield return updatePlayerRequest.SendWebRequest();
        if(updatePlayerRequest.error == null)
        {
            
            string result = updatePlayerRequest.downloadHandler.text;
            Debug.Log(result);
            if(result == "0")
            {
                FindObjectOfType<SceneSwitch>().LoadGameScene();
            }
            else
            {
                Debug.Log("error");
            }
        }
        else
        {
            Debug.Log(updatePlayerRequest.error);
        }

    }
}
