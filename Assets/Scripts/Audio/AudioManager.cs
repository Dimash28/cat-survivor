using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private AudioSource audioSource;

    private Dictionary<SoundSO, int> activeSoundCounts = new();

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(SoundSO sound)
    {
        activeSoundCounts.TryGetValue(sound, out int count);
        
        float overlapVolume = Mathf.Clamp(1f - (count * 0.15f), 0.2f, 1f);
        
        float volume = Mathf.Clamp(
            sound.volume * overlapVolume + Random.Range(-sound.volumeVariance, sound.volumeVariance), 
            0f, 1f
        );

        audioSource.pitch = sound.isPitchVariable
            ? sound.pitch + Random.Range(-sound.pitchVariance, sound.pitchVariance)
            : sound.pitch;

        audioSource.PlayOneShot(sound.clip, volume);

        activeSoundCounts[sound] = count + 1;
        StartCoroutine(DecrementSoundCount(sound, sound.clip.length));
    }

    private IEnumerator DecrementSoundCount(SoundSO sound, float delay)
    {
        yield return new WaitForSeconds(delay);
        activeSoundCounts[sound] = Mathf.Max(0, activeSoundCounts[sound] - 1);
    }
}
