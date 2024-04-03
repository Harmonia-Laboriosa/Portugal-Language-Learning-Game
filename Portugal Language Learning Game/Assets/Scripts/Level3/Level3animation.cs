using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3animation : MonoBehaviour
{
    Animator m_Animator;
    RectTransform pos;
    bool m_Jump;
    Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Jump = false;
        pos = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Jump)
        {
            pos.position = Vector3.Lerp(pos.position, targetPosition, Time.deltaTime*2f);
        }
    }

    public void RightJump()
    {
        if (!m_Jump)
        {
            m_Jump = true; // Set m_Jump to true only if it's not already jumping
            m_Animator.SetBool("isJumpingRight", true);
            float newX = pos.position.x + 350.0f;
            targetPosition = new Vector3(newX, pos.position.y, pos.position.z);
            StartCoroutine("IsNotJump");
        }
    }

    IEnumerator IsNotJump()
    {
        yield return new WaitForSeconds(1f);
        m_Animator.SetBool("isJumpingRight", false);
        m_Jump = false; // Reset m_Jump after animation completes
    }
}
