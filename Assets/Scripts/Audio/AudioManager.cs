using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource uiSource;
    [SerializeField] private AudioSource weaponsSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip soundtrack;
    [SerializeField] private AudioSource deathMusicSource;
    [SerializeField] private AudioClip deathSoundtrack;

    private Dictionary<SoundSO, int> activeSoundCounts = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        musicSource.clip = soundtrack;
        musicSource.loop = true;
        musicSource.Play();

        deathMusicSource.clip = deathSoundtrack;
        deathMusicSource.loop = false;
        deathMusicSource.volume = 0f;
    }

    public void Play(SoundSO sound)
    {
        activeSoundCounts.TryGetValue(sound, out int count);

        if (count >= sound.maxSimultaneous) return;

        AudioSource source = GetSourceForSound(sound);

        float volume = Mathf.Clamp(
            sound.volume + Random.Range(-sound.volumeVariance, sound.volumeVariance),
            0f, 1f
        );

        source.pitch = sound.isPitchVariable
            ? sound.pitch + Random.Range(-sound.pitchVariance, sound.pitchVariance)
            : sound.pitch;

        source.PlayOneShot(sound.clip, volume);

        activeSoundCounts[sound] = count + 1;
        StartCoroutine(DecrementSoundCount(sound, sound.clip.length));
    }

    public void SetVolumes(float master, float sfx, float music, float ui)
    {
        sfxSource.volume = master * sfx;
        uiSource.volume = master * ui;
        musicSource.volume = master * music;
    }
    
    private AudioSource GetSourceForSound(SoundSO sound)
    {
        return sound.audioCategory switch
        {
            AudioCategory.SFX => sfxSource,
            AudioCategory.UI => uiSource,
            AudioCategory.Weapons => weaponsSource,
            _ => sfxSource
        };
    }

    private IEnumerator DecrementSoundCount(SoundSO sound, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        activeSoundCounts[sound] = Mathf.Max(0, activeSoundCounts[sound] - 1);
    }
}
