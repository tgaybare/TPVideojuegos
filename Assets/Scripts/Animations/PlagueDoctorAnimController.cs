using UnityEngine;

namespace Animations
{
    public class PlagueDoctorAnimController : MonoBehaviour, IAnimController
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
        }

        public void Walk()
        {
            _animator.SetBool("Walking", true);
        }

        public void StopWalking()
        {
            _animator.SetBool("Walking", false);
        }

        public void FinishAttack()
        {
            _animator.SetTrigger("MagicStaffDown");
        }
    }
}