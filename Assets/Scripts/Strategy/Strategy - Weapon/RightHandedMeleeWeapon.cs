using System.Collections.Generic;
using Animations;
using Unity.VisualScripting;
using UnityEngine;
using Weapons;

namespace Strategy.Strategy___Weapon
{
    public class RightHandedMeleeWeapon :  MonoBehaviour, IWeapon
    {
        
        [SerializeField] protected List<int> layerMasks;
        
        protected RightHandedMeleeAttackAnimController _animController;
        protected virtual void Start()
        {
            _animController = gameObject.GetComponentInParent<RightHandedMeleeAttackAnimController>();
        }
        
        #region I_WEAPON_PROPERTIES
        
        public int Damage => _damage;
        private int _damage = 10; 
        
        #endregion
        
        public virtual void Attack()
        {
            _animController.Attack();
        }
    }
}