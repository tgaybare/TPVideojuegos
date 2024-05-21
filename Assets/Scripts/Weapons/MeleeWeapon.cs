using System;
using System.Collections;
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
            Transform parentTransform = transform.parent;
            transform.position = parentTransform.forward * 2 + parentTransform.position;
            StartCoroutine(WaitForTrigger(defaultPosition));
            //TODO: hacer con una animacion
        }

        private IEnumerator WaitForTrigger(Vector3 defaultPosition)
        {
            yield return new WaitForSeconds(0.2f);
            transform.position = defaultPosition;
        }

        #endregion
        
    }
}