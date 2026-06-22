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

        G.experience.OnLevelUp += RandomizeUpgradeButtons;
    }

    private void RandomizeUpgradeButtons()
    {
        List<UpgradeDataSO> playerUpgradeList = G.upgrade.GetPlayerUpgradeList();
        List<UpgradeDataSO> availableUpgrades = GetAvailableUpgrades(playerUpgradeList);

        ShuffleUpgradeDataSOList(availableUpgrades);

        for (int i = 0; i < upgradeButtonDataList.Count; i++)
        {
            if (upgradeButtonDataList[i] == null) continue;

            if (i < availableUpgrades.Count)
            {
                upgradeButtonDataList[i].gameObject.SetActive(true);
                upgradeButtonDataList[i].SetUpgradeDataSO(availableUpgrades[i]);
            }
            else
            {
                upgradeButtonDataList[i].gameObject.SetActive(false);
            }
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
        if (upgradeDataSO.upgradeLevel == 0)
            return !playerUpgradeList.Any(o => o.baseUpgradeDataSO == upgradeDataSO || o == upgradeDataSO);

        UpgradeDataSO previous = upgradeDataSO.previousUpgradeDataSO;

        if (previous == null) return false;

        bool hasPrevious = playerUpgradeList.Any(o => o == previous);

        if (!hasPrevious) return false;

        return !playerUpgradeList.Any(o => 
            o.baseUpgradeDataSO == upgradeDataSO.baseUpgradeDataSO && 
            o.upgradeLevel >= upgradeDataSO.upgradeLevel);
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

    private void OnDestroy()
    {
        if (G.experience != null)
            G.experience.OnLevelUp -= RandomizeUpgradeButtons;
    }
}