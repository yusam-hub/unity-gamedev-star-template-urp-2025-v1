using UnityEngine;

namespace YusamCommon
{
    public abstract class YuCoGameSettingsBlackboardBaseSo : ScriptableObject
    {
        public abstract void LoadBlackboard(IYuCoDataStorage dataStorage);

        public abstract void SaveBlackboard(IYuCoDataStorage dataStorage);
    }
}