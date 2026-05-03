using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private LevelUpUI levelUpUI;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private List<Button> upgradeButtonList;

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

                weaponManager.LevelUpWeapon(currentUpgrade);
                levelUpUI.HideLevelUpUIAndUnpause();
            });
        }
    }
}
