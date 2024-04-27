using Controllers;
using UnityEngine;
using UnityEngine.UIElements;


// FIXME: No se esta usando porque llena la EventQueue
namespace Commands
{
    public class CmdRotateTowards : ICommand
    {

        private Ray _rayDirection;
        private IMoveable _moveable;

        public CmdRotateTowards(Ray rayDirection, IMoveable moveable)
        {
            _moveable = moveable;
            _rayDirection = rayDirection;
        }

        public void Do()
        {
            _moveable.RotateTowards(_rayDirection);
        }
    }
}