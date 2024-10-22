using Strategy.Strategy___Weapon;
using UnityEngine;

public interface IDistanceWeapon : IWeapon
{

    GameObject ProjectilePrefab { get; }
    int MaxProjectileCount { get; }
    void Reload();
}
