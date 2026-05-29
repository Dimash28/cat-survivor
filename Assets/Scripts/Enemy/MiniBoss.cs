using UnityEngine;

public class MiniBoss : Enemy
{
    [SerializeField] private ArtifactPickup guardedArtifact;

    protected override void OnDeath()
    {
        base.OnDeath();
        guardedArtifact.Unlock();
    }
}
