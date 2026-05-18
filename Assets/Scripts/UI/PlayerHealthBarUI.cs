using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image background;
    [SerializeField] private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem.OnDamageTaken += HealthSystem_OnDamageTaken;
        healthSystem.OnHeal += HealthSystem_OnHeal;

        Hide();
    }

    private void UpdateHealthBarUI()
    {
        healthBar.fillAmount = healthSystem.GetCurrentHealth() / healthSystem.GetMaxHealth();
    }

    private void HealthSystem_OnDamageTaken()
    {
        Show();
        
        UpdateHealthBarUI();
    }

    private void HealthSystem_OnHeal()
    {
        UpdateHealthBarUI();

        if (healthBar.fillAmount == 1)
            Hide();
    }

    private void Show()
    {
        healthBar.enabled = true;
        background.enabled = true;
    }
    
    private void Hide()
    {
        healthBar.enabled = false;
        background.enabled = false;
    }
}
