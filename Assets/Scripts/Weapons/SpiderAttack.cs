using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Weapons
{
    public class SpiderAttack : MeleeWeapon
    {
        private Collider _spiderCollider;
        
        
        public int Damage => _damage;
        private int _damage = 50; 
        
        private void Start()
        {
            _spiderCollider = gameObject.GetComponent<Collider>();
        }

        public override void Attack()
        {
            // Attack animation logic
            //TODO: animation
            
            // Attack logic
            _spiderCollider.enabled = true;
            StartCoroutine(WaitForTrigger());
            
        }

        private IEnumerator WaitForTrigger()
        {
            yield return new WaitForSeconds(0.5f);
            _spiderCollider.enabled = false;
        }

        //private IEnumerator AttackAnimationDelay()
        //{
            //while (animation.isPlaying)
            //{
            //    yield return null;
            //}
            
         //   yield return new WaitForSeconds(2);
        //}
        
        public void OnCollisionEnter(Collision collision)
        {
            Debug.Log("es colision");
            if (layerMasks.Contains(collision.gameObject.layer))
            {
                IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
                damageable?.TakeDamage(Damage);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            
            if (layerMasks.Contains(other.gameObject.layer))
            {
                Debug.Log("es trigger");
                IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
                damageable?.TakeDamage(Damage);
            }
        }
    }
}