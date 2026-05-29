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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

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

                if (currentUpgrade == null) return;

                if (currentUpgrade.weaponDataSO != null)
                {
                    if (currentUpgrade.upgradeLevel == 0)
                        weaponManager.AddWeapon(currentUpgrade.weaponDataSO.prefab.GetComponent<Weapon>());

                    weaponManager.LevelUpWeapon(currentUpgrade);
                }
                else
                {
                    foreach (var effect in currentUpgrade.upgradeEffectList)
                    {
                        switch (effect.type)
                        {
                            case UpgradeType.MoveSpeed:
                                PlayerStats.Instance.IncreaseMoveSpeed(effect.value);
                                break;
                            case UpgradeType.Health:
                                PlayerStats.Instance.IncreaseMaxHealth(effect.value);
                                break;
                        }
                    }
                }

                playerUpgradeList.Add(currentUpgrade);
                levelUpUI.HideLevelUpUIAndUnpause();
            });
        }
    }

    public List<UpgradeDataSO> GetPlayerUpgradeList()
    {
        return new List<UpgradeDataSO>(playerUpgradeList);
    }

    private void OnDestroy()
    {
        if (ExperienceSystem.Instance != null)
            ExperienceSystem.Instance.OnLevelUp -= SetupUpgradeButtons;
    }
}
