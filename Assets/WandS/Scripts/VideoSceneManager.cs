using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip videoClip;
    [SerializeField] private bool isStereoscopic;
    [SerializeField] private Material skyboxMaterial;

    void Start()
    {
        StartCoroutine(PlayVideoSequence());
    }

    private IEnumerator PlayVideoSequence()
    {
        // Set up skybox
        RenderSettings.skybox = skyboxMaterial;

        // Configure video player
        videoPlayer.clip = videoClip;
        videoPlayer.renderMode = VideoRenderMode.MaterialOverride;
        videoPlayer.targetMaterialRenderer = null; // Will render to skybox
        videoPlayer.targetMaterialProperty = "_MainTex";

        // Set skybox shader properties for stereo
        if (isStereoscopic)
        {
            skyboxMaterial.SetFloat("_Layout", 2); // Top/Bottom
            skyboxMaterial.SetFloat("_ImageType", 1); // 360 degrees
        }

        // Prepare and play
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
            yield return null;

        videoPlayer.Play();

        // Fade in (assuming you have a FadeController - create one or remove this line)
        // yield return FadeController.Instance.FadeIn();

        // Wait for video to complete
        while (videoPlayer.isPlaying)
        {
            // Optional: Check for user input to skip back to menu
            // Note: For Quest, you might want to use XR Input System instead of OVRInput
            // if(OVRInput.GetDown(OVRInput.Button.One))
            //     break;

            yield return null;
        }

        // Return to main menu
        ReturnToMainMenu();
    }

    private void ReturnToMainMenu()
    {
        StartCoroutine(TransitionToMainMenu());
    }

    private IEnumerator TransitionToMainMenu()
    {
        // Fade out (assuming you have a FadeController - create one or remove this line)
        // yield return FadeController.Instance.FadeOut();

        yield return SceneManager.LoadSceneAsync("MainMenu");
    }
}