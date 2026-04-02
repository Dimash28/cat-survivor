using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private const int CHUNK_SIZE = 26;
    private const string STATIC_POINT_RIGHT = "Right";
    private const string STATIC_POINT_LEFT = "Left";
    private const string STATIC_POINT_UP = "Up";
    private const string STATIC_POINT_DOWN = "Down";
    private const string STATIC_POINT_RIGHT_UP = "RightUp";
    private const string STATIC_POINT_LEFT_UP = "LeftUp";
    private const string STATIC_POINT_RIGHT_DOWN = "RightDown";
    private const string STATIC_POINT_LEFT_DOWN = "LeftDown";
    [SerializeField] private List<GameObject> mapChunkList;
    [SerializeField] private Transform player;
    [SerializeField] private float checkerRadius;
    [SerializeField] private LayerMask mapLayerMask;
    
    private GameObject currentChunk;
    private Vector3 noChunkPosition;

    private void Start()
    {
        
    }

    private void Update()
    {
        ChunkChecker();
    }

    private void ChunkChecker()
    {
        if (!currentChunk)
        {
            return;
        }
        Vector2 inputVector = GameInput.Instance.GetInputVectorNormalized();
        Transform currentChunkStaticPoints = currentChunk.transform.Find("StaticPoints");

        if (inputVector.x > 0 && inputVector.y == 0) //right
        {
            if (!Physics2D.OverlapCircle(currentChunkStaticPoints.Find(STATIC_POINT_RIGHT).position, checkerRadius, mapLayerMask))
            {
                noChunkPosition = currentChunkStaticPoints.Find(STATIC_POINT_RIGHT).position;
                SpawnChunk();
            }
        }
        else if (inputVector.x < 0 && inputVector.y == 0) //left
        {
            if (!Physics2D.OverlapCircle(currentChunkStaticPoints.Find(STATIC_POINT_LEFT).position, checkerRadius, mapLayerMask))
            {
                noChunkPosition = currentChunkStaticPoints.Find(STATIC_POINT_LEFT).position;
                SpawnChunk();
            }
        }
        else if (inputVector.x == 0 && inputVector.y > 0) //up
        {
            if (!Physics2D.OverlapCircle(currentChunkStaticPoints.Find(STATIC_POINT_UP).position, checkerRadius, mapLayerMask))
            {
                noChunkPosition = currentChunkStaticPoints.Find(STATIC_POINT_UP).position;
                SpawnChunk();
            }
        }
        else if (inputVector.x == 0 && inputVector.y < 0) //down
        {
            if (!Physics2D.OverlapCircle(currentChunkStaticPoints.Find(STATIC_POINT_DOWN).position, checkerRadius, mapLayerMask))
            {
                noChunkPosition = currentChunkStaticPoints.Find(STATIC_POINT_DOWN).position;
                SpawnChunk();
            }
        }
        else if (inputVector.x > 0 && inputVector.y > 0) //up-right
        {
            if (!Physics2D.OverlapCircle(currentChunkStaticPoints.Find(STATIC_POINT_RIGHT_UP).position, checkerRadius, mapLayerMask))
            {
                noChunkPosition = currentChunkStaticPoints.Find(STATIC_POINT_RIGHT_UP).position;
                SpawnChunk();
            }
        }
        else if (inputVector.x < 0 && inputVector.y > 0) //up-Left
        {
            if (!Physics2D.OverlapCircle(currentChunkStaticPoints.Find(STATIC_POINT_LEFT_UP).position, checkerRadius, mapLayerMask))
            {
                noChunkPosition = currentChunkStaticPoints.Find(STATIC_POINT_LEFT_UP).position;
                SpawnChunk();
            }
        }
        else if (inputVector.x > 0 && inputVector.y < 0) //down-right
        {
            if (!Physics2D.OverlapCircle(currentChunkStaticPoints.Find(STATIC_POINT_RIGHT_DOWN).position, checkerRadius, mapLayerMask))
            {
                noChunkPosition = currentChunkStaticPoints.Find(STATIC_POINT_RIGHT_DOWN).position;
                SpawnChunk();
            }
        }
        else if (inputVector.x < 0 && inputVector.y < 0) //down-left
        {
            if (!Physics2D.OverlapCircle(currentChunkStaticPoints.Find(STATIC_POINT_LEFT_DOWN).position, checkerRadius, mapLayerMask))
            {
                noChunkPosition = currentChunkStaticPoints.Find(STATIC_POINT_LEFT_DOWN).position;
                SpawnChunk();
            }
        }
    }

    private void SpawnChunk()
    {
        int randomIndex = Random.Range(0, mapChunkList.Count);
        Instantiate(mapChunkList[randomIndex], noChunkPosition, Quaternion.identity);
    }

    public void SetCurrentChunk(GameObject chunk)
    {
        currentChunk = chunk;
    }

    public GameObject GetCurrentChunk()
    {
        return currentChunk;
    }

    public void ClearCurrentChunk()
    {
        currentChunk = null;
    }
}
