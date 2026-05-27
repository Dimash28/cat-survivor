using UnityEngine;

public class AudioEventHandler : MonoBehaviour
{
    [SerializeField] private SoundSO levelUpSound;
    [SerializeField] private SoundSO playerHitSound;
    [SerializeField] private SoundSO gameOverSound;

    private void Start()
    {
        ExperienceSystem.Instance.OnLevelUp += OnLevelUp;
        Player.Instance.GetHealthSystem().OnDamageTaken += OnPlayerHit;
        GameManager.Instance.OnGameOver += OnGameOver;
    }

    private void OnLevelUp()
    {
        AudioManager.Instance.Play(levelUpSound);
    }

    private void OnPlayerHit()
    {
        AudioManager.Instance.Play(playerHitSound);
    }

    private void OnGameOver()
    {
        AudioManager.Instance.Play(gameOverSound);
    }

    private void OnDestroy()
    {
        if (ExperienceSystem.Instance != null)
            ExperienceSystem.Instance.OnLevelUp -= OnLevelUp;

        if (Player.Instance != null)
            Player.Instance.GetHealthSystem().OnDamageTaken -= OnPlayerHit;
    }
}
