using System;
using Sound;
using UnityEngine;

namespace Strategy.Strategy___Weapon
{
    public class RightHandedSword : RightHandedMeleeWeapon
    {
        
        private FixedSoundPlayer _soundPlayer;
        
        protected override void Start()
        {
            base.Start();
            _soundPlayer = gameObject.GetComponent<FixedSoundPlayer>();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (layerMasks.Contains(other.gameObject.layer) && other.gameObject.CompareTag("Enemy") && _animController.IsAttacking())
            {
                _soundPlayer.Play();
                IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
                damageable?.TakeDamage(Damage);
            }
        }
    }
}