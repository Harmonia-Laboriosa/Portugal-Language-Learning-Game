using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
public class SManage : MonoBehaviour
{
    public static SManage instance;

    public int[] LevelScore ;
    public int Level1Score ;
    public int totalScore=0;
    public int score = 0; // Current score
    public TMP_Text scoreText; // Reference to the UI text displaying the score

    void Awake()
    {
        if (instance == null)
        {
            // If no instance exists, set this as the instance and don't destroy it when loading new scenes
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this GameObject
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreText(); // Update the score text on start
    }

    public int GetScore()
    {
        return score;
    }

    // Method to increase the score by a specified amount
    public void IncreaseScore(int amount)
    {
        score += amount;
        
        UpdateScoreText();
        
       // Update the score text after increasing the score
    }

    public void ResetScore()
    {
        score = 0;
    }

    // Method to decrease the score by a specified amount
/*
    public void DecreaseScore(int amount)
    {
        score -= amount;
        // Ensure the score doesn't go below zero
        score = Mathf.Max(score, 0);
        UpdateScoreText(); // Update the score text after decreasing the score
    }
*/

    // Method to update the score text UI
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString(); // Update the text to display the current score
            Debug.Log(" "+ "The current score : " + score);
        }
    }
    IEnumerator SavePlayerScore()
    {
        var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");

        string username = CurrentPlayer.GetComponent<CurrentPlayer>().Username;
        string scoreFromPlayer = CurrentPlayer.GetComponent<CurrentPlayer>().Score.ToString();
        WWWForm scoreForm = new WWWForm();
        scoreForm.AddField("apppassword", "thisisfromtheapp");
        scoreForm.AddField("username", username);
        scoreForm.AddField("score", scoreFromPlayer);
        UnityWebRequest updatePlayerRequest = UnityWebRequest.Post("http://ec2-54-172-175-103.compute-1.amazonaws.com/cruds/updateplayerscore.php", scoreForm);
        yield return updatePlayerRequest.SendWebRequest();
        if (updatePlayerRequest.error == null)
        {

            string result = updatePlayerRequest.downloadHandler.text;
            Debug.Log(result);
            if (result == "0")
            {
                //FindObjectOfType<SceneSwitch>().LoadGameScene();
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
