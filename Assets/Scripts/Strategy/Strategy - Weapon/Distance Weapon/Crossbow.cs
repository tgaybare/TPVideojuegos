using Sound;
using System.Collections;
using UnityEngine;

public class Crossbow : DistanceWeapon
{
    private float _reloadTime = 500f;
    private GameObject _crossbowGameObject;

    private void Awake()
    {
        _soundPlayer = gameObject.GetComponentsInChildren<FixedSoundPlayer>()[1];
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

        for (int i = 0; i < this.ProjectilesPerAttack; i++)
        {
            boltSpawnPosition = new Vector3(transform.position.x, _crossbowGameObject.transform.position.y, transform.position.z);
            if (ExplosiveShot)
            {
                Instantiate(ExplosiveProjectilePrefab, boltSpawnPosition, transform.rotation);
            }
            else
            {
                Instantiate(ProjectilePrefab, boltSpawnPosition, transform.rotation);
            }

            yield return new WaitForSeconds(this.ProjectileDelay);
        }

    }
}
