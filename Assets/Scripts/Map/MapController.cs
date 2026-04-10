using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private Transform mapParent;
    [SerializeField] private List<GameObject> mapChunkList;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private int loadRadius = 2;
    [SerializeField] private float chunkSize = 26f;
    
    private ChunkOptimizationSystem chunkOptimizationSystem;
    private Dictionary<Vector2Int, GameObject> activeChunkDictionary;

    private void Awake()
    {
        activeChunkDictionary = new Dictionary<Vector2Int, GameObject>();
        chunkOptimizationSystem = GetComponent<ChunkOptimizationSystem>();
    }

    private void Update()
    {
        UpdateChunksAroundCamera();
    }

    private void UpdateChunksAroundCamera()
    {
        if(mainCamera == null) return;

        Vector2Int currentChunkCoordinate = WorldToChunkCoordinate(mainCamera.position);

        for (int x = -loadRadius; x <= loadRadius; x++)
        {
            for (int y = -loadRadius; y <= loadRadius; y++)
            {
                Vector2Int coordinate = currentChunkCoordinate + new Vector2Int(x, y);
                if (!activeChunkDictionary.ContainsKey(coordinate))
                {
                    SpawnChunkAtCoordinate(coordinate);
                }
            }
        }
    }

    private void SpawnChunkAtCoordinate(Vector2Int coordinate)
    {
        Vector3 spawnPosition = new Vector3(
            coordinate.x * chunkSize,
            coordinate.y * chunkSize,
            0
        );

        int randomIndex = Random.Range(0, mapChunkList.Count);
        GameObject latestChunk = Instantiate(mapChunkList[randomIndex], spawnPosition, Quaternion.identity, mapParent);

        activeChunkDictionary.Add(coordinate, latestChunk);
        chunkOptimizationSystem.AddSpawnedChunkInList(latestChunk);
    } 

    private Vector2Int WorldToChunkCoordinate(Vector3 worldPosition)
    {
        return new Vector2Int(
            Mathf.FloorToInt(worldPosition.x / chunkSize),
            Mathf.FloorToInt(worldPosition.y / chunkSize)
        );
    }
    public Transform GetCameraTransform()
    {
        return mainCamera;
    }
}
