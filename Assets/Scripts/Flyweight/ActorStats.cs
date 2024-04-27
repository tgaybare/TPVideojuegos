
using UnityEngine;

[CreateAssetMenu(fileName = "ActorStats", menuName = "Stats/Actors", order = 0)]
public class ActorStats: ScriptableObject
{
    [SerializeField] private Enemy1StatsValues stats;

    public int MaxLife => stats.MaxLife;
    public int Damage => stats.Damage;
    public float Speed => stats.Speed;

}

[System.Serializable]
public struct Enemy1StatsValues
{
    public int MaxLife;
    public int Damage;
    public float Speed;
}