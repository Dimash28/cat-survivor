using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] private GameObject template;

    public static LevelUpUI Instance { get; private set; }
    public bool IsShowing { get; private set; }

    private void Awake()
    {
        Instance = this;

        Hide();
    }

    private void Start()
    {
        G.experience.OnLevelUp += ExperienceSystem_OnLevelUp;
    }

    private void ExperienceSystem_OnLevelUp()
    {
        Show();
        
        G.game.SetOnPause();
    }

    private void Show()
    {
        IsShowing = true;
        template.SetActive(true);

        foreach (var button in GetComponentsInChildren<Button>())
        {
            button.OnDeselect(null);
        }

        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }

    private void Hide()
    {
        IsShowing = false;
        template.SetActive(false);
    }

    public void HideLevelUpUIAndUnpause()
    {
        Hide();
        G.game.SetUnpause();

        G.experience.TriggerNextLevelUp();
    }
}
