using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    [SerializeField] private float maxGameTimeInMinutes = 15f;

    private float gameStartingTimer = 3f;
    private float gamePlayingTimer;
    private bool isPaused;
    private GameState state;

    private void Awake()
    {
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
                gameStartingTimer -= Time.deltaTime;
                if(gameStartingTimer <= 0f)
                {
                    state = GameState.GamePlaying;
                }
            break;
            case GameState.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if(gamePlayingTimer <= 0f)
                {
                    state = GameState.GameOver;
                }
            break;
            case GameState.GameOver:
                
            break;
        }
    }

    private void PerformPause(object sender, System.EventArgs e)
    {
        if (!isPaused) SetOnPause();
        else SetUnpause();
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
}
