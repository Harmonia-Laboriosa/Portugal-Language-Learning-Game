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
 
    private string apiUrl= "https://api.harmonialaboriosa.com/account/user/update/"; // API endpoint for login
    private string jwtToken; // Store the JWT token after login


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
            scoreText.text = "Points: " + score.ToString(); // Update the text to display the current score
            Debug.Log(" "+ "The current Points : " + score);
        }
    }
    IEnumerator SavePlayerScore()
    {
        var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");

        if (CurrentPlayer != null)
        {
            var playerComponent = CurrentPlayer.GetComponent<CurrentPlayer>();
            int score = playerComponent.Score;
            string accessToken = playerComponent.accessToken;
            jwtToken = accessToken;

            // Manually create the form data as a URL-encoded string
            string formData = "game_score=" + UnityWebRequest.EscapeURL(score.ToString());

            // Convert the string to a byte array
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(formData);

            // Create the UnityWebRequest with PUT method
            UnityWebRequest www = UnityWebRequest.Put(apiUrl, bodyRaw);

            // Set the appropriate headers
            if (!string.IsNullOrEmpty(jwtToken))
            {
                www.SetRequestHeader("Authorization", "Bearer " + jwtToken);
            }
            www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log($"Error: {www.error}");
            }
            else
            {
                Debug.Log($"Score updated to {score}.");
                
                Debug.Log("Updated score: " + score);
            }
        }
        else
        {
            Debug.Log("Error: CurrentPlayer not found.");
        }

    }
}
