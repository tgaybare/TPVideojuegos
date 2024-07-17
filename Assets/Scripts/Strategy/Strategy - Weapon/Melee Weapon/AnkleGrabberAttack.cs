
using System;
using Sound;
using UnityEngine;

public class AnkleGrabberAttack : MeleeWeapon
{
    private FixedSoundPlayer _soundPlayer;
    
    public int AnkleGrabberDamage => _ankleGrabberDamage;
    private int _ankleGrabberDamage = 40;

    private void Awake()
    {
        _soundPlayer = gameObject.GetComponent<FixedSoundPlayer>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (layerMasks.Contains(other.gameObject.layer))
        {
            _soundPlayer.Play();
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            damageable?.TakeDamage(AnkleGrabberDamage);
        }
    }
}
