using UnityEngine;

namespace YusamCommon
{
    /*[CreateAssetMenu(
        fileName = nameof(YuCoScriptableObject),
        menuName = "YusamCommon/New " + nameof(YuCoScriptableObject)
    )]*/
    public abstract class YuCoScriptableObject : ScriptableObject
    {
        public bool isDebugging;
    }
}