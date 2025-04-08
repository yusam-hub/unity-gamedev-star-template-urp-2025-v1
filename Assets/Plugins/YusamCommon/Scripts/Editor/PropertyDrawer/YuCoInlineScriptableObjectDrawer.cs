using UnityEngine;

namespace YusamCommon
{
#if UNITY_EDITOR    
    [UnityEditor.CustomPropertyDrawer(typeof(YuCoInlineScriptableObject))]
    public class YuCoInlineScriptableObjectDrawer : UnityEditor.PropertyDrawer
    {
        bool foldout;

        public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
        {
            UnityEditor.EditorGUI.BeginProperty(position, label, property);

            if (property.objectReferenceValue == null)
            {
                UnityEditor.EditorGUI.PropertyField(position, property, label);
                UnityEditor.EditorGUI.EndProperty();
                return;
            }

            ScriptableObject scriptableObject = (ScriptableObject)property.objectReferenceValue;
            UnityEditor.SerializedObject serializedObject = new UnityEditor.SerializedObject(scriptableObject);

            // Foldout
            Rect foldoutRect = new Rect(position.x, position.y, position.width, UnityEditor.EditorGUIUtility.singleLineHeight);
            foldout = UnityEditor.EditorGUI.Foldout(foldoutRect, foldout, label, true);

            // Object field (reference to SO)
            Rect objectFieldRect = new Rect(position.x + 15, position.y + UnityEditor.EditorGUIUtility.singleLineHeight, position.width - 15, UnityEditor.EditorGUIUtility.singleLineHeight);
            UnityEditor.EditorGUI.PropertyField(objectFieldRect, property, GUIContent.none);

            if (foldout)
            {
                serializedObject.Update();

                float yOffset = objectFieldRect.yMax + 4f;
                var prop = serializedObject.GetIterator();
                prop.NextVisible(true); // skip "m_Script"

                while (prop.NextVisible(false))
                {
                    float height = UnityEditor.EditorGUI.GetPropertyHeight(prop, true);
                    Rect fieldRect = new Rect(position.x + 15, yOffset, position.width - 15, height);
                    UnityEditor.EditorGUI.PropertyField(fieldRect, prop, true);
                    yOffset += height + 2f;
                }

                serializedObject.ApplyModifiedProperties();
            }

            UnityEditor.EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(UnityEditor.SerializedProperty property, GUIContent label)
        {
            float height = UnityEditor.EditorGUIUtility.singleLineHeight * 2f;

            if (foldout && property.objectReferenceValue != null)
            {
                UnityEditor.SerializedObject serializedObject = new UnityEditor.SerializedObject(property.objectReferenceValue);
                var prop = serializedObject.GetIterator();
                prop.NextVisible(true); // skip m_Script

                while (prop.NextVisible(false))
                {
                    height += UnityEditor.EditorGUI.GetPropertyHeight(prop, true) + 2f;
                }
            }

            return height;
        }
    }
#endif
}