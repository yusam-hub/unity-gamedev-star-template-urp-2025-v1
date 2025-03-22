#if UNITY_EDITOR

using System;
using UnityEditorInternal;
using UnityEngine;

namespace Tools.UnityEditor
{
    [CreateAssetMenu(
        fileName = nameof(ReferenceCheckFilter),
        menuName = "Tools/New" + nameof(ReferenceCheckFilter)
    )]
    public sealed class ReferenceCheckFilter : ScriptableObject
    {
        [SerializeField]
        public AssemblyDefinitionAsset[] definitions;

        public static ReferenceCheckFilter Instance => Resources.Load<ReferenceCheckFilter>(nameof(ReferenceCheckFilter));

        public bool TypeExists(Type type)
        {
            if (type == null)
            {
                return false;
            }

            var targetName = type.Assembly.GetName().Name;
            if (targetName == "Assembly-CSharp")
            {
                return true;
            }
            
            foreach (var asset in this.definitions)
            {
                if (targetName == asset.name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

#endif