using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using Commands;
using Strategy.Strategy___Weapon;
using Strategy.Strategy___Movement;

public class MeleeAIManager : MonoBehaviour
{
    
    // [SerializeField] private GameObject player;
    private IWeapon _currentAttackStrategy;
    [SerializeField] private IWeapon _meleeWeapon;

    [SerializeField] private GameObject player;
    [SerializeField] private float attackRange = 0.5f;
    
    private CmdMovement _cmdMoveForward;
    private CmdAttack _cmdAttack;
    
    private void Awake()
    {
        // Movement directions
        Vector3 forward = new Vector3(0, 0, 1);


        // Movement Commands
        _cmdMoveForward = new CmdMovement(forward, GetComponent<IMoveable>());
        
        // Attack
        _cmdAttack = new CmdAttack(_meleeWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        IMoveable enemy = GetComponent<IMoveable>();
        Ray playerDirection = new Ray(transform.position, player.transform.position - transform.position);
        // Debug.Log(playerDirection.direction);
        enemy.RotateTowards(playerDirection);
        
        //Attack
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            _cmdAttack.Do();
        }
        else
        {
            _cmdMoveForward.Do();
        }
    }

}
