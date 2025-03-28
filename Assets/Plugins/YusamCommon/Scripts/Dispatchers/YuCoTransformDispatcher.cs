using UnityEngine;

namespace YusamCommon
{
    public sealed class YuCoTransformDispatcher : MonoBehaviour
    {
        [SerializeField] 
        private Transform _transform;

        public Transform GetTransform()
        {
            return _transform;
        }
    }
}