using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Weapon Data")]
public class WeaponDataSO : ScriptableObject
{
    [Header("General")] 
    public string weaponName;
    public Sprite icon;
    public float damage;
    public float cooldown;

    [Header("Projectile Type")] 
    public float projectileSpeed;
    public int projectileCount = 1;
    public GameObject projectilePrefab;

    [Header("Aura Type")] 
    public float auraRadius = 3f;          
    public float tickRate = 0.5f;           
    public bool isContinuousDamage = true;
    public GameObject auraPrefab;
}
