using System;
using UnityEngine;

namespace YusamCommon
{
    public sealed class YuCoControllerColliderHitDispatcher : MonoBehaviour
    {
        [SerializeField] 
        private bool _isDebugging; 
        
        public event Action<ControllerColliderHit> OnHit;
        
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (_isDebugging)
            {
                Debug.Log($"OnControllerColliderHit {hit.gameObject.name}");
            }
            OnHit?.Invoke(hit);
        }
    }
}