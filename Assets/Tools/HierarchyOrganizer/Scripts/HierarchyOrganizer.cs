#if UNITY_EDITOR
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YusamCommon;

namespace Tools.UnityEditor
{
    internal static class HierarchyOrganizer
    {
        [MenuItem("GameObject/Tools/Hierarchy Organize Create", false, 0)]
        private static void HierarchyOrganizeCreate(MenuCommand menuCommand)
        {
            var targetObject = Selection.activeGameObject;
            if (targetObject != null) return;

            var rootObjects = new List<GameObject>();

            var activeScene = SceneManager.GetActiveScene();
            activeScene.GetRootGameObjects(rootObjects);

            var systemGo = new GameObject("[ System ]");
            
            var systemCamera = new GameObject("< Camera >");
            systemCamera.transform.SetParent(systemGo.transform);
            
            var systemLight = new GameObject("< Light >");
            systemLight.transform.SetParent(systemGo.transform);
            
            var systemCanvas = new GameObject("< Canvas >");
            systemCanvas.transform.SetParent(systemGo.transform);
            CreateCanvas(systemCanvas.transform);
            
            foreach (GameObject go in rootObjects)
            {
                if (go.TryGetComponent<Camera>(out var camera))
                {
                    go.transform.SetParent(systemCamera.transform);
                    if (camera.CompareTag("MainCamera"))
                    {
                        go.transform.position = new Vector3(0, 5, -5);
                        go.transform.localRotation = Quaternion.Euler(45,0,0);
                        if (!camera.TryGetComponent<YuCoFlyCamera>(out var flyCamera))
                        {
                            camera.AddComponent<YuCoFlyCamera>();
                        }
                    }
                }
                if (go.TryGetComponent<Light>(out var light))
                {
                    go.transform.SetParent(systemLight.transform);
                }
            }
            
            var worldGo = new GameObject("[ World ]");

            CreateGroundPlane(worldGo.transform);
            CreateCube(worldGo.transform);

            EditorSceneManager.MarkSceneDirty(activeScene);
        }

        private static void CreateGroundPlane(Transform parent)
        {
            var primitive = GameObject.CreatePrimitive(PrimitiveType.Plane);
            primitive.transform.SetParent(parent);
            primitive.transform.localScale = new Vector3(10, 1, 10);
            var mat = Resources.Load<Material>("Materials/YuCo_Prototype_Scaling_Gray");
            if (mat)
            {
                primitive.GetComponent<MeshRenderer>().sharedMaterial = mat;
            }
            primitive.name = "Ground Plane";
        }
        
        private static void CreateCube(Transform parent)
        {
            var primitive = GameObject.CreatePrimitive(PrimitiveType.Cube);
            primitive.transform.SetParent(parent);
            primitive.transform.position = new Vector3(0, 0.5f, 0);
            primitive.transform.localScale = new Vector3(1, 1, 1);
            var mat = Resources.Load<Material>("Materials/YuCo_Prototype_Scaling_Orange");
            if (mat)
            {
                primitive.GetComponent<MeshRenderer>().sharedMaterial = mat;
            }
            primitive.name = "Cube Example";
        }
        
        private static void CreateCanvas(Transform parent)
        {
            var canvasGo = new GameObject("Canvas Default");
            canvasGo.transform.SetParent(parent);
            var canvas = canvasGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            var canvasScaler = canvasGo.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            canvasGo.AddComponent<GraphicRaycaster>();
            canvasGo.SetActive(false);

            var eventSystemGo = new GameObject("Event System");
            eventSystemGo.transform.SetParent(parent);
            eventSystemGo.AddComponent<EventSystem>();
            eventSystemGo.AddComponent<StandaloneInputModule>();
        }
   }
}
#endif