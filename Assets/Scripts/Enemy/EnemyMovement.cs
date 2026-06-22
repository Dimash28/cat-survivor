using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Enemy enemy;

    [SerializeField] private Vector2 offset;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update() 
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = GetDirectionToPlayer() + offset;
        transform.position += (Vector3)direction * enemy.GetRuntimeDataSO().MoveSpeed * Time.deltaTime;
    }

    public Vector2 GetDirectionToPlayer()
    {
        return (G.player.GetPlayerPosition() - transform.position).normalized;
    }
}
