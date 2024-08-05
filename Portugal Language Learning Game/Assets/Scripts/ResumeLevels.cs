using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ResumeLevels : MonoBehaviour
{
    int score;
    

    // Start is called before the first frame update
    public void resume()
    {
        var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
        if (CurrentPlayer != null)
        {
            score = CurrentPlayer.GetComponent<CurrentPlayer>().Score;
            switch (score)
            {
                case 1:
                    PlayLevelScene("Instructions 2");
                    break;
                case 2:
                    PlayLevelScene("Instructions 3");
                    break;
                case 3:
                    PlayLevelScene("Instructions 4");
                    break;
                case 4:
                    PlayLevelScene("Instructions 5");
                    break;
                case 5:
                    PlayLevelScene("Instructions 6");
                    break;
                case 6:
                    PlayLevelScene("Instructions 7");
                    break;
                case 7:
                    PlayLevelScene("Instructions 8");
                    break;
                case 8:
                    PlayLevelScene("Instructions 9");
                    break;
                case 9:
                    PlayLevelScene("Instructions 10");
                    break;
                default:
                    PlayLevelScene("Cut Scene");
                    Debug.Log("Only level 1 Active");
                    break;
            }
        }
        else
        {
            Debug.Log("there is no current player");
        }
    }

void PlayLevelScene(string x)
    {
        SceneManager.LoadScene(x);
    }

}
