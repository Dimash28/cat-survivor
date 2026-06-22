using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    public float MasterVolume { get; private set; }
    public float SfxVolume { get; private set; }
    public float MusicVolume { get; private set; }
    public float UIVolume { get; private set; }
    public bool IsFullscreen { get; private set; }

    private const string MASTER_VOLUME_KEY = "MasterVolume";
    private const string SFX_VOLUME_KEY = "SfxVolume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string UI_VOLUME_KEY = "UIVolume";
    private const string FULLSCREEN_KEY = "Fullscreen";

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    private void Load()
    {
        MasterVolume = PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 1f);
        SfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f);
        MusicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f);
        UIVolume = PlayerPrefs.GetFloat(UI_VOLUME_KEY, 1f);
        IsFullscreen = PlayerPrefs.GetInt(FULLSCREEN_KEY, 1) == 1;

        Apply();
    }

    public void SetMasterVolume(float value)
    {
        MasterVolume = value;
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, value);
        Apply();
    }

    public void SetSfxVolume(float value)
    {
        SfxVolume = value;
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, value);
        Apply();
    }

    public void SetMusicVolume(float value)
    {
        MusicVolume = value;
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, value);
        Apply();
    }

    public void SetUIVolume(float value)
    {
        UIVolume = value;
        PlayerPrefs.SetFloat(UI_VOLUME_KEY, value);
        Apply();
    }

    public void SetFullscreen(bool value)
    {
        IsFullscreen = value;
        PlayerPrefs.SetInt(FULLSCREEN_KEY, value ? 1 : 0);
        Apply();
    }

    private void Apply()
    {
        Screen.fullScreen = IsFullscreen;

        if (G.audio != null)
            G.audio.SetVolumes(MasterVolume, SfxVolume, MusicVolume, UIVolume);
    }
}