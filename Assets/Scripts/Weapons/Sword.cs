using UnityEngine;

namespace Weapons
{
    public class Sword : MeleeWeapon
    {
        
        public void OnTriggerEnter(Collider other)
        {
            if (layerMasks.Contains(other.gameObject.layer))
            {
                IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
                damageable?.TakeDamage(Damage);
            }
        }
        
    }
}