using UnityEngine;

public class AudioEventHandler : MonoBehaviour
{
    [SerializeField] private SoundSO levelUpSound;
    [SerializeField] private SoundSO playerHitSound;
    [SerializeField] private SoundSO gameOverSound;

    private void Start()
    {
        G.experience.OnLevelUp += OnLevelUp;
        G.player.GetHealthSystem().OnDamageTaken += OnPlayerHit;
        G.game.OnGameOver += OnGameOver;
    }

    private void OnLevelUp()
    {
        G.audio.Play(levelUpSound);
    }

    private void OnPlayerHit()
    {
        G.audio.Play(playerHitSound);
    }

    private void OnGameOver()
    {
        G.audio.Play(gameOverSound);
    }

    private void OnDestroy()
    {
        if (G.experience != null)
            G.experience.OnLevelUp -= OnLevelUp;

        if (G.player != null)
            G.player.GetHealthSystem().OnDamageTaken -= OnPlayerHit;
    }
}
