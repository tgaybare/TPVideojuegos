using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Stats/Actors/Enemy", order = 1)]
public class EnemyStats : ActorStats
{
    [SerializeField] private EnemyStatValues enemyStats;

    public int Damage => enemyStats.Damage;
    public int AttackRange => enemyStats.AttackRange;
    
}

[System.Serializable]
public struct EnemyStatValues
{
    public int Damage;
    public int AttackRange;
}