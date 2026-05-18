using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private EnemyDataSO enemyDataSO;
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private List<int> timeToSpawnList;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private HealthSystem healthSystem;
    private EnemyDataSO runtimeDataSO;
    private HitEffect hitEffect;
    private bool isDying = false;

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

        healthSystem.SetMaxHealth(runtimeDataSO.MaxHealth);
    }

    private void Start() 
    {
        hitEffect = GetComponent<HitEffect>();
    }

    protected virtual void Update()
    {
        if (healthSystem.IsDead) 
            return;
    }

    protected virtual void OnEnable()
    {
        if (healthSystem != null)
        {
            healthSystem.OnDamageTaken += OnDamageTaken;
            healthSystem.OnDeath += OnDeath;
        }
    }

    protected virtual void OnDisable()
    {
        if (healthSystem != null)
        {
            healthSystem.OnDamageTaken -= OnDamageTaken;
            healthSystem.OnDeath -= OnDeath;
        }
    }

    private void OnDamageTaken()
    {
        if (isDying) return;

        hitEffect.PlayHitEffect();
    }

    protected virtual void OnDeath()
    {
        if (isDying) return;
        isDying = true;

        StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(0.15f);

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

    public List<int> GetTimeToSpawn()
    {
        return timeToSpawnList;
    }

    public EnemyDataSO GetRuntimeDataSO()
    {
        return runtimeDataSO;
    }
}
