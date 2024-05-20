using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.DungeonGeneration
{
    public class RoomEnemiesSpawner : RoomGameObjectsSpawner
    {
        public List<GameObject> EnemiesInRoom => _enemiesInRoom;
        [SerializeField] private List<GameObject> _enemiesInRoom = new List<GameObject>();

        protected override void Awake()
        {
            VerifyEnemyTags();
            base.Awake();
        }

        protected override void SpawnObjects(RandomSpawner data)
        {
            Debug.Log("Spawning enemies");
            int randomAmount = Random.Range(data.spawnerData.MinSpawn, data.spawnerData.MaxSpawn + 1);

            for (int i = 0; i < randomAmount; i++)
            {
                Vector3 spawnPosition = _gridController.AllocateRandomTile();
                GameObject enemy = Instantiate(data.spawnerData.ToSpawn, spawnPosition, Quaternion.identity, transform);
                _enemiesInRoom.Add(enemy);
                enemy.SetActive(false);
            }
        }

        private void VerifyEnemyTags()
        {
            foreach (RandomSpawner spawner in _randomSpawners)
            {
                if (!spawner.spawnerData.ToSpawn.CompareTag("Enemy"))
                    throw new System.Exception($"'{spawner.name}' objects to spawn do not have an 'Enemy' tag");
            }
        }

        public void RemoveEnemy(GameObject enemy)
        {
            _enemiesInRoom.Remove(enemy);
        }
    }
}