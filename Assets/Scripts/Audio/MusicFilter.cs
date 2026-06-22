using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class MusicFilter : MonoBehaviour
{
    public static MusicFilter Instance { get; private set; }

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource normalMusic;
    [SerializeField] private AudioSource deathMusic;
    [SerializeField] private float filteredCutoff = 500f;
    [SerializeField] private float normalCutoff = 22000f;
    [SerializeField] private float normalPitch = 1f;
    [SerializeField] private float transitionDuration = 0.5f;

    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        audioMixer.SetFloat("MusicLowPass", normalCutoff);
        audioMixer.SetFloat("MusicPitch", normalPitch);

        G.game.OnGameOver += OnGameOver;
        G.game.OnPause += SetPauseFilter;
    }

    private void OnDestroy()
    {
        if (G.game != null)
        {
            G.game.OnGameOver -= OnGameOver;
            G.game.OnPause -= SetPauseFilter;
        }
    }

    private void SetPauseFilter()
    {
        if (isGameOver) return;
        StopAllCoroutines();
        bool paused = G.game.IsPaused();
        StartCoroutine(ApplyLowpass(paused));
    }

    private IEnumerator ApplyLowpass(bool enable)
    {
        float from = enable ? normalCutoff : filteredCutoff;
        float to   = enable ? filteredCutoff : normalCutoff;
        float timer = 0f;

        while (timer < transitionDuration)
        {
            timer += Time.unscaledDeltaTime;
            float t = timer / transitionDuration;
            float freq = Mathf.Lerp(from, to, t);
            audioMixer.SetFloat("MusicLowPass", freq);
            yield return null;
        }
        audioMixer.SetFloat("MusicLowPass", to);
    }

    private void OnGameOver()
    {
        isGameOver = true;
        StopAllCoroutines();
        StartCoroutine(CrossFadeToDeath());
    }

    private IEnumerator CrossFadeToDeath()
    {
        deathMusic.time = normalMusic.time;
        deathMusic.Play();
        audioMixer.SetFloat("MusicLowPass", normalCutoff); // сбросить фильтр

        float timer = 0f;
        while (timer < transitionDuration)
        {
            timer += Time.unscaledDeltaTime;
            float t = timer / transitionDuration;
            normalMusic.volume = Mathf.Lerp(1f, 0f, t);
            deathMusic.volume = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }
        normalMusic.volume = 0f;
        deathMusic.volume = 1f;
        deathMusic.loop = true;
        normalMusic.Stop();
    }
}