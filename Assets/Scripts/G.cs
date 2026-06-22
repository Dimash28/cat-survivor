using UnityEngine;
using UnityEngine.SceneManagement;

public static class G
{
    public static AudioManager audio { get; private set; }
    public static ExperienceSystem experience { get; private set; }
    public static GameInput input { get; private set; }
    public static GameManager game { get; private set; }
    public static Player player { get; private set; }
    public static PlayerStats stats { get; private set; }
    public static LevelUpUI levelUpUI { get; private set; }
    public static UpgradeSystem upgrade { get; private set; }
    public static ArtifactSequencer artifact { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        audio = Object.FindAnyObjectByType<AudioManager>();
        input = Object.FindAnyObjectByType<GameInput>();
        game = Object.FindAnyObjectByType<GameManager>();
        player = Object.FindAnyObjectByType<Player>();
        stats = Object.FindAnyObjectByType<PlayerStats>();
        experience = Object.FindAnyObjectByType<ExperienceSystem>();
        levelUpUI = Object.FindAnyObjectByType<LevelUpUI>();
        upgrade = Object.FindAnyObjectByType<UpgradeSystem>();
        artifact = Object.FindAnyObjectByType<ArtifactSequencer>();

        if (audio == null) Debug.LogError("[G] AudioManager not found in scene!");
        if (input == null) Debug.LogError("[G] GameInput not found in scene!");
        if (game == null) Debug.LogError("[G] GameManager not found in scene!");
        if (player == null) Debug.LogError("[G] Player not found in scene!");
        if (stats == null) Debug.LogError("[G] PlayerStats not found in scene!");
        if (experience == null) Debug.LogError("[G] ExperienceSystem not found in scene!");
        if (levelUpUI == null) Debug.LogError("[G] LevelUpUI not found in scene!");
        if (upgrade == null) Debug.LogError("[G] UpgradeSystem not found in scene!");
        if (artifact == null) Debug.LogError("[G] ArtifactSequencer not found in scene!");

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Initialize();
    }
}