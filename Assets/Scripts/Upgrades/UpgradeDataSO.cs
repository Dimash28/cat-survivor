using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade Data")]
public class UpgradeDataSO : ScriptableObject
{
    public string upgradeTitle;
    public WeaponDataSO weaponDataSO;

    public int upgradeLevel;
    public UpgradeDataSO previousUpgradeDataSO;
    public UpgradeDataSO baseUpgradeDataSO;

    public List<UpgradeEffect> upgradeEffectList;
}
