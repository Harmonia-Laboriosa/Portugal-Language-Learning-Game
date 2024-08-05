using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelUpdate : MonoBehaviour
{
    int score;
    public Button[] Level;
    public GameObject[] BlurredLevel;
    public GameObject[] UnlockedLevel;

    // Start is called before the first frame update
    void Update()
    {
        var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
        if (CurrentPlayer != null)
        {
            score = CurrentPlayer.GetComponent<CurrentPlayer>().Score;
            switch (score)
            {
                case 1:
                    UpdateLevelButtons(2);
                    break;
                case 2:
                    UpdateLevelButtons(3);
                    break;
                case 3:
                    UpdateLevelButtons(4);
                    break;
                case 4:
                    UpdateLevelButtons(5);
                    break;
                case 5:
                    UpdateLevelButtons(6);
                    break;
                case 6:
                    UpdateLevelButtons(7);
                    break;
                case 7:
                    UpdateLevelButtons(8);
                    break;
                case 8:
                    UpdateLevelButtons(9);
                    break;
                case 9:
                    UpdateLevelButtons(10);
                    break;
                default:
                    UpdateLevelButtons(1);
                    Debug.Log("Only level 1 Active");
                    break;
            }
        }
    }

    void UpdateLevelButtons(int x)
    {
        for (int i = 0; i < x; i++)
        {
            Level[i].GetComponent<Button>().interactable = true;
            BlurredLevel[i].SetActive(false);
        }
    }
}
