using System.Collections;
using System.Collections.Generic;
using Strategy.Strategy___Shooting;
using UnityEngine;

public interface IDistanceWeapon : IWeapon
{
    GameObject ProjectilePrefab { get; }
    int MaxProjectileCount { get; }
    void Reload();
}
