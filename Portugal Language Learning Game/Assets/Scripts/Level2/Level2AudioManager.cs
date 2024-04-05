using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2AudioManager : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource sources;
    // Start is called before the first frame update
    void Start()
    {
       sources = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playTypesound()
    {
        sources.clip = audioClip;
        sources.Play();
    }
    public void stopTypesound()
    {
        sources.clip = audioClip;
        sources.Stop();
    }

}
