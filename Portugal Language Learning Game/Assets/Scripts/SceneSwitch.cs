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

    public void LoadScene_Cutscene()
    {
        SceneManager.LoadScene("Cut Scene");
    }
    public void LoadWelcomeScene()
    {
        SceneManager.LoadScene("WelcomeScene");
    }

    public void LoadNewUserScene()
    {
        SceneManager.LoadScene("NewUserPlayer");
    }

    public void LoadLoginUserScene()
    {
        SceneManager.LoadScene("LoginUser");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("PlayerWelcomeScene 1");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadPlayGameScene()
    {
        SceneManager.LoadScene("Cut Scene");
    }

    public void LoadVictoryCardScene()
    {
        SceneManager.LoadScene("VictoryCard");
    }




}
