﻿using System;
using System.Collections.Generic;
using Strategy.Strategy___Weapon;
using Unity.VisualScripting;
using UnityEngine;

namespace Weapons
{
    public class MeleeWeapon: MonoBehaviour, IWeapon
    {
        [SerializeField] protected List<int> layerMasks;

        
        #region I_WEAPON_PROPERTIES
        
        public int Damage => _damage;
        private int _damage = 10; 
        
        #endregion


        #region I_WEAPON_METHODS

        public virtual void Attack()
        {
            Vector3 defaultPosition = transform.position;
            transform.position = transform.forward * 2 + transform.position;
            transform.position = defaultPosition; //TODO: hacer con una animacion
        }

        #endregion
        
    }
}