using UnityEngine;

namespace YusamCommon
{
    public abstract class YuCoArraySo: YuCoScriptableObject
    {
        private int _index = 0;

        protected abstract int GetMaxArray();

        protected int GetNextIndex()
        {
            _index++;
            if (_index >= GetMaxArray())
            {
                _index = 0;
            }
            return _index;
        }
        
        protected int GetRandomIndex()
        {
            _index = Random.Range(0, GetMaxArray() - 1);
            return _index;
        }
    }
}