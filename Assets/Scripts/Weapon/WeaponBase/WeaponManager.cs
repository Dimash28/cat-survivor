using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    [Header("Starting Weapons")]
    [SerializeField] private List<Weapon> startingWeapons;   // префабы оружий

    private List<Weapon> activeWeapons = new List<Weapon>();

    private void Start()
    {
        InitializeStartingWeapons();
    }

    private void InitializeStartingWeapons()
    {
        foreach (Weapon weaponPrefab in startingWeapons)
        {
            if (weaponPrefab != null)
            {
                AddWeapon(weaponPrefab);
            }
        }
    }

    // Основной метод добавления оружия
    public void AddWeapon(Weapon weaponPrefab)
    {
        // Создаём экземпляр оружия как ребёнка игрока
        Weapon newWeapon = Instantiate(weaponPrefab, transform);
        newWeapon.transform.localPosition = Vector3.zero;

        activeWeapons.Add(newWeapon);

        Debug.Log($"Weapon added: {newWeapon.name}");
    }

    // Удаление оружия (если понадобится)
    public void RemoveWeapon(Weapon weapon)
    {
        if (activeWeapons.Contains(weapon))
        {
            activeWeapons.Remove(weapon);
            Destroy(weapon.gameObject);
        }
    }

    // Получить все активные оружия (если нужно)
    public List<Weapon> GetActiveWeapons()
    {
        return activeWeapons;
    }

    // Опционально: уровень оружия
    public void LevelUpWeapon(Weapon weapon)
    {
        // Пока заглушка, позже реализуем систему уровней
        Debug.Log($"Leveled up: {weapon.name}");
    }
}
