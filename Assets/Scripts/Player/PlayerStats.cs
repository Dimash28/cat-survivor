using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    [SerializeField] private float baseMaxHealth = 100f;
    [SerializeField] private float baseMoveSpeed = 5f;

    public float MaxHealth { get; private set; }
    public float MoveSpeed { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        MaxHealth = baseMaxHealth;
        MoveSpeed = baseMoveSpeed;
    }

    public void IncreaseMaxHealth(float amount)
    {
        MaxHealth += amount;
        G.player.GetHealthSystem().IncreaseMaxHealth(amount);
    }

    public void IncreaseMoveSpeed(float amount)
    {
        MoveSpeed += amount;
    }
}
