using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandHour : MonoBehaviour
{
    public string tagH;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == tagH)
        {
            Level5manager level5 = FindObjectOfType<Level5manager>();
            level5.tagFromCollissionHour = true;
        }
        else
        {
            Level5manager level5 = FindObjectOfType<Level5manager>();
            level5.tagFromCollissionHour = false;
        }
        
    }
    
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == tagH)
        {
            Level5manager level5 = FindObjectOfType<Level5manager>();
            level5.tagFromCollissionHour = true;
        }
        else
        {
            Level5manager level5 = FindObjectOfType<Level5manager>();
            level5.tagFromCollissionHour = false;
        }
    }
    */

}
