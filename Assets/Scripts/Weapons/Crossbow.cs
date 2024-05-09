using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : DistanceWeapon
{
    private float _reloadTime = 500f;
    public override void Attack()
    {
        GameObject crossbow = GameObject.FindWithTag("Crossbow");
        Vector3 position = new Vector3(transform.position.x, crossbow.transform.position.y, transform.position.z);

        // if (CompareTag("Player"))
        // {
        //     Instantiate(
        //         ProjectilePrefab, 
        //         position, 
        //         transform.rotation);
        // }

        Instantiate(
            ProjectilePrefab, 
            position, 
            transform.rotation);
    }

    

    public override void Reload() => base.Reload();
}
