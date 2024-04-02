using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3animation : MonoBehaviour
{

    Animator m_Animator;
    RectTransform pos;
    public bool m_Jump;
    Vector3 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = this.gameObject.GetComponent<Animator>();
        m_Jump = false;
        pos = this.gameObject.GetComponent<RectTransform>();
        currentPosition = pos.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RightJump()
    {
        Vector3 currentPosition = pos.position;
        m_Animator.SetBool("isJumpingRight", true);
        float newX = currentPosition.x + 120.0f;
        pos.position = new Vector3(newX, pos.position.y, pos.position.z);
        StartCoroutine("IsNotJump");
    }

    public IEnumerator IsNotJump()
    {
        yield return new WaitForSeconds(1.5f);
        m_Animator.SetBool("isJumpingRight", false);

    }
}
