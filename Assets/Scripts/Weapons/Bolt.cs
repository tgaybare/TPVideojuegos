using System;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Bolt : MonoBehaviour, IProjectile
    {

        [SerializeField] private int _damage = 10;
        [SerializeField] private float _speed = 50;
        [SerializeField] private float _lifetime = 5;
        [SerializeField] private List<int> _layerMasks;

        public int Damage => _damage;
        public float Speed => _speed;
        public float LifeTime => _lifetime;

        private Vector3 _direction;

        private void Start()
        {
           // _direction = transform.forward;
        }

        private void Update()
        {
            //transform.position +=  Time.deltaTime * Speed * _direction ;
            Travel();

            _lifetime -= Time.deltaTime;
            if (_lifetime <= 0) Destroy(this.gameObject);
        }


        public void Travel()
        {
            transform.position += transform.forward * Time.deltaTime * Speed;
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (_layerMasks.Contains(collision.gameObject.layer))
            {
                IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
                damagable?.TakeDamage(Damage);

                Destroy(this.gameObject);
            }
        }
    }
}