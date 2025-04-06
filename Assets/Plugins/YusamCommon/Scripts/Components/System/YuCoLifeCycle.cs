using System;
using UnityEngine;
using UnityEngine.Events;

namespace YusamCommon
{
    public class YuCoLifeCycle : YuCoMonoBehaviour
    {
        [SerializeField] 
        private UnityEvent _onAwake;
        [SerializeField] 
        private UnityEvent _onStart;
        [SerializeField] 
        private UnityEvent _onEnable;
        [SerializeField] 
        private UnityEvent _onUpdate;
        [SerializeField] 
        private UnityEvent _onLateUpdate;
        [SerializeField] 
        private UnityEvent _onFixedUpdate;
        [SerializeField] 
        private UnityEvent _onDisable;
        [SerializeField] 
        private UnityEvent _onDestroy;
        protected virtual void Awake()
        {
            _onAwake?.Invoke();
        }

        protected virtual void Start()
        {
            _onStart?.Invoke();
        }

        protected virtual void OnEnable()
        {
            _onEnable?.Invoke();
        }

        protected virtual void Update()
        {
            _onUpdate?.Invoke();
        }

        protected virtual void LateUpdate()
        {
            _onLateUpdate?.Invoke();
        }

        protected virtual void FixedUpdate()
        {
            _onFixedUpdate?.Invoke();
        }

        protected virtual void OnDisable()
        {
            _onDisable?.Invoke();
        }

        protected virtual void OnDestroy()
        {
            _onDestroy?.Invoke();
        }
        
        
    }
}