using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class LoadingScreen : MonoBehaviour
{
    public Slider progressBar; 
    public string sceneToLoad;

    private float targetProgress = 1f;
    private float currentProgress = 0f; 
   [SerializeField]
    private float timeToFill = 3f; 
   
    void Start()
    {
      
        if (progressBar != null)
            progressBar.value = 0f;
        StartCoroutine(UpdateLoadingProgress());
    }

   
    void Update()
    {
     
        currentProgress = Mathf.Min(currentProgress, targetProgress);

   
        if (progressBar != null)
            progressBar.value = currentProgress;

   
        if (currentProgress >= 1f)
        {
         
            SceneManager.LoadScene(sceneToLoad);
        }
    }

  
    IEnumerator UpdateLoadingProgress()
    {
        float timer = 0f;
        while (timer < timeToFill)
        {
            timer += Time.deltaTime;
            currentProgress = timer / timeToFill;
            yield return null;
        }
        currentProgress = 1f; 
    }
}
