using System;
using UnityEngine;

namespace Weapons
{
    public class Bolt : MonoBehaviour, IProjectile
    {
        public int Damage { get; }
        public float Speed { get; set; }
        public float LifeTime { get; }

        private Vector3 _direction;

        private void Start()
        {
            _direction = transform.forward;
            Speed = 10;
        }

        private void Update()
        {
            transform.position +=  Time.deltaTime * Speed * _direction ;
        }


        public void Travel()
        {
            throw new NotImplementedException();
        }

        public void OnCollisionEnter(Collision collision)
        {
            throw new System.NotImplementedException();
        }
    }
}