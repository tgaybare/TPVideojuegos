using System.Collections;
using System.Collections.Generic;
using Commands;
using Controllers;
using UnityEngine;

public class CharacterInputManager : MonoBehaviour
{
    
    // [SerializeField] private Sword _sword;
    // [SerializeField] private Bow _bow;
    
    private KeyCode _moveForward = KeyCode.W;
    private KeyCode _moveBackward = KeyCode.S;
    private KeyCode _moveLeft = KeyCode.A;
    private KeyCode _moveRight = KeyCode.D;
    // private KeyCode _attack1 = KeyCode.Mouse0;
    // private KeyCode _attack2 = KeyCode.Mouse1;
    
    private CmdMovement _cmdMoveForward;
    private CmdMovement _cmdMoveBackward;
    private CmdMovement _cmdMoveLeft;
    private CmdMovement _cmdMoveRight;

    // Start is called before the first frame update
    void Start()
    {
        _cmdMoveBackward = new CmdMovement(-transform.forward, GetComponent<IMoveable>());
        _cmdMoveForward = new CmdMovement(transform.forward, GetComponent<IMoveable>());
        _cmdMoveLeft = new CmdMovement(-transform.right, GetComponent<IMoveable>());
        _cmdMoveRight = new CmdMovement(transform.right, GetComponent<IMoveable>());
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown())
        // {
        //     
        // }
        
        //Movement 
        if (Input.GetKey(_moveForward)) EventQueueManager.instance.AddEventToQueue(_cmdMoveForward);
        if (Input.GetKey(_moveBackward)) EventQueueManager.instance.AddEventToQueue(_cmdMoveBackward);
        if (Input.GetKey(_moveLeft)) EventQueueManager.instance.AddEventToQueue(_cmdMoveLeft);
        if (Input.GetKey(_moveRight)) EventQueueManager.instance.AddEventToQueue(_cmdMoveRight);
        
        //Attacks


    }
}
