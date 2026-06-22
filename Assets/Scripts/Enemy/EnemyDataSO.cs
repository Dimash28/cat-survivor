using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Enemy Data")]
public class EnemyDataSO : ScriptableObject
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxHealth;
    [SerializeField] private float damage;
    [SerializeField] private List<SpawnTimeRange> spawnTimeRanges;
    public bool requiresUnlock = false;
    
    public List<SpawnTimeRange> SpawnTimeRanges => spawnTimeRanges;
    public float MoveSpeed => moveSpeed;
    public float MaxHealth => maxHealth;
    public float Damage => damage;

    public void SetSpawnFrom(int fromMinute)
    {
        spawnTimeRanges = new List<SpawnTimeRange>
        {
            new SpawnTimeRange { from = fromMinute, to = 15 }
        };
    }
}
