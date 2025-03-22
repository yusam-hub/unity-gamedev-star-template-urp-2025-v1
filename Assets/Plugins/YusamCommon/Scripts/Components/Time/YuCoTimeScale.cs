using UnityEngine;

namespace YusamCommon
{
    public class YuCoTimeScale : MonoBehaviour
    {
        [SerializeField] [Range(0, 1)] 
        private float timeScale = 1;

        private void Update()
        {
            Time.timeScale = timeScale;
        }
    }
}