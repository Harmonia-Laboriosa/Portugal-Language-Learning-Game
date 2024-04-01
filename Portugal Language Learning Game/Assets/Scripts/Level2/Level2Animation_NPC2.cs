using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Animation_NPC2 : MonoBehaviour
{
    public Animator npc_anim2;
    // Start is called before the first frame update
    void Start()
    {
       //npc_anim2 = this.gameObject.GetComponent<Animator>();
    }

    public void idleTalk()
    {
        npc_anim2.SetBool("talks", true);
    }

    public void idleNotTalk()
    {
        npc_anim2.SetBool("talks", false);
    }
}
