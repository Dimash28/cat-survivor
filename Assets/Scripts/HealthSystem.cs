using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;

    public float CurrentHealth { get; private set; }

    public bool IsDead => CurrentHealth <= 0;

    public event System.Action OnDeath;
    public event System.Action OnDamageTaken;

    private void Awake()
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
}
