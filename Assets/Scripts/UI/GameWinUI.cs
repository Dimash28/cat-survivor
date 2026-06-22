using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWinUI : MonoBehaviour
{
    [SerializeField] private GameObject template;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private SoundSO gameWinSoundSO;

    private void Awake()
    {
        Hide();
    }

    private void Start()
    {
        G.game.OnGameWin += GameManager_OnGameWin;

        restartButton.onClick.AddListener(() =>
        {
            G.game.Restart();
        });
        
        mainMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }

    private void GameManager_OnGameWin()
    {
        Show();
        G.audio.Play(gameWinSoundSO);
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
