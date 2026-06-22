using UnityEngine;

public class EnemyTeleport : MonoBehaviour
{
    [SerializeField] private float teleportDistance = 12f;
    [SerializeField] private float spawnDistance = 11f;

    private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Update()
    {
        if (healthSystem.IsDead) return;

        float distance = Vector2.Distance(transform.position, G.player.GetPlayerPosition());
        if (distance > teleportDistance)
            Teleport();
    }

    private void Teleport()
    {
        Vector2 playerDir = G.input.GetInputVectorNormalized();
        if (playerDir == Vector2.zero)
            playerDir = Random.insideUnitCircle.normalized;

        float randomAngle = Random.Range(-60f, 60f);
        Vector2 spawnDir = RotateVector(playerDir, randomAngle);

        transform.position = G.player.GetPlayerPosition() +
                             (Vector3)spawnDir * spawnDistance;
    }

    private Vector2 RotateVector(Vector2 v, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        return new Vector2(cos * v.x - sin * v.y, sin * v.x + cos * v.y);
    }
}