using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject firelit;
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
        firelit.SetActive(true);
    }

}
