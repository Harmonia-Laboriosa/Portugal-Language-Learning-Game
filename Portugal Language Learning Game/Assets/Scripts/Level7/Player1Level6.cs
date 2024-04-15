using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Level6 : MonoBehaviour
{
    public Animator NPC1anim;
    public Level7Manager Level7Manager;

    public void playNPC()
    {
        NPC1anim.SetBool("Wave", true);   
    }

    public void playGame()
    {
        Level7Manager.StartQuiz();
    }

}
