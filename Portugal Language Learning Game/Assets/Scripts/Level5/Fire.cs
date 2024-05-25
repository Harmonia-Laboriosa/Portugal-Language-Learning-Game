using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    public GameObject firelit;
    public Image[] images;
    public Image[] hands;
    public Level5manager level5Manager;

    public AudioSource fireSound;

    // Start is called before the first frame update
    void Start()
    {
        firelit.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fireON()
    {
        fireSound.Play();
        firelit.SetActive(true);
        for(int i=0; i< images.Length; i++)
        {
            images[i].color = Color.white;
        }
        for (int i = 0; i < hands.Length; i++)
        {
            hands[i].color = ColorUtility.TryParseHtmlString("#FF7C00", out Color color) ? color : Color.white;

        }
      
    }

}
