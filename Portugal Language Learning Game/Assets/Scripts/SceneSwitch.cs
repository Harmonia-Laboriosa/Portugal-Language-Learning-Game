using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwitch : MonoBehaviour
{

    public void OnClick_LoadScene(int x)
    {
        SceneManager.LoadScene(x);
    }
    public void LoadWelcomeScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNewUserScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLoginUserScene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(4);
    }
    public void LoadPlayGameScene()
    {
        SceneManager.LoadScene(5);
    }

   

  


}
