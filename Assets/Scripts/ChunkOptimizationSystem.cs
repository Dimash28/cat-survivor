using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChunkOptimizationSystem : MonoBehaviour
{
    [SerializeField] private float maxOptimizationDistance;
    private float optimizationDistance;
    private MapController mapController;

    private List<GameObject> spawnedChunkList;

    private void Start()
    {
        mapController = GetComponent<MapController>();
        spawnedChunkList = new List<GameObject>();
    }

    private void Update()
    {
        ChunkOptimizer();
    }
    
    public void AddSpawnedChunkInList(GameObject latestChunk)
    {
        spawnedChunkList.Add(latestChunk);
    }

    private void ChunkOptimizer()
    {
        foreach (GameObject chunk in spawnedChunkList)
        {
            if(chunk == null)
            {
                return;
            }

            Transform player = mapController.GetPlayerTransform();
            optimizationDistance = Vector3.Distance(player.position, chunk.transform.position);
            if(optimizationDistance > maxOptimizationDistance)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
