using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ProjectFolderMenus
{
    public static class ProjectFolderMenuGame
    {
        private const string RootFolderName = "Assets";
        private const int MenuPriority = 0;
        
        private static readonly Dictionary<string, string[]> _subFolders = new()
        {
            {"3rdParty",new[]{"_Tmp"}},
            {"_Tmp",new[]{"3rdParty"}},
            {"Content", new[]{"_Tmp",  "Configs", "Prefabs", "Resources"}},
            {"Scenes",new[]{"_Tmp", "Levels"}},
            {"Scripts",new[]{"_Tmp"}},
        };
        
        private static readonly Dictionary<string, string[]> _rootFolders = new()
        {
            {"_Tmp", new[]{"_"}},
            {"3rdParty", new[]{"_"}},
            {"Game",new[]{"Content", "Scripts", "Scenes"}},
            {"Plugins",new[]{"_"}},
            {"Tools",new[]{"_"}},
            {"Settings",new[]{"_"}},
        };
        
        [MenuItem("Assets/Tools/Root Folders Init", false, MenuPriority)]
        public static void ProjectFolderMenuGameCreator(MenuCommand menuCommand)
        {
            foreach (var kvp in _rootFolders)
            {
                var currentRootFolderPath = RootFolderName + "/" + kvp.Key;
                if (!AssetDatabase.IsValidFolder(currentRootFolderPath))
                {
                    AssetDatabase.CreateFolder(RootFolderName, kvp.Key);
                    Debug.Log($"Root folder created: {currentRootFolderPath}");
                }
                
                if (kvp.Key.Equals("_Tmp"))
                {
                    ProjectFolderMenusHelper.CreateFileGitIgnore(kvp.Key);
                }
                
                foreach (var subFolderName in kvp.Value)
                {
                    if (subFolderName != "_" && !AssetDatabase.IsValidFolder($"{currentRootFolderPath}/{subFolderName}"))
                    {
                        AssetDatabase.CreateFolder(currentRootFolderPath, subFolderName);
                    }

                    if (kvp.Key.Equals("Game") && _subFolders.TryGetValue(subFolderName, out var subNames))
                    {
                        foreach (var subName in subNames)
                        {
                            var subPath = $"{currentRootFolderPath}/{subFolderName}/{subName}";
                            
                            if (subName != "_" && !AssetDatabase.IsValidFolder(subPath))
                            {
                                AssetDatabase.CreateFolder($"{currentRootFolderPath}/{subFolderName}", subName);
                                Debug.Log($"Sub folder created: {subPath}");
                            }
                            
                            if (subName.Equals("_Tmp"))
                            {
                                ProjectFolderMenusHelper.CreateFileGitIgnore(subPath);
                            }
                        }
                    }


                }
            }
            
            ProjectFolderMenusHelper.CreateFileWww();
        }
    }
}

