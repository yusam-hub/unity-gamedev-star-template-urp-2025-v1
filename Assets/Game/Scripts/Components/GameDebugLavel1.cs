using UnityEngine;
using YusamCommon;

namespace Game
{
    public class GameDebugLevel1 : YuCoSingleton<GameDebugLevel1>
    {
        protected override void AwakeOnce()
        {
            base.AwakeOnce();
            Debug.Log($"Loaded current level = " + YuCoGameSettings.Instance.BlackboardExample().currentLevel);
        }
    }
}