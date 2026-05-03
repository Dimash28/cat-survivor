using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtonRandomizer : MonoBehaviour
{
    [SerializeField] private List<UpgradeDataSO> upgradeDataSOList;
    [SerializeField] private List<UpgradeButtonData> upgradeButtonDataList; // можно оставить для ручного заполнения

    private void Start()
    {
        if (upgradeButtonDataList == null || upgradeButtonDataList.Count == 0)
        {
            upgradeButtonDataList = new List<UpgradeButtonData>(GetComponentsInChildren<UpgradeButtonData>());
        }

        ExperienceSystem.Instance.OnLevelUp += RandomizeUpgradeButtons;
    }

    private void RandomizeUpgradeButtons()
    {
        if (upgradeDataSOList == null || upgradeDataSOList.Count == 0)
        {
            Debug.LogError("upgradeDataSOList is empty!");
            return;
        }

        foreach (UpgradeButtonData upgradeButtonData in upgradeButtonDataList)
        {
            if (upgradeButtonData == null) continue;

            int randomIndex = Random.Range(0, upgradeDataSOList.Count);
            UpgradeDataSO selected = upgradeDataSOList[randomIndex];

            upgradeButtonData.SetUpgradeDataSO(selected);

            Debug.Log($"Set upgrade on {upgradeButtonData.name}: {selected.name} | Weapon: {selected.weaponDataSO?.weaponName}");
        }
    }
}