using UnityEngine;

namespace YusamCommon
{
    [CreateAssetMenu(
        fileName = "YuCoGameSettingsBlackboardExampleSo",
        menuName = "YusamCommon/Game/Settings/YuCoGameSettingsBlackboardExampleSo"
    )]
    public class YuCoGameSettingsBlackboardExampleSo : YuCoGameSettingsBlackboardBaseSo
    {
        public string sceneLevelPrefix = "GameLevel_";
        public int currentLevel;

        public override void LoadBlackboard(IYuCoDataStorage dataStorage)
        {
            currentLevel = dataStorage.GetParameter(nameof(currentLevel), currentLevel);
        }

        public override void SaveBlackboard(IYuCoDataStorage dataStorage)
        {
            dataStorage.SetParameter(nameof(currentLevel), currentLevel);
        }
    }
}