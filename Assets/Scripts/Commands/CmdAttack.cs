using Strategy.Strategy___Shooting;

namespace Commands
{
    public class CmdAttack : ICommand
    {
        private IWeapon _weapon;
        
        public CmdAttack(IWeapon weapon)
        {
            _weapon = weapon;
        }

        public void Do()
        {
            _weapon.Attack();
        }
    }
}