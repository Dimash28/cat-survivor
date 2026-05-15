using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade Data")]
public class UpgradeDataSO : ScriptableObject
{
    public string upgradeTitle;
    public WeaponDataSO weaponDataSO;

    public int level;
    public UpgradeDataSO previousUpgradeDataSO;

    public List<UpgradeEffect> upgradeEffectList;
}
