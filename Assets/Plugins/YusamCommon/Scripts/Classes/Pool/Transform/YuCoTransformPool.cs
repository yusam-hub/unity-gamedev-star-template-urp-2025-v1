using System.Collections.Generic;
using UnityEngine;

namespace YusamCommon
{
    public sealed class YuCoTransformPool : YuCoObject, IYuCoTransformPool
    {
        private readonly Transform _prefab;
        private readonly Transform _container;
        private readonly Queue<Transform> _queue = new();

        public YuCoTransformPool(Transform prefab, Transform container)
        {
            _prefab = prefab;
            _container = container;
        }

        public void Init(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var transform = DoCreate();
                _queue.Enqueue(transform);
            }
        }

        private Transform DoCreate()
        {
            var transform = Object.Instantiate(_prefab, _container);
            OnCreate(transform);
            return transform;
        }

        public Transform Rent()
        {
            if (!_queue.TryDequeue(out var transform))
            {
                transform = DoCreate();
            }

            OnRent(transform);
            return transform;
        }

        public void Return(Transform transform)
        {
            if (!_queue.Contains(transform))
            {
                OnReturn(transform);
                _queue.Enqueue(transform);
            }
        }

        public void Clear()
        {
            foreach (var transform in _queue)
            {
                OnDestroy(transform);
                Object.Destroy(transform);
            }

            _queue.Clear();
        }

        private void OnCreate(Transform transform)
        {
            transform.gameObject.SetActive(false);
        }

        private void OnDestroy(Transform transform)
        {
        }

        private void OnRent(Transform transform)
        {
            transform.gameObject.SetActive(true);
        }

        private void OnReturn(Transform transform)
        {
            transform.gameObject.SetActive(false);
            transform.SetParent(_container);
        }
    }
}