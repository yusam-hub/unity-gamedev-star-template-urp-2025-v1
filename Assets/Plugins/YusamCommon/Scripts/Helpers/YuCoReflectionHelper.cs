using System;
using System.Reflection;
using UnityEngine;

namespace YusamCommon
{
    public class YuCoReflectionHelper
    {
        public static void PrintSerializedFields<T>(T obj)
        {
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                if (field.IsPublic || Attribute.IsDefined(field, typeof(SerializeField)))
                {
                    Debug.Log($"FieldName: {field.Name}, FieldType: {field.FieldType}");
                }
            }
        }
    }
}