using System;
using Sound;
using Unity.VisualScripting;
using UnityEngine;

namespace Weapons
{
    public class Sword : MeleeWeapon
    {
        
        private FixedSoundPlayer _soundPlayer;
        
        protected override void Start()
        {
            base.Start();
            _soundPlayer = gameObject.GetComponent<FixedSoundPlayer>();
        }

        private void Update()
        {
            
        }

        public void OnTriggerEnter(Collider other)
        {
            if (layerMasks.Contains(other.gameObject.layer))
            {
                _soundPlayer.Play();
                IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
                damageable?.TakeDamage(Damage);
            }
        }
        
    }
}