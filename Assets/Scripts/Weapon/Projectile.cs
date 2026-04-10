using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 6;
    private Vector2 velocity;
    private float damage;
    private float timer;

    private void Start()
    {
        timer = lifeTime;
    }

    public void Setup(Vector2 velocity, float damage)
    {
        this.velocity = velocity;
        this.damage = damage;

        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Update()
    {
        transform.Translate(velocity * Time.deltaTime, Space.World);

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
