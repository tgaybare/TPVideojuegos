using UnityEngine;

[CreateAssetMenu(fileName = "ActorStats", menuName = "Stats/Actors/Player", order = 0)]

public class ActorStats: ScriptableObject
{
    [SerializeField] private ActorStatValues stats;

    public int MaxLife => stats.MaxLife;
    public float Speed => stats.Speed;
    public float AttackRate => stats.AttackRate;

}

[System.Serializable]
public struct ActorStatValues
{
    public int MaxLife;
    public float Speed;
    public float AttackRate;
}