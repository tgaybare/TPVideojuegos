using Controllers;
using UnityEngine;

namespace Commands
{
    public class CmdMovement: ICommand
    {
        
        private Vector3 _direction;
        private IMoveable _moveable;
        
        public CmdMovement(Vector3 direction, IMoveable moveable)
        {
            _moveable = moveable;
            _direction = direction;
        }

        public void Do()
        {
            _moveable.Move(_direction);
        }
    }
}