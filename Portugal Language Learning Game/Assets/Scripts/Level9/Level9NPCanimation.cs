using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level9NPCanimation : MonoBehaviour
{
    public Animator NPCanim;
    public Level9Conversation dialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NPCtalk()
    {
        NPCanim.SetBool("npctalk",true);
        dialogue.ConversationStart();
    }
}
