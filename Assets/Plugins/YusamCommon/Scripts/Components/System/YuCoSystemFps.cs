using UnityEngine;

namespace YusamCommon
{
    public class YuCoSystemFps : MonoBehaviour
    {
        [SerializeField]
        private float time = 1;

        [SerializeField]
        private float repeatRate = 1;

        [SerializeField] 
        public int targetFrameRate;
        
        [SerializeField]
        public int currentFps;
        
        private void Start()
        {
            ChangeFrameRate(targetFrameRate);
            
            InvokeRepeating(nameof(GetFps), time, repeatRate);
        }

        private int GetFpsByUnscaledDeltaTime()
        {
            return (int) (1f / Time.unscaledDeltaTime);
        }

        private void GetFps()
        {
            currentFps = GetFpsByUnscaledDeltaTime();
        }

        public void ChangeFrameRate(int value)
        {
            targetFrameRate = value;
            Application.targetFrameRate = targetFrameRate;
        }
    }
}