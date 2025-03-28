using System.Collections.Generic;
using UnityEngine;

namespace YusamCommon
{
    public class YuCoTransformPool : IYuCoTransformPool
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
            for (int i = 0; i < count; i++)
            {
                var transform = GameObject.Instantiate(_prefab, _container);
                OnCreate(transform);
                _queue.Enqueue(transform);
            }
        }

        public Transform Rent()
        {
            if (!_queue.TryDequeue(out Transform transform))
            {
                transform = GameObject.Instantiate(_prefab, _container);
                OnCreate(transform);
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
            foreach (Transform transform in _queue)
            {
                this.OnDestroy(transform);
                GameObject.Destroy(transform);
            }

            _queue.Clear();
        }

        protected virtual void OnCreate(Transform transform)
        {
            transform.gameObject.SetActive(false);
        }

        protected virtual void OnDestroy(Transform transform)
        {
        }

        protected virtual void OnRent(Transform transform)
        {
            transform.gameObject.SetActive(true);
        }

        protected virtual void OnReturn(Transform transform)
        {
            transform.gameObject.SetActive(false);
            transform.SetParent(_container);
        }
    }
}