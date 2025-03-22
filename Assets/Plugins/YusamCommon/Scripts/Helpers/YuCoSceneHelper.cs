using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace YusamCommon
{
    public static class YuCoSceneHelper
    {
        public static List<string> GetListScenes()
        {
            var tmp = new List<string>();

            for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                var lastSlash = scenePath.LastIndexOf("/", StringComparison.Ordinal);
                var sceneName = scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".", StringComparison.Ordinal) - lastSlash - 1);
                tmp.Add(sceneName);
            }
            return tmp;
        }
        
        public static bool IsSceneExists(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            var tmp = GetListScenes();
            return tmp.Any(sceneName => string.Compare(name, sceneName, StringComparison.OrdinalIgnoreCase) == 0);

            /*for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                var lastSlash = scenePath.LastIndexOf("/");
                var sceneName = scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1);

                if (string.Compare(name, sceneName, true) == 0)
                    return true;
            }*/
        }
        
        public static int GetMaxScenes(string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                return 0;
            }

            int max = 0;
            
            var tmp = GetListScenes();
            foreach (var num in from sceneName in tmp 
                     where sceneName.Substring(0, prefix.Length).Equals(prefix) select sceneName.Substring(prefix.Length, sceneName.Length - prefix.Length))
            {
                if (int.TryParse(num, out var id))
                {
                    if ( id > max)
                    {
                        max = id;
                    }
                }
            }
            
            /*for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                var lastSlash = scenePath.LastIndexOf("/");
                var sceneName = scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1);

                if (sceneName.Substring(0, prefix.Length).Equals(prefix))
                {
                    string num = sceneName.Substring(prefix.Length, sceneName.Length - prefix.Length);
                    if (int.TryParse(num, out var id))
                    {
                        if ( id > max)
                        {
                            max = id;
                        }
                    }
                }
            }*/

            return max;
        }
    }
}