using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image expBar;

    private void Start()
    {
        G.experience.OnExpGained += ExperienceSystem_OnExpGained;
        G.experience.OnLevelUp += ExperienceSystem_OnLevelUp;
        G.experience.OnCurrentExpAmountReset += ExperienceSystem_OnCurrentExpAmountReset;
    }

    private void ExperienceSystem_OnExpGained()
    {
        expBar.fillAmount = 
            G.experience.GetCurrentExpAmount() / 
            G.experience.GetCurrentExpAmountToNextLevel();
        
    }

    private void ExperienceSystem_OnLevelUp()
    {
        levelText.text = "Level " + G.experience.GetLevelNumber().ToString();
    }

    private void ExperienceSystem_OnCurrentExpAmountReset()
    {
        expBar.fillAmount = 0;
    }
}
