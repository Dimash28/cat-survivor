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
