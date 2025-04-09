using UnityEngine;
using UnityEngine.Events;

namespace YusamCommon
{
    [DefaultExecutionOrder(-10000)]
    public partial class YuCoGameSettings : YuCoSingletonDontDestroyOnLoad<YuCoGameSettings>
    {
        [Space]
        
        [SerializeField] 
        private YuCoBaseDataStorage dataStorage;
        
        [Space]
        
        [SerializeField] 
        [YuCoInlineScriptableObject]
        public YuCoGameSettingsBlackboardBaseSo blackboardBaseSo;

        [Space]
        
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
}