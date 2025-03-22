using UnityEngine.SceneManagement;

namespace YusamCommon
{
    public sealed class YuCoSceneRestart : YuCoAbstractScene
    {
        public override string GetSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }
    }
}