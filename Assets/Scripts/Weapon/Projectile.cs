using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 6;
    [SerializeField] private float delay = 0.5f;
    private BoxCollider2D boxCollider2D;
    private Vector2 velocity;
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldown;
    protected float currentScale;
    protected int currentPierce;
    private float timer;
    private float delayTimer;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        currentScale = 1f;
    }

    private void Start()
    {
        boxCollider2D.enabled = false;
    }

    public void Setup(Vector2 direction, WeaponDataSO weaponDataSO)
    {
        if (weaponDataSO == null) 
        {
            Debug.LogError("WeaponDataSO is null in Setup!");
            return;
        }

        currentDamage = weaponDataSO.damage;
        currentSpeed = weaponDataSO.projectileSpeed;
        currentCooldown = weaponDataSO.cooldown;
        currentPierce = weaponDataSO.pierce;
        currentScale = weaponDataSO.projectileScale;

        velocity = direction * currentSpeed;
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.localScale *= currentScale;

        timer = lifeTime;
        delayTimer = delay;
    }

    private void Update()
    {
        transform.Translate(velocity * Time.deltaTime, Space.World);
        
        delayTimer -= Time.deltaTime;
        if(delayTimer <= 0)
        {
            boxCollider2D.enabled = true;
        }

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void ReducePierce()
    {
        currentPierce--;

        if (currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(currentDamage);

            ReducePierce();
        }
    }
}
