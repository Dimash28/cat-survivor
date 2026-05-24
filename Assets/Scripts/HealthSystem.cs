using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    public float CurrentHealth { get; private set; }

    public bool IsDead => CurrentHealth <= 0;

    public event System.Action OnDeath;
    public event System.Action OnDamageTaken;
    public event System.Action OnHeal;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        CurrentHealth -= damage;
        OnDamageTaken?.Invoke();

        if (IsDead)
        {
            OnDeath?.Invoke();
        }
    }

    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public void Heal(float healAmount)
    {
        CurrentHealth += healAmount;
        CurrentHealth = Mathf.Min(CurrentHealth, maxHealth);

        OnHeal?.Invoke();
    }
}
