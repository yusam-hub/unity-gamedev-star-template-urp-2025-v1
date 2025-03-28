using UnityEngine;

namespace YusamCommon
{
    [RequireComponent(typeof(Animator))]
    [DisallowMultipleComponent]
    public class YuCoAnimatorDispatcher : MonoBehaviour
    {
        private Animator _animator;
        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public Animator GetAnimator()
        {
            if (!_animator)
            {
                _animator = GetComponent<Animator>();
            }
            return _animator;
        }
    }
}