using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponDataSO weaponDataSO;
    protected WeaponDataSO runtimeDataSO;
    protected float timer;

    protected virtual void Awake()
    {
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
                    Debug.Log("Damage Before Upgrade " + runtimeDataSO.damage);
                    runtimeDataSO.damage += effect.value;
                    Debug.Log("Damage After Upgrade " + runtimeDataSO.damage);
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
