using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance {get; private set;}

    [SerializeField] private SoundSO healSound;

    private PlayerHitEffect playerHitEffect;
    private HealthSystem healthSystem;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        healthSystem = GetComponent<HealthSystem>();

        if (healthSystem == null)
        {
            Debug.LogError("HealthSystem не найден на Player!");
            return;
        }

        healthSystem.OnDamageTaken += OnDamageTaken;
    }
    
    private void Start() 
    {
        playerHitEffect = GetComponentInChildren<PlayerHitEffect>();
    }

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }

    public void TakeDamage(float damage)
    {
        healthSystem.TakeDamage(damage);
    }

    public void Heal(float healAmount)
    {
        healthSystem.Heal(healAmount);
        G.audio.Play(healSound);
    }

    private void OnDamageTaken()
    {
        if(playerHitEffect != null)
            playerHitEffect.PlayHitEffect();
    }

    private void OnDestroy()
    {
        if (healthSystem != null)
            healthSystem.OnDamageTaken -= OnDamageTaken;
    }
}
