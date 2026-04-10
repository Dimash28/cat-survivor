using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponDataSO weaponDataSO;
    protected WeaponDataSO runtimeDataSO;
    protected float timer;

    protected virtual void Awake()
    {
        if (weaponDataSO != null)
        {
            // Создаём копию ScriptableObject при старте
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
}
