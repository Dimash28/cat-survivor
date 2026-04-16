using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Enemy Data")]
public class EnemyDataSO : ScriptableObject
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxHealth;
    [SerializeField] private float damage;

    public float MoveSpeed => moveSpeed;
    public float MaxHealth => maxHealth;
    public float Damage => damage;
}
