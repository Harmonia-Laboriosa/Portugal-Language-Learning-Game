using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreenManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        // Activate the loading screen
        loadingScreen.SetActive(true);

        // Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting
        while (!asyncOperation.isDone)
        {
            // Output the current progress
            loadingBar.value = asyncOperation.progress;

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                // Wait for the user to press the space key to activate the scene
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

        // Deactivate the loading screen
        loadingScreen.SetActive(false);
    }
}
