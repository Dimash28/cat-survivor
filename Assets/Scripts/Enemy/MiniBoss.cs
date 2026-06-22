using UnityEngine;
using System.Collections;

public class MiniBoss : Enemy
{
    [SerializeField] private SoundSO miniBossDeathSound;
    private ArtifactPickup guardedArtifact;

    public void SetGuardedArtifact(ArtifactPickup artifact)
    {
        guardedArtifact = artifact;
    }

    protected override void OnDeath()
    {
        base.OnDeath();

        if (guardedArtifact != null)
            guardedArtifact.Unlock();

        G.audio.Play(miniBossDeathSound);
    }
}