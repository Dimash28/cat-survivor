using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private EnemyDataSO enemyDataSO;
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private List<int> timeToSpawnList;

    private HealthSystem healthSystem;
    private EnemyDataSO runtimeDataSO;
    private HitEffect hitEffect;
    private bool isDying = false;
    private ObjectPool<Enemy> pool;

    private float damageCooldown = 0.5f;
    private float lastDamageTime;

    protected virtual void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        
        if (healthSystem == null)
            Debug.LogError($"Health component is missing on {gameObject.name}!");

        if (enemyDataSO != null)
            runtimeDataSO = Instantiate(enemyDataSO);
        else
            Debug.LogError($"EnemyDataSO is not assigned on {gameObject.name}");

        if (healthSystem == null || runtimeDataSO == null) return;

        healthSystem.SetMaxHealth(runtimeDataSO.MaxHealth);
    }

    private void Start() 
    {
        hitEffect = GetComponent<HitEffect>();
    }

    protected virtual void OnEnable()
    {
        Debug.Log($"{gameObject.name} OnEnable, healthSystem: {healthSystem != null}");

        isDying = false;

        if (healthSystem != null && runtimeDataSO != null)
            healthSystem.SetMaxHealth(runtimeDataSO.MaxHealth);

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

        if (pool != null)
            pool.Release(this);
        else
            Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Time.time - lastDamageTime < damageCooldown) return;
    
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.TakeDamage(runtimeDataSO.Damage);
            lastDamageTime = Time.time;
        }
    }

    public void SetPool(ObjectPool<Enemy> pool)
    {
        this.pool = pool;
    }
    
    public virtual void TakeDamage(float damage)
    {
        if (healthSystem != null)
            healthSystem.TakeDamage(damage);
    }

    public HealthSystem GetHealthSystem() => healthSystem;

    public List<int> GetTimeToSpawn() => new List<int>(timeToSpawnList);

    public EnemyDataSO GetRuntimeDataSO() => runtimeDataSO;
}
