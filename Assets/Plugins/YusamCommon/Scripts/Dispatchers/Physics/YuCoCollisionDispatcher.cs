using System;
using UnityEngine;

namespace YusamCommon
{
    public sealed class YuCoCollisionDispatcher : MonoBehaviour
    {
        [SerializeField] 
        private bool _isDebugging;
        
        public event Action<Collision> OnEnter;
        public event Action<Collision> OnStay;
        public event Action<Collision> OnExit;
        
        private void OnCollisionEnter(Collision other)
        {
            if (_isDebugging)
            {
                Debug.Log($"OnCollisionEnter {other.gameObject.name}");
            }
            OnEnter?.Invoke(other);
        }

        private void OnCollisionStay(Collision other)
        {
            if (_isDebugging)
            {
                Debug.Log($"OnCollisionStay {other.gameObject.name}");
            }
            OnStay?.Invoke(other);
        }

        private void OnCollisionExit(Collision other)
        {
            if (_isDebugging)
            {
                Debug.Log($"OnCollisionExit {other.gameObject.name}");
            }
            OnExit?.Invoke(other);
        }
    }
}