using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace YusamCommon
{
#if UNITY_EDITOR    
    [UnityEditor.CustomPropertyDrawer(typeof(YuCoDropdown))]
    public class YuCoDropdownPropertyDrawer : UnityEditor.PropertyDrawer
    {
        public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
        {
            YuCoDropdown dropdown = (YuCoDropdown)attribute;
            object target = property.serializedObject.targetObject;

            // Найдём поле или метод с таким именем
            MemberInfo[] members = target.GetType()
                .GetMember(dropdown.valuesGetterName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (members.Length == 0)
            {
                UnityEditor.EditorGUI.LabelField(position, label.text, $"Missing: {dropdown.valuesGetterName}");
                return;
            }

            object valuesObj = null;

            // Получаем значения из поля или метода
            if (members[0] is FieldInfo field)
            {
                valuesObj = field.GetValue(target);
            }
            else if (members[0] is MethodInfo method)
            {
                valuesObj = method.Invoke(target, null);
            }

            if (valuesObj is IEnumerable enumerable)
            {
                var values = enumerable.Cast<object>().ToArray();

                if (values.Length == 0)
                {
                    UnityEditor.EditorGUI.LabelField(position, label.text, "No values");
                    return;
                }

                string[] displayOptions = values.Select(v => v?.ToString() ?? "null").ToArray();

                int index = Mathf.Clamp(property.intValue, 0, values.Length - 1);

                int newIndex = UnityEditor.EditorGUI.Popup(position, label.text, index, displayOptions);

                if (newIndex != index)
                {
                    property.intValue = newIndex;
                }
            }
            else
            {
                UnityEditor.EditorGUI.LabelField(position, label.text, "Not an IEnumerable");
            }
        }

        private object GetTargetValue(UnityEditor.SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case UnityEditor.SerializedPropertyType.Integer: return property.intValue;
                case UnityEditor.SerializedPropertyType.Float: return property.floatValue;
                case UnityEditor.SerializedPropertyType.String: return property.stringValue;
                case UnityEditor.SerializedPropertyType.Enum: return property.enumValueIndex;
                case UnityEditor.SerializedPropertyType.ObjectReference: return property.objectReferenceValue;
            }
            return null;
        }

        private void SetTargetValue(UnityEditor.SerializedProperty property, object value)
        {
            property.serializedObject.Update();

            switch (property.propertyType)
            {
                case UnityEditor.SerializedPropertyType.Integer:
                    if (value is int i) property.intValue = i;
                    break;
                case UnityEditor.SerializedPropertyType.Float:
                    if (value is float f) property.floatValue = f;
                    break;
                case UnityEditor.SerializedPropertyType.String:
                    if (value is string s) property.stringValue = s;
                    break;
                case UnityEditor.SerializedPropertyType.Enum:
                    if (value is int e) property.enumValueIndex = e;
                    break;
                case UnityEditor.SerializedPropertyType.ObjectReference:
                    if (value is UnityEngine.Object obj) property.objectReferenceValue = obj;
                    break;
            }

            property.serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}