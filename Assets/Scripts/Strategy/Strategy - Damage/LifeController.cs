using System;
using System.Collections;
using Animations;
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
        
        private MainCharacterAnimController _animatorController;

        private void Awake()
        {
            life = _stats.MaxLife;
            _maxLifeWithUpgrades = _stats.MaxLife;
            _soundPlayer = gameObject.GetComponent<VariableSoundPlayer>();
            _animatorController = gameObject.GetComponent<MainCharacterAnimController>();
        }

        public void TakeDamage(int damage)
        {
            life -= damage;
            if(life < 0)
            {
                life = 0;
            }

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
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);        
        }

        private void Die() {
            ActionManager.instance.ActionGameOver(false);
            _animatorController.Death();
        }

        public void UpdateLife(int newLife)
        {
            life = newLife;
            if(gameObject.CompareTag("Player"))
                ActionManager.instance.CharacterLifeChange(life, _maxLifeWithUpgrades);
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
            ActionManager.instance.CharacterMaxLifeChange(_stats.MaxLife, _maxLifeWithUpgrades);
        }
    }
}