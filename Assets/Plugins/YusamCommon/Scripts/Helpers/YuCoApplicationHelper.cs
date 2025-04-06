using System.Collections;
using UnityEngine;

namespace YusamCommon
{
    public static class YuCoApplicationHelper
    {
        public static void QuitBuildAndEditor()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
            
        }

        public static void OpenURL(string url)
        {
            Application.OpenURL(url);
        }
        
        public static void Pause()
        {
            Time.timeScale = 0;
        }
        
        public static void Resume()
        {
            Time.timeScale = 1;
        }
        
        public static void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        public static void ShowCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public static void CursorVisible(bool value)
        {
            if (value)
            {
                ShowCursor();
            }
            else
            {
                HideCursor(); 
            }
        }
        
        public static string[] GetLayerNames()
        {
#if UNITY_EDITOR
            var v = UnityEditorInternal.InternalEditorUtility.layers;
            for (var i = 0; i < v.Length; i++)
            {
                v[i] = $"{i}: {v[i]}";
            }
            return v;
#else
            return new string[] {};
#endif
        }

        public static int GetUnscaledFps()
        {
            return (int) (1f / Time.unscaledDeltaTime);
        }
    }
}