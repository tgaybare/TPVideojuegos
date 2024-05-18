using System;
using System.Collections;
using Commands;
using Strategy.Strategy___Movement;
using Strategy.Strategy___Weapon;
using UnityEngine;

namespace Enemy
{
    public class EnemyAIManager : MonoBehaviour
    {
        [SerializeField] private IWeapon _weapon;
        
        public EnemyStats Stats => stats;
        [SerializeField] private EnemyStats stats;
        
        [SerializeField] private GameObject player;
        private float AttackRange => stats.AttackRange;
        private float AttackRate => stats.AttackRate;
        private float RestAfterAttack => stats.RestAfterAttack;
    
        private CmdMovement _cmdMoveDirection;
        private CmdAttack _cmdAttack;
        
        private double timeSinceLastAttack = 0;
        private double waitTime = 0;
        
        private void Awake()
        {
            // Movement directions
            Vector3 forward = new Vector3(0, 0, 1);

            _weapon = GetComponentInChildren<IWeapon>();

            // Movement Commands
            _cmdMoveDirection = new CmdMovement(forward, GetComponent<IMoveable>());
        
            // Attack
            _cmdAttack = new CmdAttack(_weapon);
        }
        
        void Update()
        {
            if (waitTime < RestAfterAttack)
            {
                waitTime += Time.deltaTime;
                return;
            }
            IMoveable enemy = GetComponent<IMoveable>();
            Ray playerDirection = new Ray(transform.position, player.transform.position - transform.position);
            enemy.RotateTowards(playerDirection);
        
            //Attack
            if (Vector3.Distance(transform.position, player.transform.position) < AttackRange)
            {
                if (timeSinceLastAttack > AttackRate)
                {
                    _cmdAttack.Do();
                    waitTime = 0;
                    timeSinceLastAttack = 0;
                }
                timeSinceLastAttack += Time.deltaTime;
            }
            else
            {
                _cmdMoveDirection.ChangeDirection(playerDirection.direction);
                _cmdMoveDirection.Do();
                timeSinceLastAttack += Time.deltaTime;
                
            }
        }

    }
}