using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteAudio : MonoBehaviour
{

    public void Mutehandler(bool mute)
    {
        if (mute)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
