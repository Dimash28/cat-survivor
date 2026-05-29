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
        
        if (upgradeDataSO == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        buttonText.text = upgradeDataSO.upgradeTitle;

        if(upgradeDataSO.weaponDataSO == null)
        { 
            buttonSprite.sprite = null;
            return;
        }
        
        buttonSprite.sprite = upgradeDataSO.weaponDataSO.icon;
    }

    public UpgradeDataSO GetUpgradeDataSO()
    {
        if (upgradeDataSO == null)
            Debug.LogError($"UpgradeDataSO is null on {gameObject.name}");
        return upgradeDataSO;
    }
}
