using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class SpiderAttack : MeleeWeapon
    {
        private Collider _spiderCollider;
        
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
    }
}