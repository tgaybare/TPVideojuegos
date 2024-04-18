using System.Collections;
using System.Collections.Generic;
using Commands;
using Controllers;
using Strategy.Strategy___Shooting;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterInputManager : MonoBehaviour
{

    // [SerializeField] private Sword _sword;
    // [SerializeField] private Bow _bow;

    private IWeapon _currentAttackStrategy;
    [SerializeField] private IDistanceWeapon _distanceWeapon;
    // [SerializeField] private IMeleeWeapon _meleeWeapon;
    
    private KeyCode _moveForward = KeyCode.W;
    private KeyCode _moveBackward = KeyCode.S;
    private KeyCode _moveLeft = KeyCode.A;
    private KeyCode _moveRight = KeyCode.D;
    private KeyCode _attack = KeyCode.Mouse0;

    private KeyCode _chooseMelee = KeyCode.Alpha1;
    private KeyCode _chooseDistance = KeyCode.Alpha2;
    private bool _isMelee;
    
    private CmdMovement _cmdMoveForward;
    private CmdMovement _cmdMoveBackward;
    private CmdMovement _cmdMoveLeft;
    private CmdMovement _cmdMoveRight;
    private CmdMovement _cmdMoveForwardLeft;
    private CmdMovement _cmdMoveBackwardLeft;
    private CmdMovement _cmdMoveForwardRight;
    private CmdMovement _cmdMoveBackwardRight;
    private CmdAttack _cmdAttack;

    // Start is called before the first frame update
    void Start()
    {
        // _melee = GetComponent<IMelee>();
        _distanceWeapon = GetComponent<Crossbow>();
        _isMelee = false;
        _currentAttackStrategy = _distanceWeapon;
        Debug.Log(_currentAttackStrategy);

        // Movement directions
        Vector3 backward = new Vector3(0, 0, -1);
        Vector3 forward = new Vector3(0, 0, 1);
        Vector3 left = new Vector3(-1, 0, 0);
        Vector3 right = new Vector3(1, 0, 0);
        Vector3 forwardLeft = 0.75f * (forward + left);
        Vector3 forwardRight = 0.75f * (forward + right);
        Vector3 backwardLeft = 0.75f * (backward + left);
        Vector3 backwardRight = 0.75f * (backward + right);


        // Movement Commands
        _cmdMoveBackward = new CmdMovement(backward, GetComponent<IMoveable>());
        _cmdMoveForward = new CmdMovement(forward, GetComponent<IMoveable>());
        _cmdMoveLeft = new CmdMovement(left, GetComponent<IMoveable>());
        _cmdMoveRight = new CmdMovement(right, GetComponent<IMoveable>());
        _cmdMoveForwardLeft = new CmdMovement(forwardLeft, GetComponent<IMoveable>());
        _cmdMoveForwardRight = new CmdMovement(forwardRight, GetComponent<IMoveable>());
        _cmdMoveBackwardLeft = new CmdMovement(backwardLeft, GetComponent<IMoveable>());
        _cmdMoveBackwardRight = new CmdMovement(backwardRight, GetComponent<IMoveable>());

        // Attack
        _cmdAttack = new CmdAttack(_currentAttackStrategy);
    }

    // Update is called once per frame
    void Update()
    {
        
        //Movement 
        if (Input.GetKey(_moveForward)) 
        {
            if(Input.GetKey(_moveLeft))
                EventQueueManager.instance.AddEventToQueue(_cmdMoveForwardLeft);
            else if(Input.GetKey(_moveRight))
                EventQueueManager.instance.AddEventToQueue(_cmdMoveForwardRight);
            else
                EventQueueManager.instance.AddEventToQueue(_cmdMoveForward);
        }
        else if (Input.GetKey(_moveBackward))
        {
            if (Input.GetKey(_moveLeft))
                EventQueueManager.instance.AddEventToQueue(_cmdMoveBackwardLeft);
            else if (Input.GetKey(_moveRight))
                EventQueueManager.instance.AddEventToQueue(_cmdMoveBackwardRight);
            else
                EventQueueManager.instance.AddEventToQueue(_cmdMoveBackward);
        }
        else if (Input.GetKey(_moveLeft)) EventQueueManager.instance.AddEventToQueue(_cmdMoveLeft);
        else if (Input.GetKey(_moveRight)) EventQueueManager.instance.AddEventToQueue(_cmdMoveRight);
        


        //Change weapon
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            if (!_isMelee)
            {
                // _currentAttackStrategy = GetComponent<IMeleeWeapon>();
                _cmdAttack = new CmdAttack(_currentAttackStrategy);
            }
            else
            {
                _currentAttackStrategy = GetComponent<IDistanceWeapon>();
                _cmdAttack = new CmdAttack(_currentAttackStrategy);
            
            }
        }
        else if (Input.GetKeyDown(_chooseMelee))
        {
            // _currentAttackStrategy = GetComponent<IMeleeWeapon>();
            _cmdAttack = new CmdAttack(_currentAttackStrategy);
        }
        else if (Input.GetKeyDown(_chooseDistance))
        {
            _currentAttackStrategy = GetComponent<IDistanceWeapon>();
            _cmdAttack = new CmdAttack(_currentAttackStrategy);
        }

        //Attacks
        if (Input.GetKeyDown(_attack)) EventQueueManager.instance.AddEventToQueue(_cmdAttack);
        

    }
}
