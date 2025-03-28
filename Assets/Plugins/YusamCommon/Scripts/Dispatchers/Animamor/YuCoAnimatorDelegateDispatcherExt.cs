using System;
using System.Collections.Generic;
using UnityEngine;

namespace YusamCommon
{

    [RequireComponent(typeof(Animator))]
    [DisallowMultipleComponent]
    public sealed class YuCoAnimatorDelegateDispatcherExt : YuCoAnimatorDelegateDispatcher
    {
        [Serializable]
        private struct AtCoAnimatorDelegateStringTransform
        {
            public string animEvent;
            public Transform animBone;
        }
        [SerializeField] private AtCoAnimatorDelegateStringTransform[] animEventBoneLinks;
        private readonly Dictionary<string, Transform> _animEventBoneTransforms = new();
        
        public event AtCoAnimatorEventBoneDelegate OnAnimBoneEvent;

        
        protected override void Awake()
        {
            base.Awake();
            
            OnAnimEvent += OnAnimEventExt;
            foreach (var item in animEventBoneLinks)
            {
                _animEventBoneTransforms.TryAdd(item.animEvent, item.animBone);
            }
        }

        private void OnAnimEventExt(string animevent)
        {
            if (_animEventBoneTransforms.TryGetValue(animevent, out var bone))
            {
                if (_isDebuggingOnAnimEvent)
                {
                    Debug.Log($"ReceiveEvent {animevent} {bone.position}");
                }
                
                OnAnimBoneEvent?.Invoke(animevent, bone); 
            }
        }
    }
}