using UnityEngine;

namespace YusamCommon
{
    public class YuCoLayerChanger : MonoBehaviour
    {
        [SerializeField]
        [YuCoDropdown("GetLayerNames")]
        private int layerId;

        [SerializeField] 
        private GameObject[] gameObjects;
        
        private void Awake()
        {
            SetLayers();
        }

        private string[] GetLayerNames()
        {
            return YuCoApplicationHelper.GetLayerNames();
        }

        private void SetLayers()
        {
            foreach (var go in gameObjects)
            {
                go.layer = layerId;
            }  
        }

        public void SetLayerId(int value)
        {
            layerId = value;
            SetLayers();
        }

    }
}