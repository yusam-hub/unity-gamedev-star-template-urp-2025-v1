using UnityEngine;

namespace YusamCommon
{
    public interface IYuCoTransformPool
    {
        public Transform Rent();
        public void Return(Transform transform);
        
        public void Init(int initialCount);
        public void Clear();
    }
}