using System;
using UnityEngine;

namespace YusamCommon
{
    public sealed class YuCoTriggerDispatcher : MonoBehaviour
    {
        [SerializeField] 
        private bool _isDebuggingEnter; 
        [SerializeField] 
        private bool _isDebuggingStay;
        [SerializeField] 
        private bool _isDebuggingExit;
        
        public event Action<Collider> OnEnter;
        public event Action<Collider> OnStay;
        public event Action<Collider> OnExit;

        private void OnTriggerEnter(Collider other)
        {
            if (_isDebuggingEnter)
            {
                Debug.Log($"OnTriggerEnter {other.gameObject.name}");
            }
            OnEnter?.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            if (_isDebuggingStay)
            {
                Debug.Log($"OnTriggerStay {other.gameObject.name}");
            }
            OnStay?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (_isDebuggingExit)
            {
                Debug.Log($"OnTriggerExit {other.gameObject.name}");
            }
            OnExit?.Invoke(other);
        }
    }
}