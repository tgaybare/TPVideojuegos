using System;
using System.Collections;
using Managers;
using Sound;
using Unity.VisualScripting;
using UnityEngine;

namespace Strategy.Strategy___Weapon
{
    public class LifeController : MonoBehaviour, IDamageable
    {
        public ActorStats Stats => stats;
        public ActorStats stats;

        private VariableSoundPlayer _soundPlayer;
        [SerializeField] private AudioClip deathSound;
        
        public float MaxLife => stats.MaxLife;
        public float Life => life;
        [SerializeField] private float life;

        private void Start()
        {
            life = MaxLife;
            _soundPlayer = gameObject.GetComponent<VariableSoundPlayer>();
        }

        public void TakeDamage(int damage)
        {
            life -= damage;
            if (life <= 0)
            {
                //Play death sound for entities that have one assigned
                if (deathSound != null) 
                {
                    _soundPlayer.PlayOneShot(deathSound);
                    StartCoroutine(WaitForSound());
                }
                
                if (gameObject.CompareTag("Player"))
                {
                    Die();
                } else if (gameObject.CompareTag("Enemy"))
                {
                    KillEnemy(gameObject);
                }
            }
        }

        private IEnumerator WaitForSound()
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);        
        }

        private void Die() {
            ActionManager.instance.ActionGameOver(false);
            
        }

        private void KillEnemy(GameObject enemy)
        {
            ActionManager.instance.ActionEnemyKilled(enemy);
        }
    }
}