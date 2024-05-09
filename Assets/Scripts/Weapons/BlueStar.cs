using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Weapons
{
    public class BlueStar : MonoBehaviour, IProjectile
    {
        
        [SerializeField] private int _damage = 20;
        [SerializeField] private float _speed = 10;
        [SerializeField] private float _lifetime = 5;
        [SerializeField] private List<int> _layerMasks;

        public int Damage => _damage;
        public float Speed => _speed;
        public float LifeTime => _lifetime;

        private Vector3 _direction;
        
        private void Awake()
        {
            transform.position += transform.forward * 4f;
        }

        private void Update()
        {
            Travel();

            _lifetime -= Time.deltaTime;
            if (_lifetime <= 0) Destroy(this.gameObject);
        }
        
        public void Travel()
        {
            transform.position += transform.forward * Time.deltaTime * Speed;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (_layerMasks.Contains(other.gameObject.layer))
            {
                IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
                damageable?.TakeDamage(Damage);
                // ActionManager.instance.BoltHit();
                
                Destroy(this.gameObject);
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (_layerMasks.Contains(collision.gameObject.layer))
            {
                IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
                damageable?.TakeDamage(Damage);
                // ActionManager.instance.BoltHit();
                
                Destroy(this.gameObject);
            }
        }
        
    }
}