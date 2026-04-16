using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Weapon Data")]
public class WeaponDataSO : ScriptableObject
{
    [Header("General")] 
    public string weaponName;
    public Sprite icon;

    [Header("Base Stats")]
    [SerializeField] private float damage;
    [Range(0.1f, 50f)] [SerializeField] private float cooldown;

    [Header("Projectile Type")] 
    [SerializeField] private float projectileSpeed;
    [Range(1, 50)] [SerializeField] private int projectileCount = 1;
    [Range(1, 5)] [SerializeField] private int pierce = 1;
    [SerializeField] private GameObject projectilePrefab;

    [Header("Aura Type")] 
    [Range(0.1f, 50f)] [SerializeField] private float auraRadius = 3f;          
    [Range(0.1f, 50f)] [SerializeField] private float tickRate = 0.5f;           
    [SerializeField] private GameObject auraPrefab;

    public float Damage => damage;
    public float Cooldown => cooldown;
    public float ProjectileSpeed => projectileSpeed;
    public int ProjectileCount => projectileCount;
    public int Pierce => pierce;
    public GameObject ProjectilePrefab => projectilePrefab;

    public float AuraRadius => auraRadius;
    public float TickRate => tickRate;
    public GameObject AuraPrefab => auraPrefab;
}
