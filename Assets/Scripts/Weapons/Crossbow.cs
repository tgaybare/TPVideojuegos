using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : DistanceWeapon
{
    public override void Attack()
    {
        GameObject crossbow = GameObject.FindWithTag("Crossbow");
        Vector3 position = new Vector3(transform.position.x, crossbow.transform.position.y, transform.position.z);
        Instantiate(
            ProjectilePrefab, 
            position, 
            transform.rotation);
    }

    

    public override void Reload() => base.Reload();
}
