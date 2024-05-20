using UnityEngine;

namespace Weapons
{
    public class Sword : MeleeWeapon
    {
        
        public void OnTriggerEnter(Collider other)
        {
            if (layerMasks.Contains(other.gameObject.layer))
            {
                Debug.Log("Toy entrando al OnCollision correcto");
                Debug.Log(other.gameObject.name);
                IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
                damageable?.TakeDamage(Damage);
            }
        }
        
    }
}