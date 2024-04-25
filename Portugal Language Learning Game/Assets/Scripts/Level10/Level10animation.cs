using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level10animation : MonoBehaviour
{
    public Animator player;
    public Animator guard1;
    public Animator guard2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sword()
    {
        guard1.SetBool("sword1", true);
        guard2.SetBool("sword2", true);
    }
}
