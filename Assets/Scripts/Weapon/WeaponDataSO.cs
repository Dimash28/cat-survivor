using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Weapon Data")]
public class WeaponDataSO : ScriptableObject
{
    [Header("General")] 
    public string weaponName;
    public Sprite icon;

    [Header("Base Stats")]
    [SerializeField] public float damage;
    [Range(0.1f, 50f)] [SerializeField] public float cooldown;

    [Header("Projectile Type")] 
    [SerializeField] public float projectileSpeed;
    [Range(1, 50)] [SerializeField] public int projectileCount = 1;
    [Range(1, 5)] [SerializeField] public int pierce = 1;
    [SerializeField] public GameObject projectilePrefab;

    [Header("Aura Type")] 
    [Range(0.1f, 50f)] [SerializeField] public float auraRadius = 3f;                  
    [SerializeField] public GameObject auraPrefab;
}
