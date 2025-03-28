//using JetBrains.Annotations;
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
        [SerializeField] protected bool _isDebuggingOnAnimEvent; 
        [SerializeField] protected bool _isDebuggingReceiveStateEnter;
        [SerializeField] protected bool _isDebuggingReceiveStateExit;

        public event AtCoAnimatorEventDelegate OnAnimEvent;
        public event AtCoAnimatorStateDelegate OnStateEntered;
        public event AtCoAnimatorStateDelegate OnStateExited;
        
        //[UsedImplicitly]
        internal void ReceiveEvent(string animEvent)
        {
            if (_isDebuggingOnAnimEvent)
            {
                Debug.Log($"ReceiveEvent {animEvent}");
            }
            
            OnAnimEvent?.Invoke(animEvent);
        }
        
        //[UsedImplicitly]
        internal void ReceiveStateEnter(string stateId, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_isDebuggingReceiveStateEnter)
            {
                Debug.Log($"ReceiveStateEnter {stateId} {stateInfo} {layerIndex}");
            }
            OnStateEntered?.Invoke(stateId, stateInfo, layerIndex);
        }

        //[UsedImplicitly]
        internal void ReceiveStateExit(string stateId, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_isDebuggingReceiveStateExit)
            {
                Debug.Log($"ReceiveStateExit {stateId} {stateInfo} {layerIndex}");
            }
            OnStateExited?.Invoke(stateId, stateInfo, layerIndex);
        }
    }
}