using System;
using Managers;
using UnityEngine;

namespace Strategy.Strategy___Weapon
{
    public class LifeController : MonoBehaviour, IDamageable
    {
        public ActorStats Stats => stats;
        [SerializeField] private ActorStats stats;
        
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
                    ActionManager.instance.ActionGameOver(false);
                }
                
                Destroy(gameObject);
                
                Invoke(nameof(LoadMenuScreen),6f);
            }
        }
        
        private void LoadMenuScreen() => UnitySceneManager.instance.Load_MenuScreen();
    }
}