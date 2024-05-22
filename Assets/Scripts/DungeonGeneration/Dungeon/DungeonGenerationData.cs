using UnityEngine;

[CreateAssetMenu(fileName = "DungeonGenerationData.asset", menuName = "DungeonGenerationData/DungeonData", order = 0)]

public class DungeonGenerationData : ScriptableObject 
{
    [SerializeField] private int _numberOfCrawlers;
    [SerializeField] private int _iterationMin;
    [SerializeField] private int _iterationMax;

    public int NumberOfCrawlers { get => _numberOfCrawlers; }
    public int IterationMin { get => _iterationMin; }
    public int IterationMax { get => _iterationMax; }

}