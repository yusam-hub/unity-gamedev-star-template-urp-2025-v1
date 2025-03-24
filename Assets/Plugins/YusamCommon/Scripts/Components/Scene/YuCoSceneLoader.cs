using UnityEngine;
using UnityEngine.SceneManagement;

namespace YusamCommon
{
    public sealed class YuCoSceneLoader : YuCoAbstractScene
    {
        [SerializeField] 
        public string sceneName = "GameLevel_1";

        protected override string GetSceneName()
        {
            return sceneName;
        }

        public void YuCoSetSceneName(string value)
        {
            sceneName = value;
        }
    }
}