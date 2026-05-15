using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image expBar;

    private void Start()
    {
        ExperienceSystem.Instance.OnExpGained += ExperienceSystem_OnExpGained;
        ExperienceSystem.Instance.OnLevelUp += ExperienceSystem_OnLevelUp;
        ExperienceSystem.Instance.OnCurrentExpAmountReset += ExperienceSystem_OnCurrentExpAmountReset;
    }

    private void ExperienceSystem_OnExpGained()
    {
        expBar.fillAmount = 
            ExperienceSystem.Instance.GetCurrentExpAmount() / 
            ExperienceSystem.Instance.GetCurrentExpAmountToNextLevel();
        
    }

    private void ExperienceSystem_OnLevelUp()
    {
        levelText.text = "Level " + ExperienceSystem.Instance.GetLevelNumber().ToString();
    }

    private void ExperienceSystem_OnCurrentExpAmountReset()
    {
        expBar.fillAmount = 0;
    }
}
