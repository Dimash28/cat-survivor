using UnityEngine;

public class ArtifactPickup : MonoBehaviour
{
    [SerializeField] private GameObject lockedVisual;
    [SerializeField] private GameObject unlockedVisual;
    [SerializeField] private Enemy miniBossPrefab;
    [SerializeField] private float miniBossSpawnRadius = 3f;
    [SerializeField] private SoundSO spawnMiniBossSound;
    [SerializeField] private SoundSO unlockSound;
    [SerializeField] private SoundSO pickupSound;
    
    private bool isLocked = true;
    private bool miniBossSpawned = false;

    private void Start()
    {
        lockedVisual.SetActive(false);
        unlockedVisual.SetActive(true);
    }

    public void Unlock()
    {
        isLocked = false;

        unlockedVisual.SetActive(true);
        lockedVisual.SetActive(false);

        G.audio.Play(unlockSound);
    }

    private void SpawnMiniBoss()
    {
        miniBossSpawned = true;

        Vector2 randomDir = Random.insideUnitCircle.normalized;
        Vector3 spawnPos = transform.position + (Vector3)randomDir * miniBossSpawnRadius;

        Enemy miniBoss = Instantiate(miniBossPrefab, spawnPos, Quaternion.identity);

        if (miniBoss.TryGetComponent<MiniBoss>(out MiniBoss mb))
            mb.SetGuardedArtifact(this);
        
        G.audio.Play(spawnMiniBossSound);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<Player>(out Player player)) return;

        if (isLocked && !miniBossSpawned)
        {
            SpawnMiniBoss();
            lockedVisual.SetActive(true);
            unlockedVisual.SetActive(false);
            return;
        }

        if (!isLocked)
        {
            G.artifact.OnArtifactCollected();
            G.audio.Play(pickupSound);
            Destroy(gameObject);
        }
    }
}