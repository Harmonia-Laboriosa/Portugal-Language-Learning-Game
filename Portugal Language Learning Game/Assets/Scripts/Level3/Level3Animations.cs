using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Animations : MonoBehaviour
{

    Animator m_Animator;
    public bool m_Jump;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_Jump = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RightJump()
    {
            m_Animator.SetBool("rightJump", true);
            StartCoroutine("IsNotJump");        
    }
    public void LeftJump()
    {
        m_Animator.SetBool("leftJump", true);
        StartCoroutine("IsNotJump");
    }

    public IEnumerator IsNotJump()
    {
        yield return new WaitForSeconds(1.5f);
        m_Animator.SetBool("rightJump", false);
        m_Animator.SetBool("leftJump", false);
    }
}
