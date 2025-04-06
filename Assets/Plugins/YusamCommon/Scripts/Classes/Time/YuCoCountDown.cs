using System;
using UnityEngine;

namespace YusamCommon
{
    [Serializable]
    public sealed class YuCoCountDown : YuCoObject
    {
        [SerializeField]
        private float _duration;

        [SerializeField]
        private float _current;

        public YuCoCountDown(float duration)
        {
            _duration = duration;
            _current = duration;
        }

        public YuCoCountDown(float duration, float current)
        {
            _duration = duration;
            _current = current;
        }

        public float GetDuration()
        {
            return _duration;
        }
        
        public void Reset(float duration)
        {
            _duration = duration;
            _current = duration;
        }
        
        public void Reset(float duration, float current)
        {
            _duration = duration;
            _current = current;
        }
        
        public bool IsExpired()
        {
            return _current <= 0;
        }

        public float GetProgress()
        {
            return _current / _duration;
        }

        public void Reset()
        {
            _current = _duration;
        }

        public void Tick(float deltaTime)
        {
            _current = Mathf.Max(0, _current - deltaTime);
        }
        
        public override string ToString()
        {
            return $"{nameof(_duration)}: {_duration}, {nameof(_current)}: {_current}";
        }
    }
}