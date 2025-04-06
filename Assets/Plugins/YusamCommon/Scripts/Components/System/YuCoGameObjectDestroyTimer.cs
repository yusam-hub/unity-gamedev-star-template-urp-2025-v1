using UnityEngine;

namespace YusamCommon
{
    public class YuCoGameObjectDestroyTimer : YuCoMonoBehaviour
    {
        private YuCoCountDown _countDown;

        public void SetTimer(float duration)
        {
            _countDown = new YuCoCountDown(duration);
        }

        private void Update()
        {
            if (_countDown == null) return;
            _countDown.Tick(Time.deltaTime);
            if (_countDown.IsExpired())
            {
                Destroy(gameObject);
            }
        }
    }
}