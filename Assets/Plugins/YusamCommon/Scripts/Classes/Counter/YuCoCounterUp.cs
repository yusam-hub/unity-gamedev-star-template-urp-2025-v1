using System;
using UnityEngine;

namespace YusamCommon
{
    [Serializable]
    public sealed class YuCoCounterUp : YuCoObject
    {
        [SerializeField]
        private int _max;

        [SerializeField]
        private int _current;

        public YuCoCounterUp(int max)
        {
            _max = max;
            _current = 0;
        }

        public YuCoCounterUp(int max, int current)
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
            _current = 0;
        }
        
        public bool IsExpired()
        {
            return _current >= _max;
        }

        public float GetProgress()
        {
            return (float) _current / _max;
        }

        public void Reset()
        {
            _current = 0;
        }

        public void Tick()
        {
            _current++;
        }
        
        public override string ToString()
        {
            return $"{nameof(_max)}: {_max}, {nameof(_current)}: {_current}";
        }
    }
}