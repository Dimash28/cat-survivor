using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonData : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Image buttonSprite;
    private UpgradeDataSO upgradeDataSO;

    public void SetUpgradeDataSO(UpgradeDataSO upgradeDataSO)
    {
        this.upgradeDataSO = upgradeDataSO;
        
        buttonText.text = this.upgradeDataSO.upgradeTitle;
        buttonSprite.sprite = this.upgradeDataSO.weaponDataSO.icon;
    }

    public UpgradeDataSO GetUpgradeDataSO()
    {
        if (upgradeDataSO == null)
            Debug.LogError($"UpgradeDataSO is null on {gameObject.name}");
        return upgradeDataSO;
    }
}
