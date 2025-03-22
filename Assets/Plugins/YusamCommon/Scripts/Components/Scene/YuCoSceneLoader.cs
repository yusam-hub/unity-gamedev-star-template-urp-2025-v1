using UnityEngine;

namespace YusamCommon
{
    public sealed class YuCoSceneLoader : YuCoAbstractScene
    {
        [SerializeField] 
        private string sceneName = "GameLevel_1";

        public override string GetSceneName()
        {
            return sceneName;
        }
    }
}