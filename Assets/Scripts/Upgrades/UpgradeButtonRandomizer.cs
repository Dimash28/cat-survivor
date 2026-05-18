using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeButtonRandomizer : MonoBehaviour
{
    [SerializeField] private List<UpgradeDataSO> upgradeDataSOList;
    [SerializeField] private List<UpgradeButtonData> upgradeButtonDataList;

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
        List<UpgradeDataSO> playerUpgradeList = UpgradeSystem.Instance.GetPlayerUpgradeList();
        List<UpgradeDataSO> availableUpgrades = GetAvailableUpgrades(playerUpgradeList);

        if (availableUpgrades.Count == 0)
        {
            foreach (var button in upgradeButtonDataList)
                button?.SetUpgradeDataSO(null);
            return;
        }

        ShuffleUpgradeDataSOList(availableUpgrades);

        int index = 0;

        foreach (UpgradeButtonData button in upgradeButtonDataList)
        {
            if (button == null) continue;

            if (index >= availableUpgrades.Count)
            {
                ShuffleUpgradeDataSOList(availableUpgrades);
                index = 0;
            }

            button.SetUpgradeDataSO(availableUpgrades[index]);
            index++;
        }
    }

    private List<UpgradeDataSO> GetAvailableUpgrades(List<UpgradeDataSO> ownedUpgrades)
    {
        List<UpgradeDataSO> available = new List<UpgradeDataSO>();

        foreach (var upgradeDataSO in upgradeDataSOList)
        {
            if (IsUpgradeAvailable(upgradeDataSO, ownedUpgrades))
                available.Add(upgradeDataSO);
        }

        return available;
    }

    private bool IsUpgradeAvailable(UpgradeDataSO upgradeDataSO, List<UpgradeDataSO> playerUpgradeList)
    {
        if (upgradeDataSO.level == 0)
            return !playerUpgradeList.Any(o => o.baseUpgradeDataSO == upgradeDataSO || o == upgradeDataSO);

        UpgradeDataSO previous = upgradeDataSO.previousUpgradeDataSO;

        if (previous == null) return false;

        bool hasPrevious = playerUpgradeList.Any(o => o == previous);

        if (!hasPrevious) return false;

        return !playerUpgradeList.Any(o => 
            o.baseUpgradeDataSO == upgradeDataSO.baseUpgradeDataSO && 
            o.level >= upgradeDataSO.level);
    }

    private void ShuffleUpgradeDataSOList(List<UpgradeDataSO> list)
    {
        if (list == null || list.Count <= 1) return;

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}