using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Animation : MonoBehaviour
{
    Animator mc_anim;
    bool reachedOriginalpos=false;
    public Conversation chat;
    public GameObject dialogueimage;
    public string anamationName;
    // Start is called before the first frame update
    void Start()
    {
        this.mc_anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.mc_anim.GetCurrentAnimatorStateInfo(0).IsName(anamationName) && !reachedOriginalpos)
        {
            reachedOriginalpos = true;
            
            chat.ConversationStart();
            dialogueimage.SetActive(true);
        }
    }
}
