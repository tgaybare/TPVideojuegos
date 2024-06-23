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
        public ActorStats Stats => _stats;
        [SerializeField] private ActorStats _stats;

        private VariableSoundPlayer _soundPlayer;
        [SerializeField] private AudioClip deathSound;
        
        public float MaxLife => _maxLifeWithUpgrades;
        private float _maxLifeWithUpgrades;

        public float Life => life;
        [SerializeField] private float life;

        private void Start()
        {
            life = _stats.MaxLife;
            _maxLifeWithUpgrades = _stats.MaxLife;
            _soundPlayer = gameObject.GetComponent<VariableSoundPlayer>();
        }

        public void TakeDamage(int damage)
        {
            life -= damage;
            if(gameObject.CompareTag("Player"))
                ActionManager.instance.CharacterLifeChange(life, _maxLifeWithUpgrades);
          

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

        public void Heal(int healAmount)
        {
            life += healAmount;
            if (life > _maxLifeWithUpgrades)
            {
                life = _maxLifeWithUpgrades;
            }
            if(gameObject.CompareTag("Player"))
                ActionManager.instance.CharacterLifeChange(life, _maxLifeWithUpgrades);
        }

        public void incrementMaxLife(float multiplier)
        {
            _maxLifeWithUpgrades = (int)(_maxLifeWithUpgrades * multiplier);
            life *= multiplier;
            ActionManager.instance.CharacterLifeChange(life, _maxLifeWithUpgrades);
        }
    }
}