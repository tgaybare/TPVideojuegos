using System;
using UnityEngine;

namespace Animations
{
    public class MeleeAttackAnimController : MonoBehaviour
    {
        private Animator _animator;

        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
        }
        
        public void Attack()
        {
            _animator.SetTrigger("MeleeAttack");
        }
    }
}