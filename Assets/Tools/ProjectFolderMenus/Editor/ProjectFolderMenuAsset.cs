using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ProjectFolderMenus
{
    public static class ProjectFolderMenuAsset
    {
        private const int MenuPriority = 0;
        
        private static readonly Dictionary<string, string[]> _folders = new()
        {
            {"Models", new[]{"_"}},
            {"Prefabs",new[]{"_"}},
            {"Materials",new[]{"_"}},
            {"Textures",new[]{"_"}},
            {"Shaders",new[]{"_"}},
            {"Sprites",new[]{"_"}},
            {"Fonts",new[]{"_"}},
            {"Animations",new[]{"_"}},
            {"Scenes",new[]{"_"}},
        };
        
        [MenuItem("Assets/Folder/Create Asset Folders in selected", false, MenuPriority)]
        public static void CreateAssetSelectedFolders(MenuCommand menuCommand)
        {
            var parentFolder = ProjectFolderMenusHelper.GetCurrentProjectDirectory();
            
            foreach (var kvp in _folders)
            {
                var currentRootFolderPath = parentFolder + "/" + kvp.Key;
                if (AssetDatabase.IsValidFolder(currentRootFolderPath)) continue;
                AssetDatabase.CreateFolder(parentFolder, kvp.Key);
                Debug.Log($"Folder created: {currentRootFolderPath}");
            }
        }
        

    }
}

