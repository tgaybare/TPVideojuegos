using Strategy.Strategy___Movement;
using UnityEngine;
using UnityEngine.UIElements;


// FIXME: No se esta usando porque llena la EventQueue
namespace Commands
{
    public class CmdRotateTowardsMouse : ICommand
    {
        private IMoveable _moveable;

        public CmdRotateTowardsMouse(IMoveable moveable)
        {
            _moveable = moveable;
        }

        public void Do()
        {
            Ray mouseProjectionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            _moveable.RotateTowards(mouseProjectionRay);
        }
    }
}