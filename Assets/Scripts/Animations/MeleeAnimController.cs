using UnityEngine;

namespace Animations
{
    public class MeleeAnimController : MonoBehaviour, IAnimController
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
        
        public void SetAttacking(bool attacking)
        {
            _animator.SetBool("Attacking", attacking);
        }

        public void Walk()
        {
            _animator.SetBool("Walking", true);
        }

        public void StopWalking()
        {
            _animator.SetBool("Walking", false);
        }

        public bool IsAttacking()
        {
            return _animator.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack");
        }

        public void Die()
        {
            _animator.SetBool("Death", true);
        }
    }
}