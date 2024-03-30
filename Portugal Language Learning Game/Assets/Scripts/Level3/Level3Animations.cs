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

    public void IsJump()
    {
            m_Animator.SetBool("jump", true);
            StartCoroutine("IsNotJump");        
            //m_Animator.SetBool("jump", false);
        
    }

    public IEnumerator IsNotJump()
    {
        yield return new WaitForSeconds(1.5f);
        m_Animator.SetBool("jump", false);
    }
}
