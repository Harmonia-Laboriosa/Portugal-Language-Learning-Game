using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Level6 : MonoBehaviour
{
    public Level7Manager level7;
    public GameObject cupBoardOpen;
    public void PartAnim()
    {
        level7.NextQuestion();
    }
    public void OnpenCupBoard()
    {
        cupBoardOpen.SetActive(true);
    }

}
