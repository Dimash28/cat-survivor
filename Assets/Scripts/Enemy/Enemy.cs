using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private EnemyDataSO enemyDataSO;
    [SerializeField] private int experienceValue = 5;

    private HealthSystem healthSystem;

    protected virtual void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        
        if (healthSystem == null)
        {
            Debug.LogError($"Health component is missing on {gameObject.name}!");
        }
    }

    protected virtual void Update()
    {
        if (healthSystem.IsDead) 
            return;

        MoveTowardsPlayer();
    }

    protected virtual void MoveTowardsPlayer()
    {
        Vector2 direction = (Player.Instance.GetPlayerPosition() - transform.position).normalized;
        transform.position += (Vector3)direction * enemyDataSO.MoveSpeed * Time.deltaTime;
    }

    public virtual void TakeDamage(float damage)
    {
        Debug.Log("EnemyTookDamage");

        if (healthSystem != null)
        {
            Debug.Log("Enemy is not null");
            healthSystem.TakeDamage(damage);
        }
    }

    protected virtual void OnEnable()
    {
        if (healthSystem != null)
        {
            healthSystem.OnDeath += OnDeath;
        }
    }

    protected virtual void OnDestroy()
    {
        if (healthSystem != null)
        {
            healthSystem.OnDeath -= OnDeath;
        }
    }

    protected virtual void OnDeath()
    {
        Destroy(gameObject);
    }
}
