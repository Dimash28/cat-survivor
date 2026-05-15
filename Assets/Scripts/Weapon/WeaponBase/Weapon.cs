using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponDataSO weaponDataSO;
    protected WeaponDataSO runtimeDataSO;
    protected float timer;

    protected virtual void Awake()
    {
        runtimeDataSO = null;
        
        if (weaponDataSO != null)
        {
            runtimeDataSO = Instantiate(weaponDataSO);
        }
        else
        {
            Debug.LogError($"WeaponDataSO is not assigned on {gameObject.name}");
        }
    }

    protected virtual void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Attack();
            timer = weaponDataSO.cooldown;
        }
    }

    protected abstract void Attack();
    
    public void ApplyUpgrade(UpgradeDataSO upgradeDataSO)
    {
        foreach (var effect in upgradeDataSO.upgradeEffectList)
        {
            switch (effect.type)
            {
                case UpgradeType.Damage:
                    runtimeDataSO.damage += effect.value;
                    break;

                case UpgradeType.Cooldown:
                    if (runtimeDataSO.cooldown > 0.3)
                        runtimeDataSO.cooldown -= effect.value;
                    else 
                        return;
                    break;

                case UpgradeType.ProjectileCount:
                    runtimeDataSO.projectileCount += (int)effect.value;
                    break;

                case UpgradeType.ProjectileSpeed:
                    runtimeDataSO.projectileSpeed += effect.value;
                    break;
                
                case UpgradeType.Pierce:
                    runtimeDataSO.pierce += (int)effect.value;
                    break;
            }
        }
    }

    public Sprite GetWeaponSprite()
    {
        return runtimeDataSO.icon;
    }
    
    public string GetWeaponName()
    {
        return runtimeDataSO.name;
    }

    public WeaponDataSO GetWeaponDataSO()
    {
        return weaponDataSO;
    }
}
