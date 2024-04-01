using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Animation_NPC1 : MonoBehaviour
{
    public Animator npc_anim;
    // Start is called before the first frame update
    void Start()
    {
        npc_anim = this.gameObject.GetComponent<Animator>();
    }

    public void idleTalk()
    {
        npc_anim.SetBool("talk", true);
    }

    public void idleNotTalk()
    {
        npc_anim.SetBool("talk", false);
    }
}
