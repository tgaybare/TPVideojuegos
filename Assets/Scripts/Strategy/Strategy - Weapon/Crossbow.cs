using System;
using System.Collections;
using System.Collections.Generic;
using Sound;
using UnityEngine;

public class Crossbow : DistanceWeapon
{
    private float _reloadTime = 500f;
    private GameObject _crossbowGameObject;
    
    public int ProjectilesPerAttack { get; set; } = 1;
    [SerializeField] private float _projectileDelay = 0.2f;

    private void Awake()
    {
        _soundPlayer = gameObject.GetComponentInChildren<FixedSoundPlayer>();

        _crossbowGameObject = GameObject.FindWithTag("Crossbow");
    }

    private FixedSoundPlayer _soundPlayer;
    public override void Attack()
    {
        _soundPlayer.Play();
        StartCoroutine(SpawnBolts());
    }    

    public override void Reload() => base.Reload();

    private IEnumerator SpawnBolts()
    {
        Vector3 boltSpawnPosition;

        for (int i = 0; i < ProjectilesPerAttack; i++)
        {
            boltSpawnPosition = new Vector3(transform.position.x, _crossbowGameObject.transform.position.y, transform.position.z);
            Instantiate(ProjectilePrefab, boltSpawnPosition, transform.rotation);
            yield return new WaitForSeconds(_projectileDelay);
        }

    }
}
