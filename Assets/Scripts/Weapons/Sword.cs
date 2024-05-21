using Sound;
using UnityEngine;

namespace Weapons
{
    public class Sword : MeleeWeapon
    {
        
        private FixedSoundPlayer _soundPlayer;
        
        private void Start()
        {
            _soundPlayer = gameObject.GetComponent<FixedSoundPlayer>();
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