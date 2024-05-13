using UnityEngine;

namespace Weapons
{
    public class Sword : MeleeWeapon
    {
        
        public void OnCollisionEnter(Collision collision)
        {
            if (layerMasks.Contains(collision.gameObject.layer))
            {
                IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
                damageable?.TakeDamage(Damage);
            }
        }
        
    }
}