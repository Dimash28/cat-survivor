using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    [SerializeField] private int maxGameTimeInMinutes = 15;

    public Action OnGameOver;

    private float gameStartingTimer = 3f;
    private float gamePlayingTimer;
    private bool isPaused;
    private bool isGameOver;
    private GameState state;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        gamePlayingTimer = maxGameTimeInMinutes * 60f;
    }

    private void Start()
    {
        GameInput.Instance.OnEscapePerformed += PerformPause;
    }

    public enum GameState
    {
        GameStarting,
        GamePlaying,
        GameOver,
        Pause
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.GameStarting:
                isGameOver = false;
                Time.timeScale = 0;

                gameStartingTimer -= Time.unscaledDeltaTime;
                if(gameStartingTimer <= 0f)
                {
                    Time.timeScale = 1;
                    state = GameState.GamePlaying;
                }
                break;

            case GameState.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if(gamePlayingTimer <= 0f || Player.Instance.GetHealthSystem().IsDead)
                {
                    state = GameState.GameOver;
                }
                break;

            case GameState.GameOver:
                isGameOver = true;
                SetOnPause();

                OnGameOver?.Invoke();
                break;
        }
    }

    private void PerformPause(object sender, System.EventArgs e)
    {
        if (!isPaused) SetOnPause();
        else SetUnpause();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetOnPause()
    {
        if(isPaused) return;

        isPaused = true;
        Time.timeScale = 0;
    }

    public void SetUnpause()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
    
    public float GetGamePlayingTime()
    {
        return gamePlayingTimer;
    }

    public int GetMaxGameTimeInMinutes()
    {
        return maxGameTimeInMinutes;
    }
}
