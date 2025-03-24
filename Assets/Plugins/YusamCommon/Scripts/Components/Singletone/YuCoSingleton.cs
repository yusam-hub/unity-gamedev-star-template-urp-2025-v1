using UnityEngine;

namespace YusamCommon
{
    public class YuCoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        private static bool isQuitting;

        public static T Instance
        {
            get
            {
                if (isQuitting)
                {
                    Debug.LogWarning($"[Singleton] Instance of {typeof(T)} is already destroyed.");
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
                    }
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                CreateOnce();
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void CreateOnce()
        {
            
        }

        private void OnApplicationQuit()
        {
            isQuitting = true;
        }
    }
}