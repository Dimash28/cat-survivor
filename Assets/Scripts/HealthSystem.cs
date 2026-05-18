using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    public float CurrentHealth { get; private set; }

    public bool IsDead => CurrentHealth <= 0;

    public event System.Action OnDeath;
    public event System.Action OnDamageTaken;
    public event System.Action OnHeal;

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("HealthSystem.TakeDamage() Performed");
        if (IsDead) return;

        CurrentHealth -= damage;
        OnDamageTaken?.Invoke();

        if (IsDead)
        {
            OnDeath?.Invoke();
        }

        Debug.Log(CurrentHealth);
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
    }

    public void Heal(float healAmount)
    {
        CurrentHealth += healAmount;
        CurrentHealth = Mathf.Min(CurrentHealth, maxHealth);

        OnHeal?.Invoke();
    }
}
