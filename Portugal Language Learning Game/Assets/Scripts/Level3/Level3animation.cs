using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3animation : MonoBehaviour
{
    Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RightJump(int num)
    {
        m_Animator.SetInteger("isJump", num);
    }

}
