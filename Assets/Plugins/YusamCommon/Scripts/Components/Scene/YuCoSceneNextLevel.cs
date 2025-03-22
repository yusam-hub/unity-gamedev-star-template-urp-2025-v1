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

        public override string GetSceneName()
        {
            return IsNextSceneExists(out var newName) ? newName : sceneReturnToMainMenu;
        }
        
        public string GetNextSceneName()
        {
            var idAsString = GetCurrentLevelId();
            if (!int.TryParse(idAsString, out var id))
            {
                return SceneManager.GetActiveScene().name;
            }
            id++;
            var newName = scenePrefix + id.ToString();
            return newName;
        }   

        public bool IsNextSceneExists(out string sceneName)
        {
            var newName = GetNextSceneName();

            sceneName = newName;

            return YuCoSceneHelper.IsSceneExists(newName);
        }
        
        public string GetCurrentLevelId()
        {
            string currentName = SceneManager.GetActiveScene().name;
            return currentName.Replace(scenePrefix, "");
        }
        
        public string GetScenePrefix()
        {
            return scenePrefix;
        }
    }
}