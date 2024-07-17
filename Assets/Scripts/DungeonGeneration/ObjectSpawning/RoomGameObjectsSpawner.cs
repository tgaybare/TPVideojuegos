using UnityEngine;

public class RoomGameObjectsSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct RandomSpawner // TODO: Diferenciar EnemySpawner
    {
        public string name;
        public SpawnerData spawnerData;
    }

    [SerializeField] public GridController _gridController;
    [SerializeField] protected RandomSpawner[] _randomSpawners;

    protected virtual void Awake()
    {
        _gridController = GetComponentInChildren<GridController>();
    }

    protected virtual void Start()
    {
        InitializeObjectSpawning();
    }

    public void InitializeObjectSpawning()
    {
        foreach (RandomSpawner spawner in _randomSpawners)
        {
            this.SpawnObjects(spawner);
        }
    }

    protected virtual void SpawnObjects(RandomSpawner data)
    {
        int randomAmount = Random.Range(data.spawnerData.MinSpawn, data.spawnerData.MaxSpawn + 1);

        for (int i = 0; i < randomAmount; i++)
        {
            Vector3 spawnPosition = _gridController.AllocateRandomTile();
            GameObject spawned = Instantiate(data.spawnerData.ToSpawn, spawnPosition, Quaternion.identity, transform);
        }
    }
}

