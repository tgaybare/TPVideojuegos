using Strategy.Strategy___Movement;

namespace Commands
{
    public class CmdDodge : ICommand
    {
        private IMoveable _moveable;
        private int _duration;

        public CmdDodge(IMoveable moveable, int duration)
        {
            _moveable = moveable;
            _duration = duration;
        }

        public void Do()
        {
            _moveable.Dodge(_duration);
        }
    }
}