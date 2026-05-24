using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    [SerializeField] private int maxGameTimeInMinutes = 15;

    public Action OnGameOver;

    private float gameStartingTimer = 3f;
    private float gamePlayingTimer;
    private bool isPaused;
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

        state = GameState.GameStarting;
        Time.timeScale = 0;
    }

    private void Start()
    {
        if (GameInput.Instance != null)
            GameInput.Instance.OnEscapePerformed += PerformPause;
    
        if (Player.Instance?.GetHealthSystem() != null)
            Player.Instance.GetHealthSystem().OnDeath += GameOver;
    }

    public enum GameState
    {
        GameStarting,
        GamePlaying,
        GameOver
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.GameStarting:
                gameStartingTimer -= Time.unscaledDeltaTime;
                if(gameStartingTimer <= 0f)
                {
                    Time.timeScale = 1;
                    state = GameState.GamePlaying;
                }
                break;

            case GameState.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if(gamePlayingTimer <= 0f)
                {
                    GameOver();
                }
                break;
        }
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
    
    public float GetGamePlayingTime()
    {
        return gamePlayingTimer;
    }

    public int GetMaxGameTimeInMinutes()
    {
        return maxGameTimeInMinutes;
    }

    private void PerformPause(object sender, System.EventArgs e)
    {
        if (state != GameState.GamePlaying) return;

        if (!isPaused) SetOnPause();
        else SetUnpause();
    }

    private void GameOver()
    {
        if (state == GameState.GameOver) return;

        state = GameState.GameOver;
        SetOnPause();
        OnGameOver?.Invoke();
    }

    private void OnDestroy() 
    {
        if (GameInput.Instance != null)
            GameInput.Instance.OnEscapePerformed -= PerformPause;

        if (Player.Instance != null)
            Player.Instance.GetHealthSystem().OnDeath -= GameOver;
    }
}
