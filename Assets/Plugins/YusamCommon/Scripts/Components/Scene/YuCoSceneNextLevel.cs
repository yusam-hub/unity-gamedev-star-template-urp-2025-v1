using UnityEngine;
using UnityEngine.SceneManagement;
namespace YusamCommon
{
    public sealed class YuCoSceneNextLevel : YuCoAbstractScene
    {
        [SerializeField] 
        private string scenePrefix = "GameLevel_";
        
        [SerializeField] 
        private string sceneReturnToMainMenu = "GameLauncher";

        protected override string GetSceneName()
        {
            return IsNextSceneExists(out var newName) ? newName : sceneReturnToMainMenu;
        }
        
        private string GetNextSceneName()
        {
            var idAsString = GetCurrentSceneId();
            if (!int.TryParse(idAsString, out var id))
            {
                return SceneManager.GetActiveScene().name;
            }
            id++;
            var newName = GetScenePrefix() + id.ToString();
            return newName;
        }   

        private bool IsNextSceneExists(out string sceneName)
        {
            var newName = GetNextSceneName();

            sceneName = newName;

            return YuCoSceneHelper.IsSceneExists(newName);
        }
        
        private string GetCurrentSceneId()
        {
            var currentName = SceneManager.GetActiveScene().name;
            return currentName.Replace(GetScenePrefix(), "");
        }
        
        private string GetScenePrefix()
        {
            return scenePrefix;
        }
    }
}