using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Upgrades;
using Commands;
using Menu;
using Strategy.Strategy___Movement;
using Strategy.Strategy___Weapon;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Weapons;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class CharacterInputManager : MonoBehaviour
{

    // [SerializeField] private Sword _sword;
    // [SerializeField] private Bow _bow;

    private IWeapon _currentAttackStrategy;
    [SerializeField] private IDistanceWeapon _distanceWeapon;
    [SerializeField] private IWeapon _meleeWeapon;

    private IMoveable _player;


    private KeyCode _moveForward = KeyCode.W;
    private KeyCode _moveBackward = KeyCode.S;
    private KeyCode _moveLeft = KeyCode.A;
    private KeyCode _moveRight = KeyCode.D;
    private KeyCode _attack = KeyCode.Mouse0;
    private KeyCode _dodge = KeyCode.LeftShift;

    private KeyCode _chooseMelee = KeyCode.Alpha1;
    private KeyCode _chooseDistance = KeyCode.Alpha2;
    private bool _isMelee;
    
    private CmdRotateTowardsMouse _cmdRotateTowardsMouse;
    private CmdMovement _cmdMoveForward;
    private CmdMovement _cmdMoveBackward;
    private CmdMovement _cmdMoveLeft;
    private CmdMovement _cmdMoveRight;
    private CmdMovement _cmdMoveForwardLeft;
    private CmdMovement _cmdMoveBackwardLeft;
    private CmdMovement _cmdMoveForwardRight;
    private CmdMovement _cmdMoveBackwardRight;
    private CmdAttack _cmdAttack;
    private CmdAttack _cmdSecondAttack;
    private CmdDodge _cmdDodge;

    [SerializeField] private int dodgeDuration = 200; // in ms
    [SerializeField] private int dodgeCooldown = 2000; // in ms
    private int _dodgeCooldownTimer = 0;
    private Vector3 lastCharacterDirection; 
    
    [SerializeField] private int shotCooldown = 500; // in ms
    private int _shotCooldownTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<IMoveable>();

        _meleeWeapon = GetComponentInChildren<Sword>();
        _distanceWeapon = GetComponent<Crossbow>();
        _isMelee = false;
        _currentAttackStrategy = _distanceWeapon;

        //45 degree view
        Quaternion rotation = Quaternion.AngleAxis(-45, Vector3.up);

        // Movement directions
        Vector3 backward = rotation * new Vector3(0, 0, -1);
        Vector3 forward = rotation * new Vector3(0, 0, 1);
        Vector3 left = rotation * new Vector3(-1, 0, 0);
        Vector3 right = rotation * new Vector3(1, 0, 0);
        Vector3 forwardLeft = Vector3.Normalize(forward + left);
        Vector3 forwardRight = Vector3.Normalize(forward + right);
        Vector3 backwardLeft = Vector3.Normalize(backward + left);
        Vector3 backwardRight = Vector3.Normalize(backward + right);


        // Movement Commands
        _cmdRotateTowardsMouse = new CmdRotateTowardsMouse(_player);
        _cmdMoveBackward = new CmdMovement(backward, _player);
        _cmdMoveForward = new CmdMovement(forward, _player);
        _cmdMoveLeft = new CmdMovement(left, _player);
        _cmdMoveRight = new CmdMovement(right, _player);
        _cmdMoveForwardLeft = new CmdMovement(forwardLeft, _player);
        _cmdMoveForwardRight = new CmdMovement(forwardRight, _player);
        _cmdMoveBackwardLeft = new CmdMovement(backwardLeft, _player);
        _cmdMoveBackwardRight = new CmdMovement(backwardRight, _player);

        // Attack
        _cmdAttack = new CmdAttack(_currentAttackStrategy);
        _cmdSecondAttack = new CmdAttack(_meleeWeapon);
        
        // Dodge
        _cmdDodge = new CmdDodge(GetComponent<IMoveable>(), dodgeDuration);
    }

    // Update is called once per frame
    void Update()
    {

        // Dodge
        if (Input.GetKeyDown(_dodge) && _dodgeCooldownTimer >= dodgeCooldown)
        {
            EventQueueManager.instance.AddEventToQueue(_cmdDodge);
            _dodgeCooldownTimer = 0;
        }
        else
        {
            _dodgeCooldownTimer += (int)(Time.deltaTime * 1000);
        }


        // Change weapon
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
        if (Input.GetKeyDown(_attack) && _shotCooldownTimer >= shotCooldown && _currentAttackStrategy == _distanceWeapon)
        {
            Debug.Log("Pressed Mouse 0");
            EventQueueManager.instance.AddEventToQueue(_cmdAttack);
            _shotCooldownTimer = 0;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Pressed Mouse 1");
            EventQueueManager.instance.AddEventToQueue(_cmdSecondAttack);
        }
        else
        {
            _shotCooldownTimer += (int)(Time.deltaTime * 1000);
        }

        

        if(Input.GetKeyDown(KeyCode.H)) // For testing purposes
        {
            if(UIManager.instance.IsUpgradePickerActive())
                UIManager.instance.HideUpgradePicker();
            else
                UIManager.instance.ShowUpgradePicker();
        }

    }

    // We use FixedUpdate for movement because it's physics related
    private void FixedUpdate()
    {
        //Rotation
        _cmdRotateTowardsMouse.Do();
        
        //Movement 
        if (Input.GetKey(_moveForward))
        {
            if (Input.GetKey(_moveLeft))
                _cmdMoveForwardLeft.Do();
                
            else if (Input.GetKey(_moveRight))
                _cmdMoveForwardRight.Do();
            else
                _cmdMoveForward.Do();
        }
        else if (Input.GetKey(_moveBackward))
        {
            if (Input.GetKey(_moveLeft))
                _cmdMoveBackwardLeft.Do();
            else if (Input.GetKey(_moveRight))
                _cmdMoveBackwardRight.Do();
            else
                _cmdMoveBackward.Do();
        }
        else if (Input.GetKey(_moveLeft)) _cmdMoveLeft.Do();
        else if (Input.GetKey(_moveRight)) _cmdMoveRight.Do();
    }
}
