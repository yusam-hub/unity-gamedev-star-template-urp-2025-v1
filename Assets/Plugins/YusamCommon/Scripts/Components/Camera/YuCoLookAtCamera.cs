using UnityEngine;
using UnityEngine.Serialization;

namespace YusamCommon
{
    public class YuCoLookAtCamera : YuCoMonoBehaviour
    {
        [SerializeField] 
        private Camera mainCamera;

        [SerializeField] 
        private YuCoCameraModeEnum mode = YuCoCameraModeEnum.CameraForward;

        private void Awake()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }

        }

        private void LateUpdate()
        {
            if (!mainCamera) return;
            
            switch (mode)
            {
                case YuCoCameraModeEnum.LookAt:
                    transform.LookAt(mainCamera.transform);
                    break;
                case YuCoCameraModeEnum.LookAtInverted:
                    var pos = transform.position;
                    Vector3 dirFromCamera = pos - mainCamera.transform.position;
                    transform.LookAt(pos + dirFromCamera);
                    break;
                case YuCoCameraModeEnum.CameraForward:
                    transform.forward = mainCamera.transform.forward;
                    break;
                case YuCoCameraModeEnum.CameraForwardInverted:
                    transform.forward = -1 * mainCamera.transform.forward;
                    break;
            }

        }
    }
}