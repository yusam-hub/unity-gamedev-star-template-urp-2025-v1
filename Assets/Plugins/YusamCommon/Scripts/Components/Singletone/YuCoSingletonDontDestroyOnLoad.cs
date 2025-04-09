using System;
using UnityEngine;
using UnityEngine.Events;

namespace YusamCommon
{
    public class YuCoSingletonDontDestroyOnLoad<T> : YuCoMonoBehaviour where T : YuCoMonoBehaviour
    {
        private static T instance;
        private static bool isQuitting;

        public static T Instance
        {
            get
            {
                if (isQuitting)
                {
                    Debug.LogWarning($"[YuCoSingletonDontDestroyOnLoad] Instance of {typeof(T)} is already destroyed.");
                    return null;
                }

                if (instance == null)
                {
                    instance = FindFirstObjectByType<T>();

                    if (instance == null)
                    {
                        /*
                         * Это нужно если компонента нет на сцене - мы его создаем
                         */
                        var singletonObject = new GameObject(typeof(T).Name);
                        instance = singletonObject.AddComponent<T>();
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return instance;
            }
        }

        private bool _startOnce;
        
        [Space]
        
        [SerializeField] 
        private UnityEvent onAwakeOnce;
        [SerializeField] 
        private UnityEvent onStartOnce;

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                gameObject.transform.SetParent(null);//DontDestroyOnLoad должен быть корневым компонентом
                DontDestroyOnLoad(gameObject);
                AwakeOnce();
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void AwakeOnce()
        {
            onAwakeOnce?.Invoke();
        }
        
        protected virtual void StartOnce()
        {
            onStartOnce?.Invoke();
        }

        protected virtual void Start()
        {
            if (!_startOnce)
            {
                _startOnce = true;
                StartOnce();
            }
        }
        
        private void OnApplicationQuit()
        {
            isQuitting = true;
        }
    }
}