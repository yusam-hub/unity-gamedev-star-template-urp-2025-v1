using System;
using System.Collections.Generic;

namespace YusamCommon
{
    [Serializable]
    public class YuCoAnimatorSpeedMultiplier
    {
        public YuCoAnimatorSpeedMultiplierStruct[] sets;
        
        private Dictionary<float, float> _dictionarySets = new ();

        public void Init()
        {
            foreach (var item in sets)
            {
                _dictionarySets.TryAdd(item.sourceSpeed, item.animatorSpeed);
            }
        }

        public float GetAnimatorSpeed(float sourceSpeed)
        {
            return _dictionarySets.GetValueOrDefault(sourceSpeed, 1);
        }
        
    }
}