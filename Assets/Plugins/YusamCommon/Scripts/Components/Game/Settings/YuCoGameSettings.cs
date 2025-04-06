using UnityEngine;
using UnityEngine.Events;

namespace YusamCommon
{
    [DefaultExecutionOrder(-10000)]
    public partial class YuCoGameSettings : YuCoSingletonDontDestroyOnLoad<YuCoGameSettings>
    {
        [SerializeField] 
        private YuCoBaseDataStorage dataStorage;

        [SerializeField] 
        public YuCoGameSettingsBlackboardBaseSo blackboardBaseSo;

        [SerializeField] 
        private UnityEvent onLoadedBlackboard;
        
        [SerializeField] 
        private UnityEvent onBeforeSaveBlackboard;
        
        [SerializeField] 
        private UnityEvent onAfterSaveBlackboard;
        
        protected override void AwakeOnce()
        {
            base.AwakeOnce();
            blackboardBaseSo = Instantiate(blackboardBaseSo);
            LoadGameSettings();
        }

        private void LoadGameSettings()
        {
            blackboardBaseSo.LoadBlackboard(dataStorage);
            onLoadedBlackboard?.Invoke();
        }

        public void SaveGameSettings()
        {
            onBeforeSaveBlackboard?.Invoke();
            blackboardBaseSo.SaveBlackboard(dataStorage);
            onAfterSaveBlackboard?.Invoke();
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

        private void DrawDefault()
        {
            serializedObjDefault.Update();

            var property = serializedObjDefault.GetIterator();
            var enterChildren = true;

            while (property.NextVisible(enterChildren))
            {
              
                if (property.name.Equals("m_Script"))
                {
                    
                }
                else
                {
                    UnityEditor.EditorGUILayout.PropertyField(property, true);
                }

                enterChildren = false;
            }

            serializedObjDefault.ApplyModifiedProperties();
        }

        private void DrawBlackboard()
        {
            var holder = (YuCoGameSettings) target;
            
            if (!holder.blackboardBaseSo)
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