using System.Collections.Generic;
using UnityEngine;

namespace YusamCommon
{
    
#if UNITY_EDITOR    
    public class YuCoWallEditorWindow : UnityEditor.EditorWindow
    {
        public float wallHeight = 2f;
        public float wallWidth = 0.2f;
        
        private readonly List<Vector3> points = new();
        private GameObject wallPrefab;
        private bool isPlacing;
        private Vector3 previewPoint;
        
        [UnityEditor.MenuItem("Tools/YuCo/Wall Builder")]
        public static void ShowWindow()
        {
            GetWindow<YuCoWallEditorWindow>("YuCo Wall Builder");
        }

        private void OnGUI()
        {
            GUILayout.Label("YuCo Wall Editor", UnityEditor.EditorStyles.boldLabel);

            wallPrefab = (GameObject) UnityEditor.EditorGUILayout.ObjectField("Wall Prefab", wallPrefab, typeof(GameObject), false);

            if (GUILayout.Button(isPlacing ? "Stop Placing" : "Start Placing"))
            {
                isPlacing = !isPlacing;
                UnityEditor.SceneView.RepaintAll();
            }

            if (GUILayout.Button("Clear Points"))
            {
                points.Clear();
            }

            if (GUILayout.Button("Build Walls") && wallPrefab)
            {
                BuildWalls();
            }
            
            GUILayout.Label("Settings", UnityEditor.EditorStyles.boldLabel);
            wallHeight = UnityEditor.EditorGUILayout.FloatField(UnityEditor.ObjectNames.NicifyVariableName(nameof(wallHeight)), wallHeight);
            wallWidth = UnityEditor.EditorGUILayout.FloatField(UnityEditor.ObjectNames.NicifyVariableName(nameof(wallWidth)), wallWidth);
        }

        void OnEnable()
        {
            UnityEditor.SceneView.duringSceneGui += OnSceneGUI;
        }

        private void OnDisable()
        {
            UnityEditor.SceneView.duringSceneGui -= OnSceneGUI;
        }

        private void OnSceneGUI(UnityEditor.SceneView sceneView)
        {
            Event e = Event.current;

            if (isPlacing)
            {
                Ray ray = UnityEditor.HandleUtility.GUIPointToWorldRay(e.mousePosition);
                Vector3 rawPoint = Vector3.zero;

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    rawPoint = hit.point;
                }
                else
                {
                    Plane plane = new Plane(Vector3.up, Vector3.zero);
                    if (plane.Raycast(ray, out float enter))
                    {
                        rawPoint = ray.GetPoint(enter);
                    }
                }

                // Привязываем к сетке
                previewPoint = SnapToUnityGrid(rawPoint);

                // Рисуем превью точки
                UnityEditor.Handles.color = Color.green;
                UnityEditor.Handles.SphereHandleCap(0, previewPoint, Quaternion.identity, 0.2f, EventType.Repaint);

                if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
                {
                    points.Add(previewPoint);
                    e.Use();
                }
            }

            // Рисуем все точки и линии между ними
            UnityEditor.Handles.color = Color.white;
            for (int i = 0; i < points.Count; i++)
            {
                UnityEditor.Handles.SphereHandleCap(0, points[i], Quaternion.identity, 0.2f, EventType.Repaint);

                if (i > 0)
                    UnityEditor.Handles.DrawLine(points[i - 1], points[i]);
            }

            sceneView.Repaint();
        }
        
        private Vector3 GetUnitySnapSettings()
        {
            return UnityEditor.EditorSnapSettings.gridSize;
        }
        
        private Vector3 SnapToUnityGrid(Vector3 position)
        {
            Vector3 snap = GetUnitySnapSettings();
            return new Vector3(
                Mathf.Round(position.x / snap.x) * snap.x,
                Mathf.Round(position.y / snap.y) * snap.y,
                Mathf.Round(position.z / snap.z) * snap.z
            );
        }

        private void BuildWalls()
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                Vector3 start = points[i];
                Vector3 end = points[i + 1];
                Vector3 midPoint = (start + end) / 2f;
                float length = Vector3.Distance(start, end);

                GameObject wall = (GameObject) UnityEditor.PrefabUtility.InstantiatePrefab(wallPrefab);
                wall.transform.position = midPoint;

                Vector3 dir = end - start;
                wall.transform.rotation = Quaternion.LookRotation(dir);
                wall.transform.localScale = new Vector3(wallWidth, wallHeight, length);
            }

            points.Clear();
        }
    }
#endif
}