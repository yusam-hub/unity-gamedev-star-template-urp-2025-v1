using System;
using UnityEngine;

namespace YusamCommon
{
    [Serializable]
    public sealed class YuCoCounterDown
    {
        [SerializeField]
        private int _max;

        [SerializeField]
        private int _current;

        public YuCoCounterDown(int max)
        {
            _max = max;
            _current = max;
        }

        public YuCoCounterDown(int max, int current)
        {
            _max = max;
            _current = current;
        }

        public int GetCurrent()
        {
            return _current;
        }
        
        public int GetMax()
        {
            return _max;
        }
        
        public void Reset(int max)
        {
            _max = max;
            _current = _max;
        }
        
        public bool IsExpired()
        {
            return _current <= 0;
        }

        public float GetProgress()
        {
            return (float) _current / _max;
        }

        public void Reset()
        {
            _current = _max;
        }

        public void Tick()
        {
            _current--;
        }
        
        public override string ToString()
        {
            return $"{nameof(_max)}: {_max}, {nameof(_current)}: {_current}";
        }
    }
}