using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UIElements;

public class FinalCutsceneLoader : MonoBehaviour
{
    public UnityEngine.UI.Slider loadBar;
    public GameObject endScreen;
    public float targetProgress = 1f;
    public float currentProgress = 0f;
    [SerializeField]
    private float timeToFill = 3f;

    // Start is called before the first frame update
    void Start()
    {
        endScreen.SetActive(false);
        if (loadBar != null)
            loadBar.value = 0f;
        StartCoroutine(UpdateLoadingProgress());
    }

    // Update is called once per frame
    void Update()
    {
        currentProgress = Mathf.Min(currentProgress, targetProgress);

        if (loadBar != null)
            loadBar.value = currentProgress;

        if (currentProgress == targetProgress) 
        {
            endScreen.SetActive(true); 
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
