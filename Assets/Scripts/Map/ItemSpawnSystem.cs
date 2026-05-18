using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnSystem : MonoBehaviour
{
    [SerializeField] private List<Transform> itemSpawnPointList;
    [SerializeField] private List<GameObject> itemPrefabList;

    private void Start()
    {
        SpawnItems();
    }

    private void SpawnItems()
    {
        foreach(Transform propSpawnPoint in itemSpawnPointList)
        {
            int randomIndex = Random.Range(0, itemPrefabList.Count);
            Instantiate(itemPrefabList[randomIndex], propSpawnPoint.position, Quaternion.identity, propSpawnPoint);
        }
    }
}
