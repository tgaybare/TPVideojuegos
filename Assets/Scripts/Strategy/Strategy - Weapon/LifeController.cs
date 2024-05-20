using System;

using Managers;
using UnityEngine;

namespace Strategy.Strategy___Weapon
{
    public class LifeController : MonoBehaviour, IDamageable
    {
        public ActorStats Stats => stats;
        public ActorStats stats;
        
        public float MaxLife => stats.MaxLife;
        public float Life => life;
        [SerializeField] private float life;

        private void Start()
        {
            life = MaxLife;
        }

        public void TakeDamage(int damage)
        {
            life -= damage;
            if (life <= 0)
            {
                if (gameObject.CompareTag("Player"))
                {
                    Die();
                }
                
                Destroy(gameObject);
                
                
            }
        }

        private void Die() {
            ActionManager.instance.ActionGameOver(false);
        }
    }
}