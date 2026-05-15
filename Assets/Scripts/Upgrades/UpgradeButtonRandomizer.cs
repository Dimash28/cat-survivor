using System.Collections.Generic;
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

        foreach (UpgradeButtonData button in upgradeButtonDataList)
        {
            if (button == null) continue;

            UpgradeDataSO selected = GetValidRandomUpgrade(upgradeDataSOList, playerUpgradeList);

            if(playerUpgradeList.Contains(selected)) continue;

            if (selected != null)
            {
                button.SetUpgradeDataSO(selected);
            }
        }
    }

    private UpgradeDataSO GetValidRandomUpgrade(List<UpgradeDataSO> upgradeDataSOList, List<UpgradeDataSO> playerUpgradeList)
    {
        List<UpgradeDataSO> shuffledUpgradeDataSOList = new List<UpgradeDataSO>(upgradeDataSOList);
        ShuffleUpgradeDataSOList(shuffledUpgradeDataSOList);

        foreach (var upgradeDataSO in shuffledUpgradeDataSOList)
        {
            if (IsUpgradeAvailable(upgradeDataSO, playerUpgradeList))
            {
                return upgradeDataSO;
            }
        }

        return null;
    }

    private bool IsUpgradeAvailable(UpgradeDataSO upgradeDataSO, List<UpgradeDataSO> playerUpgradeList)
    {
        if (upgradeDataSO.level == 0)
            return true;

        foreach (var owned in playerUpgradeList)
        {
            if(owned == upgradeDataSO) return false;

            if (owned == upgradeDataSO.previousUpgradeDataSO)
            {
                return true;
            }
        }

        return false;
    }

    public void ShuffleUpgradeDataSOList(List<UpgradeDataSO> upgradeDataSOList)
    {
        int n = upgradeDataSOList.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            UpgradeDataSO value = upgradeDataSOList[k];
            upgradeDataSOList[k] = upgradeDataSOList[n];
            upgradeDataSOList[n] = value;
        }
    }
}