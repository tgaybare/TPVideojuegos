using System;
using System.Collections;
using System.Collections.Generic;
using Sound;
using UnityEngine;

public class Crossbow : DistanceWeapon
{
    private float _reloadTime = 500f;

    private void Awake()
    {
        _soundPlayer = gameObject.GetComponentsInChildren<FixedSoundPlayer>()[1];
    }

    private FixedSoundPlayer _soundPlayer;
    public override void Attack()
    {
        GameObject crossbow = GameObject.FindWithTag("Crossbow");
        Vector3 position = new Vector3(transform.position.x, crossbow.transform.position.y, transform.position.z);
        _soundPlayer.Play();

        Instantiate(
            ProjectilePrefab, 
            position, 
            transform.rotation);
    }

    

    public override void Reload() => base.Reload();
}
