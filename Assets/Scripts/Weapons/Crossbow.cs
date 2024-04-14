using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : DistanceWeapon
{
    public override void Attack() => Instantiate(
        ProjectilePrefab, 
        transform.position, 
        transform.rotation);

    public override void Reload() => base.Reload();
}
