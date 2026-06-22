using UnityEngine;

[CreateAssetMenu(menuName = "AudioSO/SoundSO")]
public class SoundSO : ScriptableObject
{
    public AudioCategory audioCategory = AudioCategory.SFX;

    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(-0.5f, 0f)] public float volumeVariance = 0;
    [Range(0.5f, 1.5f)] public float pitch = 1f;
    public bool isPitchVariable;
    [Range(0f, 0.5f)] public float pitchVariance = 0.1f;
    public bool hasCooldown;
    public float cooldown = 0.1f;

    public int maxSimultaneous = 3;
}
