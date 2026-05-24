using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemyPrefabs;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnRadius = 15f;

    private Dictionary<Enemy, ObjectPool<Enemy>> pools = new();
    private float timer;

    private void Awake()
    {
        foreach (var prefab in enemyPrefabs)
        {
            Enemy capturedPrefab = prefab;

            pools[prefab] = new ObjectPool<Enemy>(
                createFunc:      () => Instantiate(capturedPrefab),
                actionOnGet:     enemy => enemy.gameObject.SetActive(true),
                actionOnRelease: enemy => enemy.gameObject.SetActive(false),
                actionOnDestroy: enemy => Destroy(enemy.gameObject),
                defaultCapacity: 10,
                maxSize: 50
            );
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnEnemy();
            timer = spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Count == 0) return;

        int elapsed = Mathf.FloorToInt(
            (GameManager.Instance.GetMaxGameTimeInMinutes() * 60f 
            - GameManager.Instance.GetGamePlayingTime()) / 60f
        );

        List<Enemy> available = enemyPrefabs.FindAll(e => 
            e.GetTimeToSpawn().Count == 0 || 
            e.GetTimeToSpawn().Exists(t => t <= elapsed)
        );

        if (available.Count == 0) return;

        Enemy prefab = available[Random.Range(0, available.Count)];
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        Vector3 spawnPos = Player.Instance.GetPlayerPosition() + (Vector3)randomDir * spawnRadius;

        Enemy enemy = pools[prefab].Get();
        enemy.transform.position = spawnPos;
        enemy.SetPool(pools[prefab]);
    }
}
