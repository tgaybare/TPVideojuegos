using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDistanceWeapon
{
    GameObject ProjectilePrefab { get; }
    int Damage { get; }
    int MaxProjectileCount { get; }
    void Attack();
    void Reload();
}
