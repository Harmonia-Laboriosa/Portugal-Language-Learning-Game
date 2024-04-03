using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Animation : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playAnimation(int animationNum)
    {
        animator.SetInteger("rockfall", animationNum);
    }

}
