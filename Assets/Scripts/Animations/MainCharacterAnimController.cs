using UnityEngine;

namespace Animations
{
    public class MainCharacterAnimController : MonoBehaviour
    {
        private Animator _animator;

        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
        }

        public void Death()
        {
            _animator.SetTrigger("Death");
        }
    }
}