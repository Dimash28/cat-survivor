using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    public static UpgradeSystem Instance {get; private set;}

    public Action OnHealthUpgrade;

    [SerializeField] private LevelUpUI levelUpUI;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private List<Button> upgradeButtonList;
    [SerializeField] private UpgradeDataSO firstWeapon;
    [SerializeField] private SoundSO upgradeAppliedSoundSO;
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
        G.experience.OnLevelUp += SetupUpgradeButtons;
    }

    private void SetupUpgradeButtons()
    {
        foreach (Button upgradeButton in upgradeButtonList)
        {
            UpgradeButtonData upgradeButtonData = upgradeButton.GetComponent<UpgradeButtonData>();
            
            upgradeButton.onClick.RemoveAllListeners();
            
            upgradeButton.onClick.AddListener((UnityEngine.Events.UnityAction)(() =>
            {
                G.audio.Play(upgradeAppliedSoundSO);

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
                                G.stats.IncreaseMoveSpeed(effect.value);
                                break;
                            case UpgradeType.Health:
                                G.stats.IncreaseMaxHealth(effect.value);
                                OnHealthUpgrade?.Invoke();
                                break;
                        }
                    }
                }

                playerUpgradeList.Add(currentUpgrade);
                levelUpUI.HideLevelUpUIAndUnpause();
            }));
        }
    }

    public List<UpgradeDataSO> GetPlayerUpgradeList()
    {
        return new List<UpgradeDataSO>(playerUpgradeList);
    }

    private void OnDestroy()
    {
        if (G.experience != null)
            G.experience.OnLevelUp -= SetupUpgradeButtons;
    }
}
