using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollissionHandMinute : MonoBehaviour
{
    public string tagM;


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag==tagM)
        {
            Level5manager level5 = FindObjectOfType<Level5manager>();
            level5.tagFromCollissionMinute = true;
        }
        else
        {
            Level5manager level5 = FindObjectOfType<Level5manager>();
            level5.tagFromCollissionMinute = false;
        }
       
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == tagM)
        {
            Level5manager level5 = FindObjectOfType<Level5manager>();
            level5.tagFromCollissionMinute = true;
        }
        else
        {
            Level5manager level5 = FindObjectOfType<Level5manager>();
            level5.tagFromCollissionMinute = false;
        }
    }
    */


}
