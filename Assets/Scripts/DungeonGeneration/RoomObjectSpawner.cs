using System.Collections;
using UnityEngine;

 public class RoomObjectSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct RandomSpawner
    {
        public string name;
        public SpawnerData spawnerData;
    }

    [SerializeField] public GridController _gridController;
    [SerializeField] private RandomSpawner[] _randomSpawners;

    private void Start()
    {
        Debug.Log("RoomObjectSpawner initialized");
        _gridController = GetComponentInChildren<GridController>();
        InitializeObjectSpawning();
    }

    public void InitializeObjectSpawning()
    {
        foreach (RandomSpawner spawner in _randomSpawners)
        {
            SpawnObjects(spawner);
        }
    }

    private void SpawnObjects(RandomSpawner data)
    {
        Debug.Log($"Spawning objects from '{data.name}'");
        int randomAmount = Random.Range(data.spawnerData.MinSpawn, data.spawnerData.MaxSpawn + 1);
        Debug.Log($"Spawning {randomAmount} objects");

        for (int i = 0; i < randomAmount; i++)
        {
            Vector3 spawnPosition = _gridController.AllocateRandomTile();
            Instantiate(data.spawnerData.ToSpawn, spawnPosition, Quaternion.identity, transform);
            Debug.Log($"Spawned object from '{data.name}' at ({spawnPosition.x},{spawnPosition.y},{spawnPosition.z})");
        }
    }
}

