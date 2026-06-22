using UnityEngine;
using UnityEngine.UI;

public class ArtifactIndicator : MonoBehaviour
{
    [SerializeField] private ArtifactPickup targetArtifact;
    [SerializeField] private Image indicatorImage;
    [SerializeField] private float edgePadding = 50f;
    [SerializeField] private float animationDuration = 1f;

    private Camera mainCamera;
    private RectTransform rectTransform;
    private RectTransform canvasRect;
    private float animationProgress = 0f;
    private bool isAnimating = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    private void Start()
    {
        mainCamera = Camera.main;
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    public void ShowWithAnimation()
    {
        gameObject.SetActive(true);
        animationProgress = 0f;
        isAnimating = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (targetArtifact == null)
        {
            gameObject.SetActive(false);
            return;
        }

        Vector3 screenPos = mainCamera.WorldToViewportPoint(targetArtifact.transform.position);

        bool isOnScreen = screenPos.x > 0 && screenPos.x < 1 &&
                        screenPos.y > 0 && screenPos.y < 1 &&
                        screenPos.z > 0;

        if (isAnimating)
        {
            animationProgress += Time.unscaledDeltaTime / animationDuration;
            animationProgress = Mathf.Clamp01(animationProgress);

            if (animationProgress >= 1f)
                isAnimating = false;

            UpdatePosition(screenPos, animationProgress);
            return;
        }

        if (isOnScreen)
        {
            indicatorImage.gameObject.SetActive(false);
            return;
        }

        indicatorImage.gameObject.SetActive(true);
        UpdatePosition(screenPos, 1f);
    }

    private void UpdatePosition(Vector3 screenPos, float progress)
    {
        Vector2 direction = new Vector2(screenPos.x - 0.5f, screenPos.y - 0.5f).normalized;

        float halfW = canvasRect.rect.width / 2f - edgePadding;
        float halfH = canvasRect.rect.height / 2f - edgePadding;

        float slope = direction.y / direction.x;
        Vector2 targetPos;

        if (Mathf.Abs(slope) * halfW < halfH)
        {
            float x = Mathf.Sign(direction.x) * halfW;
            targetPos = new Vector2(x, x * slope);
        }
        else
        {
            float y = Mathf.Sign(direction.y) * halfH;
            targetPos = new Vector2(y / slope, y);
        }

        rectTransform.anchoredPosition = targetPos * progress;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rectTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}