using UnityEngine;

namespace YusamCommon
{
    public partial class YuCoGameSettings : YuCoSingletonDontDestroyOnLoad<YuCoGameSettings>
    {
        [SerializeField] 
        private YuCoBaseDataStorage dataStorage;

        [SerializeField] 
        public YuCoGameSettingsBlackboardBaseSo blackboardBaseSo;

        protected override void CreateOnce()
        {
            blackboardBaseSo = Instantiate(blackboardBaseSo);
            
            LoadGameSettings();
        }

        public void LoadGameSettings()
        {
            blackboardBaseSo.LoadBlackboard(dataStorage);
        }

        public void SaveGameSettings()
        {
            blackboardBaseSo.SaveBlackboard(dataStorage);
        }
        
        public YuCoGameSettingsBlackboardBaseSo BlackboardBase()
        {
            return blackboardBaseSo;
        }
    }
    
#if UNITY_EDITOR    
    [UnityEditor.CustomEditor(typeof(YuCoGameSettings))]
    public class YuCoGameSettingsEditor : UnityEditor.Editor
    {
        private UnityEditor.SerializedObject serializedObjDefault;
        private UnityEditor.SerializedObject serializedDataObject;
        private bool showFields = false;
        
        private void OnEnable()
        {
            serializedObjDefault = new UnityEditor.SerializedObject(target);
        }

        public void DrawDefault()
        {
            serializedObjDefault.Update();

            UnityEditor.SerializedProperty property = serializedObjDefault.GetIterator();
            bool enterChildren = true;

            while (property.NextVisible(enterChildren))
            {
                UnityEditor.EditorGUILayout.PropertyField(property, true);
                enterChildren = false;
            }

            serializedObjDefault.ApplyModifiedProperties();
        }
        
        public void DrawBlackboard()
        {
            YuCoGameSettings holder = (YuCoGameSettings) target;
            
            if (holder.blackboardBaseSo == null)
            {
                return;
            }

            showFields = UnityEditor.EditorGUILayout.Foldout(showFields, "Blackboard So", true, UnityEditor.EditorStyles.foldoutHeader);
            
            if (showFields)
            {
                UnityEditor.EditorGUI.indentLevel++;
    
                serializedDataObject = new UnityEditor.SerializedObject(holder.blackboardBaseSo);
                serializedDataObject.Update();
                
                UnityEditor.SerializedProperty property = serializedDataObject.GetIterator();
                property.NextVisible(true);

                UnityEditor.EditorGUILayout.Space();
                while (property.NextVisible(false))
                {
                    UnityEditor.EditorGUILayout.PropertyField(property, true);
                }

                serializedDataObject.ApplyModifiedProperties();
            }
        }
        
        public override void OnInspectorGUI()
        {
            DrawDefault();
            DrawBlackboard();
        }
    }
#endif
}