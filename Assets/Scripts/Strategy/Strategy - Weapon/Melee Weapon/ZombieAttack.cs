
using System;
using Sound;
using UnityEngine;

public class ZombieAttack : MeleeWeapon
{

    private FixedSoundPlayer _soundPlayer;

    private void Awake()
    {
        _soundPlayer = gameObject.GetComponent<FixedSoundPlayer>();
    }

    public void OnTriggerEnter(Collider other)
    {
        // Debug.Log("_animController.IsAttacking() = " + _animController.IsAttacking());
        // Debug.Log(layerMasks.Contains(other.gameObject.layer));
        
        if (layerMasks.Contains(other.gameObject.layer))
        {
            _soundPlayer.Play();
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            damageable?.TakeDamage(Damage);
        }
    }
    
}
