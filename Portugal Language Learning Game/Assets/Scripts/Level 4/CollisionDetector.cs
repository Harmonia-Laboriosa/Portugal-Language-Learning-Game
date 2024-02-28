using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{

    // Function called when a collision occurs
    private void OnCollisionEnter(Collision2D collision)
    {
            Debug.Log("Collided with: " + collision.gameObject.tag);   
    }

}
