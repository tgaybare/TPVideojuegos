using System.Collections;
using System.Collections.Generic;
using Commands;
using Strategy.Strategy___Movement;
using Strategy.Strategy___Weapon;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Timeline.TimelinePlaybackControls;

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
    private KeyCode _dodge = KeyCode.LeftShift;

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
    private CmdDodge _cmdDodge;

    [SerializeField] private int dodgeDuration = 1500; // in ms
    [SerializeField] private int dodgeCooldown = 2000; // in ms
    private int _dodgeCooldownTimer = 0;
    
    [SerializeField] private int shotCooldown = 1000; // in ms
    private int _shotCooldownTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        // _melee = GetComponent<IMelee>();
        _distanceWeapon = GetComponent<Crossbow>();
        _isMelee = false;
        _currentAttackStrategy = _distanceWeapon;

        //45 degree view
        Quaternion rotation = Quaternion.AngleAxis(0, Vector3.up);

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
            EventQueueManager.instance.AddEventToQueue(_cmdAttack);
            _shotCooldownTimer = 0;
        }
        else
        {
            _shotCooldownTimer += (int)(Time.deltaTime * 1000);
        }



    }

    // We use FixedUpdate for movement because it's physics related
    private void FixedUpdate()
    {
        IMoveable Player = GetComponent<IMoveable>();
        Ray mouseProjectionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Player.RotateTowards(mouseProjectionRay);
        
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
