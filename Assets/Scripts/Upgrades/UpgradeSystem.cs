using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    public static UpgradeSystem Instance {get; private set;}

    [SerializeField] private LevelUpUI levelUpUI;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private List<Button> upgradeButtonList;
    [SerializeField] private UpgradeDataSO firstWeapon;
    private List<UpgradeDataSO> playerUpgradeList;

    private void Awake() 
    {
        Instance = this;
        playerUpgradeList = new List<UpgradeDataSO>();
        playerUpgradeList.Add(firstWeapon);
    }

    private void Start()
    {
        ExperienceSystem.Instance.OnLevelUp += SetupUpgradeButtons;
    }

    private void SetupUpgradeButtons()
    {
        foreach (Button upgradeButton in upgradeButtonList)
        {
            UpgradeButtonData upgradeButtonData = upgradeButton.GetComponent<UpgradeButtonData>();
            
            upgradeButton.onClick.RemoveAllListeners();
            
            upgradeButton.onClick.AddListener(() =>
            {
                UpgradeDataSO currentUpgrade = upgradeButtonData.GetUpgradeDataSO();
                
                if (currentUpgrade == null)
                {
                    Debug.LogError("UpgradeDataSO is null on button!");
                    return;
                }

                if (currentUpgrade.level == 0)
                {
                    weaponManager.AddWeapon(currentUpgrade.weaponDataSO.prefab.GetComponent<Weapon>());                    
                }

                weaponManager.LevelUpWeapon(currentUpgrade);
                playerUpgradeList.Add(currentUpgrade);

                levelUpUI.HideLevelUpUIAndUnpause();
            });
        }
    }

    public List<UpgradeDataSO> GetPlayerUpgradeList()
    {
        return playerUpgradeList;
    }
}
