using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    [Header("Starting Weapons")]
    [SerializeField] private List<Weapon> weaponList;

    private List<Weapon> activeWeaponList = new List<Weapon>();

    private void Start()
    {
        InitializeStartingWeapons();
    }

    private void InitializeStartingWeapons()
    {
        foreach (Weapon weaponPrefab in weaponList)
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

        activeWeaponList.Add(newWeapon);

        Debug.Log($"Weapon added: {newWeapon.name}");
    }

    public void RemoveWeapon(Weapon weapon)
    {
        if (activeWeaponList.Contains(weapon))
        {
            activeWeaponList.Remove(weapon);
            Destroy(weapon.gameObject);
        }
    }

    public List<Weapon> GetActiveWeaponList()
    {
        return activeWeaponList;
    }

    public void LevelUpWeapon(UpgradeDataSO upgradeDataSO)
    {
        Debug.Log("LevelUpWeapon()");
        foreach (Weapon activeWeapon in activeWeaponList)
        {
            if (activeWeapon.GetWeaponDataSO() == upgradeDataSO.weaponDataSO)
            {
                activeWeapon.ApplyUpgrade(upgradeDataSO);
                Debug.Log(activeWeapon.name + ".ApplyUpgrade(" + upgradeDataSO.upgradeTitle + ")");
            }
        }
    }
}
