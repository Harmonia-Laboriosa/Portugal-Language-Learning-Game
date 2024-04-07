using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collissionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        Debug.Log(gameObject.tag);
        Level4Manager level4 = FindObjectOfType<Level4Manager>();
        level4.tagFromCollission = gameObject.tag;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Level4Manager level4 = FindObjectOfType<Level4Manager>();
        level4.tagFromCollission = gameObject.tag;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Level4Manager level4 = FindObjectOfType<Level4Manager>();
        level4.tagFromCollission = gameObject.tag;
    }

}
