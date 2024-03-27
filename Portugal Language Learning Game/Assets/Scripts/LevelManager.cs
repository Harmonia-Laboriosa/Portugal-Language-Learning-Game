using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelManager>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(LevelManager).Name);
                    _instance = singleton.AddComponent<LevelManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        
    }

    public void Replay()
    {
        int currentscene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentscene);
    }
    public void LoadLevel2(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }

    public void LoadLevel3(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }

    public void LoadLevel4(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }

    public void LoadLevel5(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }

    public void LoadLevel6(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }

    public void LoadLevel7(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }

    public void LoadLevel8(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }

}
