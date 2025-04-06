using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace YusamCommon
{
    public abstract class YuCoAbstractScene : YuCoMonoBehaviour
    {
        [SerializeField] 
        private float _waitBeforeProgressComplete = 1f;
        
        [SerializeField]
        private float _waitAfterProgressComplete = 1f;

        [SerializeField] 
        private bool _autoActivate = true;
        
        [SerializeField] 
        private bool _autoHideShowCursor = true;
        
        [SerializeField] 
        private bool _autoTimeScaleNormal = true;
        
        [SerializeField] 
        private UnityEvent<string> _onSceneStartLoading;
        [SerializeField] 
        private UnityEvent<float> _onSceneProgressLoading;
        [SerializeField] 
        private UnityEvent<string> _onSceneProgressLoadingAsString;
        [SerializeField] 
        private UnityEvent _onSceneFinishLoading;
        
        private float _loadingProgress;
        private AsyncOperation _asyncOperation;

        protected abstract string GetSceneName();
        
        public void YuCoSceneLoaderStartHandler()
        {
            if (_asyncOperation != null) return;
            
            var sceneName = GetSceneName();
            
            if (isDebugging)
            {
                Debug.Log($"The scene [ {sceneName} ] loading...");
            }
            
            if (_autoHideShowCursor)
            {
                YuCoApplicationHelper.HideCursor();
                if (isDebugging)
                {
                    Debug.Log($"Auto hide cursor");
                }
            }
            
            _onSceneStartLoading?.Invoke(sceneName);
            
            StartCoroutine(AsyncSceneLoader(sceneName));
        }
        
        public void YuCoSceneLoaderActivateHandler()
        {
            if (_asyncOperation != null)
            {
                _asyncOperation.allowSceneActivation = true;
            }
        }

        private IEnumerator AsyncSceneLoader(string sceneName)
        {
            _asyncOperation = SceneManager.LoadSceneAsync(sceneName);

            if (_asyncOperation != null)
            {
                _asyncOperation.allowSceneActivation = false;
   
                while (_asyncOperation.progress < 0.9f)
                {
                    _loadingProgress = Mathf.Clamp01(_asyncOperation.progress / 0.9f);

                    _onSceneProgressLoading?.Invoke(_loadingProgress);
                    _onSceneProgressLoadingAsString?.Invoke(Mathf.RoundToInt(_loadingProgress * 100) + " %");
                    if (isDebugging)
                    {
                        Debug.Log($" Progress [ {_loadingProgress} ]");
                    }
                    yield return true;
                }
            }

            if (_waitBeforeProgressComplete > 0)
            {
                if (isDebugging)
                {
                    Debug.Log($" Is wait before progress complete { _waitBeforeProgressComplete }");
                }

                yield return new WaitForSecondsRealtime(_waitBeforeProgressComplete);
            }

            _loadingProgress = 1;
            _onSceneProgressLoading?.Invoke(_loadingProgress);
            _onSceneProgressLoadingAsString?.Invoke(Mathf.RoundToInt(_loadingProgress * 100) + " %");
            if (isDebugging)
            {
                Debug.Log($" Progress [ {_loadingProgress} ]");
            }

            if (_waitAfterProgressComplete > 0)
            {
                if (isDebugging)
                {
                    Debug.Log($" Is waiting after progress complete { _waitAfterProgressComplete }");
                }
                yield return new WaitForSecondsRealtime(_waitAfterProgressComplete);
            }
            
            _onSceneFinishLoading?.Invoke();
            
            if (isDebugging)
            {
                Debug.Log($"The scene [ {sceneName} ] loaded");
            }
            
            if (_autoHideShowCursor)
            {
                YuCoApplicationHelper.ShowCursor();
                if (isDebugging)
                {
                    Debug.Log($"Auto show cursor");
                }
            }

            if (_autoTimeScaleNormal)
            {
                Time.timeScale = 1;
                if (isDebugging)
                {
                    Debug.Log($"Auto time scale = 1");
                }
            }

            if (_autoActivate)
            {
                if (isDebugging)
                {
                    Debug.Log($"The scene [ {sceneName} ] auto activate");
                }
                
                YuCoSceneLoaderActivateHandler();
            }
        }
    }
}