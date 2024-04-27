using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    private static bool _instanceExists = false;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelManager>();
                if (_instance == null && !_instanceExists)
                {
                    GameObject singleton = new GameObject(typeof(LevelManager).Name);
                    _instance = singleton.AddComponent<LevelManager>();
                    _instanceExists = true;
                }
            }
            return _instance;
        }
    }

   /* private void Awake()
    {
        
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        
            


     }*/

        public void Replay()
    {
        int currentscene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentscene);
    }
    public void LoadNextLevel()
    {
        int currentscene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentscene+1);
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

}
