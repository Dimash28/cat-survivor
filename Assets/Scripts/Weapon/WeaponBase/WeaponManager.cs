using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    [Header("Starting Weapons")]
    [SerializeField] private List<Weapon> startingWeapons; 

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

    public void AddWeapon(Weapon weaponPrefab)
    {
        Weapon newWeapon = Instantiate(weaponPrefab, transform);
        newWeapon.transform.localPosition = Vector3.zero;

        activeWeapons.Add(newWeapon);

        Debug.Log($"Weapon added: {newWeapon.name}");
    }

    public void RemoveWeapon(Weapon weapon)
    {
        if (activeWeapons.Contains(weapon))
        {
            activeWeapons.Remove(weapon);
            Destroy(weapon.gameObject);
        }
    }

    public List<Weapon> GetActiveWeapons()
    {
        return activeWeapons;
    }

    public void LevelUpWeapon(Weapon weapon)
    {
        Debug.Log($"Leveled up: {weapon.name}");
    }
}
