using UnityEngine;

public class ExpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject expPrefab;
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();

        enemy.GetHealthSystem().OnDeath += EnemyHealthSystem_OnDeath;
    }

    private void EnemyHealthSystem_OnDeath()
    {
        Instantiate(expPrefab, transform.position, Quaternion.identity);
    }
}
