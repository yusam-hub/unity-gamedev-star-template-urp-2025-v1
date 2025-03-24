using UnityEngine.SceneManagement;

namespace YusamCommon
{
    public sealed class YuCoSceneRestart : YuCoAbstractScene
    {
        protected override string GetSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }
    }
}