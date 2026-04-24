using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance {get; private set;}

    private HealthSystem healthSystem;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
    }

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }

    public virtual void TakeDamage(float damage)
    {
        Debug.Log("PlayerTookDamage");

        if (healthSystem != null)
        {
            Debug.Log("Player is not null");
            healthSystem.TakeDamage(damage);
        }
    }
}
