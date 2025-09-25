using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ThumbnailButton : MonoBehaviour
{
    [SerializeField] private Image thumbnailImage;
    [SerializeField] private Image whiteOverlay;
    [SerializeField] private Button uiButton;

    private System.Action onSelectCallback;

    public void Setup(MainMenuManager.VideoData videoData, System.Action callback)
    {
        thumbnailImage.sprite = videoData.thumbnail;
        onSelectCallback = callback;

        // Use Unity UI Button instead of XR Interactable
        uiButton.onClick.AddListener(() => OnGazeSelect());
    }

    private void OnGazeSelect()
    {
        StartCoroutine(PlaySelectionAnimation());
    }

    private IEnumerator PlaySelectionAnimation()
    {
        // Animate white overlay sliding over thumbnail
        whiteOverlay.gameObject.SetActive(true);

        // Use DOTween or Animator for smooth animation
        float duration = 0.8f;
        float timer = 0f;

        RectTransform overlayRect = whiteOverlay.rectTransform;
        overlayRect.anchorMin = new Vector2(0, 0);
        overlayRect.anchorMax = new Vector2(0, 1);

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            overlayRect.anchorMax = new Vector2(progress, 1);
            yield return null;
        }

        // Small delay then trigger scene load
        yield return new WaitForSeconds(0.2f);
        onSelectCallback?.Invoke();
    }
}