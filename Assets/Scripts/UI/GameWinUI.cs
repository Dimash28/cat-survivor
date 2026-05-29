using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWinUI : MonoBehaviour
{
    [SerializeField] private GameObject template;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        Hide();
    }

    private void Start()
    {
        GameManager.Instance.OnGameWin += GameManager_OnGameWin;

        restartButton.onClick.AddListener(() =>
        {
            GameManager.Instance.Restart();
        });
        
        mainMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }

    private void GameManager_OnGameWin()
    {
        Show();
    }

    private void Show()
    {
        template.SetActive(true);
    }

    private void Hide()
    {
        template.SetActive(false);
    }
}
