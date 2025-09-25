using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [System.Serializable]
    public class VideoData
    {
        public string sceneName;
        public bool isStereoscopic;
        public Sprite thumbnail;
    }

    [SerializeField] private VideoData[] videos;
    [SerializeField] private ThumbnailButton[] thumbnailButtons;

    void Start()
    {
        for (int i = 0; i < thumbnailButtons.Length; i++)
        {
            int index = i; // Capture for closure
            thumbnailButtons[i].Setup(videos[index], () => LoadVideoScene(videos[index]));
        }
    }

    public void LoadVideoScene(VideoData videoData)
    {
        StartCoroutine(TransitionToVideo(videoData.sceneName));
    }

    private IEnumerator TransitionToVideo(string sceneName)
    {
        // Fade out (assuming you have a FadeController - create one or remove this line)
        // yield return FadeController.Instance.FadeOut();

        // Load video scene
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}