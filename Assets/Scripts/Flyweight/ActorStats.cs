using UnityEngine;

[CreateAssetMenu(fileName = "ActorStats", menuName = "Stats/Actors/Player", order = 0)]

public class ActorStats: ScriptableObject
{
    [SerializeField] private ActorStatValues _stats;

    public int MaxLife { 
        get => _stats.MaxLife;
        set => _stats.MaxLife = value;
    }

    public float Speed => _stats.Speed;
    public float AttackCooldown => _stats.AttackCooldown;

}

[System.Serializable]
public struct ActorStatValues
{
    public int MaxLife;
    public float Speed;
    public float AttackCooldown;
}