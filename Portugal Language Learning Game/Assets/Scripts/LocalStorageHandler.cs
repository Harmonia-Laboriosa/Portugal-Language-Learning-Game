using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

public class LocalStorageHandler : MonoBehaviour
{
    public TextMeshProUGUI textMeshProText;
    public GameObject currenPlayerObject;

    private void Awake()
    {
        var CurrentPlayers = GameObject.FindGameObjectsWithTag("CurrentPlayer");
        foreach (var item in CurrentPlayers)
        {
            Destroy(item);
        }
    }

    // Callback function to receive data from JavaScript
    public void ReceiveJsonDataFromLocalStorage(string jsonData)
    {
        // Deserialize the JSON string to AuthData object
        AuthData authData = JsonUtility.FromJson<AuthData>(jsonData);

        var currentPlayer = Instantiate(currenPlayerObject, new Vector3(0, 0, 0), Quaternion.identity);
        currentPlayer.GetComponent<CurrentPlayer>().Username = authData.user.username;
        currentPlayer.GetComponent<CurrentPlayer>().Id = authData.user.id;
        currentPlayer.GetComponent<CurrentPlayer>().Email = authData.user.email;
        currentPlayer.GetComponent<CurrentPlayer>().accessToken = authData.accessToken;
        currentPlayer.GetComponent<CurrentPlayer>().Score = authData.user.game_score;

        // Handle the received data
        Debug.Log("Refresh Token: " + authData.refreshToken);
        Debug.Log("Access Token: " + authData.accessToken);
        Debug.Log("User ID: " + authData.user.id);
        Debug.Log("Username: " + authData.user.username);
        Debug.Log("Email: " + authData.user.email);
        Debug.Log("First Name: " + authData.user.first_name);
        Debug.Log("Last Name: " + authData.user.last_name);
        Debug.Log("Role: " + authData.user.role);

        textMeshProText.text = "User ID: " + authData.user.id + "Game Score: " + authData.user.game_score + "\n" +
                               "Username: " + authData.user.username + "\n" +
                               "Email: " + authData.user.email + "\n" +
                               "First Name: " + authData.user.first_name + "\n" +
                               "Last Name: " + authData.user.last_name + "\n" +
                               "Role: " + authData.user.role;
    }
}