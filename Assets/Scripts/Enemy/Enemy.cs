using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private EnemyDataSO enemyDataSO;
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private SoundSO deathSound;
    [SerializeField] private SoundSO damageSound;

    protected HealthSystem healthSystem;
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

    protected virtual void Update()
    {
        if (healthSystem.IsDead) return;

        int elapsed = Mathf.FloorToInt(
            (G.game.GetMaxGameTimeInMinutes() * 60f
            - G.game.GetGamePlayingTime()) / 60f
        );

        bool shouldBeAlive = enemyDataSO.SpawnTimeRanges.Exists(range =>
            elapsed >= range.from && elapsed <= range.to
        );

        if (!shouldBeAlive)
        {
            float distance = Vector2.Distance(transform.position, G.player.GetPlayerPosition());
            if (distance > 15f)
            {
                if (pool != null)
                    pool.Release(this);
                else
                    Destroy(gameObject);
            }
        }
    }

    protected virtual void OnEnable()
    {
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

        G.audio.Play(damageSound);

        if(hitEffect != null)
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

    public EnemyDataSO GetRuntimeDataSO() => runtimeDataSO;

    public EnemyDataSO GetEnemyDataSO() => enemyDataSO;
}
