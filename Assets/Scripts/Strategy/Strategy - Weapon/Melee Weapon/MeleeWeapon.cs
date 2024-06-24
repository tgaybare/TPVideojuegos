using System;
using System.Collections;
using System.Collections.Generic;
using Animations;
using Strategy.Strategy___Weapon;
using Unity.VisualScripting;
using UnityEngine;

namespace Weapons
{
    public class MeleeWeapon: MonoBehaviour, IWeapon
    {
        [SerializeField] protected List<int> layerMasks;

        protected MeleeAnimController _animController;
        
        protected virtual void Start()
        {
            _animController = gameObject.GetComponentInParent<MeleeAnimController>();
        }
        
        #region I_WEAPON_PROPERTIES
        
        public int Damage => _damage;
        private int _damage = 10; 
        
        #endregion


        #region I_WEAPON_METHODS

        public virtual void Attack()
        {
            _animController.Attack();
        }

        #endregion
        
    }
}