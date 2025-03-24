namespace YusamCommon
{
    public partial class YuCoGameSettings
    {
        public YuCoGameSettingsBlackboardExampleSo BlackboardExample()
        {
            return (YuCoGameSettingsBlackboardExampleSo) blackboardBaseSo;
        }

        public void SceneLoadingForGameSettings(string sceneName)
        {
            if (sceneName.Contains(BlackboardExample().sceneLevelPrefix))
            {
                var idAsString = sceneName.Replace(BlackboardExample().sceneLevelPrefix, "");
                BlackboardExample().currentLevel = int.Parse(idAsString);
                SaveGameSettings();
            }
        }

    }
}