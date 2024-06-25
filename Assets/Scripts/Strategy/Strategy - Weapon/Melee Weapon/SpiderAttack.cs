using System;
using System.Collections;
using System.Collections.Generic;
using Sound;
using Unity.VisualScripting;
using UnityEngine;


    public class SpiderAttack : MeleeWeapon
    {
        private Collider _spiderCollider;
        [SerializeField] private Animation _animation;
        
        public int SpiderDamage => _spiderDamage;
        private int _spiderDamage = 50; 
        
        private FixedSoundPlayer _soundPlayer;
        
        private void Start()
        {
            _spiderCollider = gameObject.GetComponent<Collider>();
            _animation = gameObject.GetComponentInParent<Animation>();
            _soundPlayer = gameObject.GetComponent<FixedSoundPlayer>();
        }

        public override void Attack()
        {
            // Attack animation logic
            //TODO: animation
            
            // Attack logic
            _spiderCollider.enabled = true;
            _animation.Play();
            StartCoroutine(WaitForTrigger());
        }

        private IEnumerator WaitForTrigger()
        {
            yield return new WaitForSeconds(0.5f);
            _spiderCollider.enabled = false;
        }
        
        public void OnCollisionEnter(Collision collision)
        {
            if (layerMasks.Contains(collision.gameObject.layer))
            {
                _soundPlayer.Play();
                IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
                damageable?.TakeDamage(SpiderDamage);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (layerMasks.Contains(other.gameObject.layer))
            {
                _soundPlayer.Play();
                IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
                damageable?.TakeDamage(SpiderDamage);
            }
        }
    }
