using UnityEngine;

public class ArtifactPickup : MonoBehaviour
{
    [SerializeField] private GameObject lockedVisual;
    [SerializeField] private GameObject unlockedVisual;
    private bool isLocked = true;

    private void Start()
    {
        lockedVisual.SetActive(true);
        unlockedVisual.SetActive(false);
    }

    public void Unlock()
    {
        isLocked = false;

        unlockedVisual.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isLocked) return;
        if (!other.TryGetComponent<Player>(out Player player)) return;

        GameManager.Instance.OnArtifactCollected();
        Destroy(gameObject);
    }
}