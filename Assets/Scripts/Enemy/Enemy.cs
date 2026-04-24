using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private EnemyDataSO enemyDataSO;
    [SerializeField] private int experienceValue = 5;

    private HealthSystem healthSystem;
    private EnemyDataSO runtimeDataSO;

    protected virtual void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        
        if (healthSystem == null)
        {
            Debug.LogError($"Health component is missing on {gameObject.name}!");
        }

        if (enemyDataSO != null)
        {
            runtimeDataSO = Instantiate(enemyDataSO);
        }
        else
        {
            Debug.LogError($"EnemyDataSO is not assigned on {gameObject.name}");
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
        transform.position += (Vector3)direction * runtimeDataSO.MoveSpeed * Time.deltaTime;
    }


    protected virtual void OnEnable()
    {
        if (healthSystem != null)
        {
            healthSystem.OnDeath += OnDeath;
        }
    }

    protected virtual void OnDisable()
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.TakeDamage(runtimeDataSO.Damage);
        }
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

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }
}
