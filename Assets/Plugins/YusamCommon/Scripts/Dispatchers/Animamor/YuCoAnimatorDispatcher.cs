using System;
using UnityEngine;
using UnityEngine.Events;

namespace YusamCommon
{
    [RequireComponent(typeof(Animator))]
    [DisallowMultipleComponent]
    public class YuCoAnimatorDispatcher : MonoBehaviour
    {
        [SerializeField] 
        protected UnityEvent awakeEventHandler;
        
        [SerializeField] 
        protected UnityEvent startEventHandler;
        
        private Animator _animator;
        
        protected virtual void Awake()
        {
            awakeEventHandler?.Invoke();
        }

        private void Start()
        {
            startEventHandler?.Invoke();
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