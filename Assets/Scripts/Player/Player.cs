using System.Net.Cache;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance {get; private set;}

    private HitEffect hitEffect;
    private HealthSystem healthSystem;
    

    private void Awake()
    {
        Instance = this;

        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDamageTaken += OnDamageTaken;
    }
    
    private void Start() 
    {
        hitEffect = GetComponentInChildren<HitEffect>();
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
        if (healthSystem != null)
        {
            healthSystem.TakeDamage(damage);
        }
    }

    public void Heal(float healAmount)
    {
        if (healthSystem != null)
        {
            healthSystem.Heal(healAmount);
        }
        else
        {
            Debug.LogError("HealthSystem is NULL in Player!");
        }
    }

    private void OnDamageTaken()
    {
        hitEffect.PlayHitEffect();
    }
}
