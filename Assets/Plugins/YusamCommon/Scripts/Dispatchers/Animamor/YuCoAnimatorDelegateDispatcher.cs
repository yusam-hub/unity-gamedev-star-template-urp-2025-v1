using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YusamCommon
{
    public delegate void AtCoAnimatorEventDelegate(string animEvent);
    public delegate void AtCoAnimatorEventBoneDelegate(string animEvent, Transform animBone);
    public delegate void AtCoAnimatorStateDelegate(string stateId, AnimatorStateInfo stateInfo, int layerIndex);
    
    [RequireComponent(typeof(Animator))]
    [DisallowMultipleComponent]
    public class YuCoAnimatorDelegateDispatcher : YuCoAnimatorDispatcher
    {
        [SerializeField] protected Transform rootBone;
        [SerializeField] protected bool ragdollDisableOnAwake = true;
        
        [SerializeField] protected bool _isDebuggingOnAnimEvent; 
        [SerializeField] protected bool _isDebuggingReceiveStateEnter;
        [SerializeField] protected bool _isDebuggingReceiveStateExit;

        public event AtCoAnimatorEventDelegate OnAnimEvent;
        public event AtCoAnimatorStateDelegate OnStateEntered;
        public event AtCoAnimatorStateDelegate OnStateExited;
        
        private List<Rigidbody> _ragdolls = new();

        protected override void Awake()
        {
            base.Awake();
            
            if (rootBone)
            {
                _ragdolls = new List<Rigidbody>(rootBone.GetComponentsInChildren<Rigidbody>());
            }
            
            if (ragdollDisableOnAwake)
            {
                RagdollDisable();
            }
        }

        internal void ReceiveEvent(string animEvent)
        {
            if (_isDebuggingOnAnimEvent)
            {
                Debug.Log($"ReceiveEvent {animEvent}");
            }
            
            OnAnimEvent?.Invoke(animEvent);
        }
        
        internal void ReceiveStateEnter(string stateId, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_isDebuggingReceiveStateEnter)
            {
                Debug.Log($"ReceiveStateEnter {stateId} {stateInfo} {layerIndex}");
            }
            OnStateEntered?.Invoke(stateId, stateInfo, layerIndex);
        }

        internal void ReceiveStateExit(string stateId, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_isDebuggingReceiveStateExit)
            {
                Debug.Log($"ReceiveStateExit {stateId} {stateInfo} {layerIndex}");
            }
            OnStateExited?.Invoke(stateId, stateInfo, layerIndex);
        }
        
        public void RagdollEnable()
        {
            if (!GetAnimator().enabled) return;
            GetAnimator().enabled = false;
            foreach (var rb in _ragdolls)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
        }
        
        public void RagdollDisable()
        {
            if (GetAnimator().enabled) return;
            
            GetAnimator().enabled = true;
            foreach (var rb in _ragdolls)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }
        }

        public bool RagdollExists()
        {
            return _ragdolls.Count > 0;
        }
        public void RagdollHit(Vector3 hitPoint, Vector3 force)
        {
            RagdollEnable();
            var closesRagdoll = _ragdolls.OrderBy(rb => Vector3.Distance(rb.position, hitPoint)).First();
            if (closesRagdoll)
            {
                closesRagdoll.AddForceAtPosition(force, hitPoint, ForceMode.Impulse);
            }
        }
    }
}