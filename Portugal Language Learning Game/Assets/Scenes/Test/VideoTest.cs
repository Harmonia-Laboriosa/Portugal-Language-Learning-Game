using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;

public class VideoTest : MonoBehaviour
{
    [SerializeField] string videoname;
    [SerializeField] GameObject loadingScreen; // Reference to the loading screen
    [SerializeField] Slider loadingBar; // Reference to the loading bar (optional)

    void Start()
    {
        PlayVideo();
    }

    public void PlayVideo()
    {
        StartCoroutine(PrepareVideo());
    }

    IEnumerator PrepareVideo()
    {
        // Activate the loading screen
        loadingScreen.SetActive(true);

        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer)
        {
            string videoPath = "/StreamingAssets/" + videoname;
            /*string videoPath = Application.streamingAssetsPath + "/" + videoname;*/
            Debug.Log(videoPath);
            videoPlayer.url = videoPath;
            videoPlayer.Prepare();

            // Wait until the video player is prepared
            while (!videoPlayer.isPrepared)
            {
                // Update the loading bar progress (optional)
                if (loadingBar != null)
                {
                    // Note: VideoPlayer doesn't provide progress information during preparation,
                    // so this part is for illustration purposes. You might need a custom solution
                    // if you require a real progress bar.
                    loadingBar.value = Mathf.Clamp01((float)videoPlayer.frame / (float)videoPlayer.frameCount);
                }

                yield return null;
            }

            // Deactivate the loading screen
            loadingScreen.SetActive(false);

            // Play the video
            videoPlayer.Play();
        }
    }
}
