using System.Collections.Generic;
using UnityEngine;

public class ArtifactSequencer : MonoBehaviour
{
    public static ArtifactSequencer Instance { get; private set; }

    [SerializeField] private List<ArtifactPickup> artifacts;
    [SerializeField] private List<ArtifactIndicator> indicators;

    private int currentIndex = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }

        Instance = this;
    }

    private void Start()
    {
        artifacts[0].gameObject.SetActive(true);
        indicators[0].ShowWithAnimation();
    }

    public void OnArtifactCollected()
    {
        Debug.Log($"OnArtifactCollected, currentIndex: {currentIndex}");
        G.game.OnArtifactCollected();
        indicators[currentIndex].Hide();
        currentIndex++;
        Debug.Log($"After increment, currentIndex: {currentIndex}, artifacts.Count: {artifacts.Count}");

        if (currentIndex < artifacts.Count)
        {
            Debug.Log($"Activating artifact {currentIndex}");
            artifacts[currentIndex].gameObject.SetActive(true);
            indicators[currentIndex].ShowWithAnimation();
        }
    }
}