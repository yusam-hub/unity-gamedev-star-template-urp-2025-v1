using UnityEngine;
using YusamCommon;

namespace Game
{
    public class GameDebugLevel2 : YuCoSingleton<GameDebugLevel2>
    {
        protected override void AwakeOnce()
        {
            base.AwakeOnce();
            Debug.Log($"Loaded current level = " + YuCoGameSettings.Instance.BlackboardExample().currentLevel);
        }
    }
}