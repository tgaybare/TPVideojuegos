using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class RoomObjectSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct RandomSpawner // TODO: Diferenciar EnemySpawner
    {
        public string name;
        public SpawnerData spawnerData;
        public List<GameObject> spawnedObjects; 
    }

    [SerializeField] public GridController _gridController;
    [SerializeField] private RandomSpawner[] _randomSpawners;

    private void Start()
    {
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
        int randomAmount = Random.Range(data.spawnerData.MinSpawn, data.spawnerData.MaxSpawn + 1);

        for (int i = 0; i < randomAmount; i++)
        {
            Vector3 spawnPosition = _gridController.AllocateRandomTile();
            GameObject spawned = Instantiate(data.spawnerData.ToSpawn, spawnPosition, Quaternion.identity, transform);
            Debug.Log($"Spawned from '{data.name}' at ({spawnPosition.x},{spawnPosition.z})");
        }
    }

    public List<GameObject> SpawnedEnemies() {
        List<GameObject> enemies = new List<GameObject>();
        foreach (RandomSpawner spawner in _randomSpawners)
        {
            foreach (GameObject obj in spawner.spawnedObjects)
            {
                if(obj != null && obj.CompareTag("Enemy"))
                {
                    enemies.Add(obj);
                }
            }
        }
        return enemies;
    }
}

