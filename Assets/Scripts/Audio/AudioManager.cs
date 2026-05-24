using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(SoundSO sound)
    {
        audioSource.pitch = sound.pitch;
        audioSource.PlayOneShot(sound.clip, sound.volume);
    }
}
