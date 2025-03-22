using System.IO;
using UnityEditor;
using UnityEngine;

namespace ProjectFolderMenus
{
    public static class ProjectFolderMenusHelper
    {
        private const string FileGitIgnore = ".gitignore";
        private const string FileWww = "Www.cs";
        
        public static string GetCurrentProjectDirectory()
        {
            foreach (var obj in Selection.GetFiltered<Object>(SelectionMode.Assets))
            {
                var path = AssetDatabase.GetAssetPath(obj);
                
                if (string.IsNullOrEmpty(path))
                    continue;

                if (System.IO.Directory.Exists(path))
                    return path;
                
                if (System.IO.File.Exists(path))
                    return System.IO.Path.GetDirectoryName(path);
            }

            return "Assets";
        }
        
        public static void CreateFileGitIgnore(string path)
        {
            var filePath = Path.Combine(path, FileGitIgnore);
            if (File.Exists(filePath)) return;
            
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory) && directory != null)
            {
                Directory.CreateDirectory(directory);
            }
            
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("*");
                writer.WriteLine(".gitignore");
            }
            
            Debug.Log("File created and data written at: " + filePath);
        }
        
        public static void CreateFileWww()
        {
            var filePath = Path.Combine(Application.dataPath, FileWww);
            if (File.Exists(filePath)) return;
            
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory) && directory != null)
            {
                Directory.CreateDirectory(directory);
            }
            
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("public static class Www");
                writer.WriteLine("{");
                writer.WriteLine("//Пустышка в корне проекта, нужна программисту для открытия проекта в IDE");
                writer.WriteLine("}");
            }

            Debug.Log("File created and data written at: " + filePath);
        }
    }
}