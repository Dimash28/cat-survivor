using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    private MapController mapController;
    [SerializeField] private GameObject targetChunk;

    private void Start()
    {
        mapController = FindAnyObjectByType<MapController>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mapController.SetCurrentChunk(targetChunk);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(mapController.GetCurrentChunk() == targetChunk)
            {
                mapController.ClearCurrentChunk();
            }
        }
    }
}
