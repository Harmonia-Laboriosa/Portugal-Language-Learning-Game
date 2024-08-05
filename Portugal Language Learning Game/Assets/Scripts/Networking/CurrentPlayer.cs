using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPlayer : MonoBehaviour
{
    // Properties to hold player data
    public string Username;
    public string Email;
    public int Id;
    public string accessToken;
    public int Score;

    // Ensure only one instance of CurrentPlayer exists
    private void Awake()
    {
        var players = FindObjectsOfType<CurrentPlayer>();
        if (players.Length > 1)
        {
            Destroy(gameObject); // Destroy any duplicate instances
            return;
        }

        DontDestroyOnLoad(gameObject); // Persist across scenes
    }
}
