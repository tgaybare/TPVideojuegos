using UnityEngine;

namespace Animations
{
    public class MagicStaffAttackAnimController: MonoBehaviour
    {
        private Animator _animator;
        
        public bool IsAttacking { get; private set; }

        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
        }
        
        public void StartAttack()
        {
            _animator.SetTrigger("MagicStaffUp");
            while (_animator.GetCurrentAnimatorStateInfo(0).IsName("MagicStaffUp"))
            {
                // Wait for animation to finish
                Debug.Log("Hola");
            }
        }
        
        public void FinishAttack()
        {
            _animator.SetTrigger("MagicStaffDown");
        }
    }
}