using UnityEngine;

namespace YusamCommon
{
    [DisallowMultipleComponent]
    public sealed class YuCoFlyCamera : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 500;
        [SerializeField] private float moveSpeed = 8;

        [SerializeField] private KeyCode upButton = KeyCode.W;
        [SerializeField] private KeyCode downButton = KeyCode.S;
        [SerializeField] private KeyCode leftButton = KeyCode.A;
        [SerializeField] private KeyCode rightButton = KeyCode.D;
               
        private float _rotateSpeedCurrent;
        private float _moveSpeedCurrent;
        
        public Vector3 GetDirection()
        {
            Vector3 dir = Vector3.zero;
            
            if (Input.GetKey(upButton))
            {
                dir.z = 1;
            } else if (Input.GetKey(downButton))
            {
                dir.z = -1;
            }
            
            if (Input.GetKey(leftButton))
            {
                dir.x = -1;  
            } else if (Input.GetKey(rightButton))
            {
                dir.x = 1;  
            }

            return dir;
        }

        private bool IsButtonPressed()
        {
            return Input.GetMouseButton((int) YuCoMouseButtonEnum.Secondary);
        }

        private void LateUpdate()
        {
            var deltaTime = Time.unscaledDeltaTime;
            if (IsButtonPressed())
            {
                YuCoApplicationHelper.HideCursor();
                UpdateMovement(deltaTime);
                UpdateRotation(deltaTime);
            }
            else
            {
                YuCoApplicationHelper.ShowCursor();
            }  
        }
        
        private void UpdateMovement(float deltaTime)
        {
            transform.Translate(GetDirection() * moveSpeed * deltaTime);
        }

        private void UpdateRotation(float deltaTime)
        {
            var mouseX = Input.GetAxis("Mouse X");
            var mouseY = Input.GetAxis("Mouse Y");
            var input = new Vector3(-mouseY, mouseX, 0);
            input *= rotateSpeed;
            input *= deltaTime;
            transform.Rotate( input);
            var eulerAngles = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, 0);
        }
    }
}