using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerData.asset", menuName = "Spawners/Spawner")]

public class SpawnerData : ScriptableObject
{
    [SerializeField] private SpawnerDataValues _values;

    public GameObject ToSpawn => _values.ToSpawn;
    public int MinSpawn => _values.MinSpawn;
    public int MaxSpawn => _values.MaxSpawn;


}

[System.Serializable]
public struct SpawnerDataValues
{
    public GameObject ToSpawn;
    public int MinSpawn;
    public int MaxSpawn;
}