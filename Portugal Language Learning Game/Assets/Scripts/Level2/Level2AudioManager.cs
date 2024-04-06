using UnityEngine;

public class Level2AudioManager : MonoBehaviour
{
    public AudioClip typingSound; // Assign your typing sound clip in the Unity Editor
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();

        // Assign the typing sound clip to the AudioSource
        audioSource.clip = typingSound;
    }

    // Method to play the typing sound
    public void playTypesound()
    {
        // Check if the AudioSource is not playing already to avoid overlap
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Method to stop playing the typing sound
    public void stopTypesound()
    {
        audioSource.Stop();
    }
}
