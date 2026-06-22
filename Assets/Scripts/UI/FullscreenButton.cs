using UnityEngine;
using UnityEngine.UI;

public class FullscreenButton : MonoBehaviour
{
    [SerializeField] private Sprite fullscreenSprite;
    [SerializeField] private Sprite windowedSprite;
    private Image buttonImage;

    private bool isFullscreen;

    private void Start()
    {
        isFullscreen = Screen.fullScreen;
        UpdateSprite();
    }

    public void Toggle()
    {
        isFullscreen = !isFullscreen;
        Screen.fullScreen = isFullscreen;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        buttonImage.sprite = isFullscreen ? fullscreenSprite : windowedSprite;
    }
}