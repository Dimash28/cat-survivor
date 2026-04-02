using System.Collections.Generic;
using UnityEngine;

public class PropSpawnSystem : MonoBehaviour
{
    [SerializeField] private List<Transform> propSpawnPointList;
    [SerializeField] private List<GameObject> propPrefabList;

    private void Start()
    {
        SpawnProps();
    }

    private void SpawnProps()
    {
        foreach(Transform propSpawnPoint in propSpawnPointList)
        {
            int randomIndex = Random.Range(0, propPrefabList.Count);
            Instantiate(propPrefabList[randomIndex], propSpawnPoint.position, Quaternion.identity);
        }
    }
}
