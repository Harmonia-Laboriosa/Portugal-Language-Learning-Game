using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeManger : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioSource volumeAudio;

    // Start is called before the first frame update
    void Start()
    {
        volumeAudio.volume = 0.05f;
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.05f);
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        volumeAudio.volume = volumeSlider.value;
        Save();
    }

    public void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
