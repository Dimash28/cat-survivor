using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        GameStarting,
        GamePlaying,
        GameOver,
        GameWin
    }
    
    public static GameManager Instance {get; private set;}
    [SerializeField] private int maxGameTimeInMinutes = 15;

    public Action OnGameOver;
    public Action OnGameWin;
    public Action OnPause;

    private float gameStartingTimer = 3f;
    private float gamePlayingTimer;
    private bool isPaused;
    private GameState state;

    private int totalArtifacts = 3;
    private int collectedArtifacts = 0;

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
        if (G.input != null)
            G.input.OnEscapePerformed += PerformPause;
    
        if (G.player?.GetHealthSystem() != null)
            G.player.GetHealthSystem().OnDeath += GameOver;
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

    public void OnArtifactCollected()
    {
        collectedArtifacts++;

        if (collectedArtifacts >= totalArtifacts)
            GameWin();
    }

    private void GameWin()
    {
        if (state == GameState.GameOver || state == GameState.GameWin) return;
        state = GameState.GameWin;
        SetOnPause();
        OnGameWin?.Invoke();
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

        OnPause?.Invoke();
    }

    public void SetUnpause()
    {
        isPaused = false;
        Time.timeScale = 1;

        OnPause?.Invoke();
    }
    
    public float GetGamePlayingTime()
    {
        return gamePlayingTimer;
    }

    public int GetMaxGameTimeInMinutes()
    {
        return maxGameTimeInMinutes;
    }
    
    public int GetElapsedMinutes()
    {
        return Mathf.FloorToInt(
            (maxGameTimeInMinutes * 60f - gamePlayingTimer) / 60f
        );
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    private void PerformPause(object sender, System.EventArgs e)
    {
        if (state != GameState.GamePlaying || G.levelUpUI.IsShowing) return;

        if (!isPaused) 
        {
            SetOnPause();
        }
        else
        {
            SetUnpause();
        }
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
        if (G.input != null)
            G.input.OnEscapePerformed -= PerformPause;

        if (G.player != null)
            G.player.GetHealthSystem().OnDeath -= GameOver;
    }
}
