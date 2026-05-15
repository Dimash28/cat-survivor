using System.Collections;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] private GameObject template;

    private void Awake()
    {
        Hide();
    }

    private void Start()
    {
        ExperienceSystem.Instance.OnLevelUp += ExperienceSystem_OnLevelUp;
    }

    private void ExperienceSystem_OnLevelUp()
    {
        Show();
        
        StartCoroutine(PauseAfterDelay());
    }

    private IEnumerator PauseAfterDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        GameManager.Instance.SetOnPause();
    }

    private void Show()
    {
        template.SetActive(true);
    }

    private void Hide()
    {
        template.SetActive(false);
    }

    public void HideLevelUpUIAndUnpause()
    {
        Hide();
        GameManager.Instance.SetUnpause();
    }
}
