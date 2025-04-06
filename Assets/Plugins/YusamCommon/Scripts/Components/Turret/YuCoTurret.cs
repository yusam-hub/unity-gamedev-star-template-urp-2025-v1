using System;
using UnityEngine;

namespace YusamCommon
{
    [Serializable]
    public class YuCoTurret : YuCoMonoBehaviour
    {
        [SerializeField] 
        private bool isExternalUpdate = true;
        
        [SerializeField] 
        private Transform owner;
        
        [SerializeField] 
        private Transform turret;
        
        [SerializeField] 
        private Transform cannon;
        
        [SerializeField] 
        private float verticalSpeed = 30f;
        
        [SerializeField] 
        private float verticalUpLimit = 60f;
        
        [SerializeField] 
        private float verticalDownLimit = 5f;
        
        [SerializeField] 
        private float horizontalSpeed = 60f;
        
        [SerializeField] 
        private bool hasHorizontalLimited;
        
        [SerializeField] 
        [Range(0, 179)] 
        private float horizontalLeftLimit = 120f;
        
        [SerializeField] 
        [Range(0, 179)] 
        private float horizontalRightLimit = 120f;
        
        [SerializeField] 
        private bool _isIdle;
        
        [SerializeField] 
        private bool _isIdleTurretAroundRotate = true;
        
        [SerializeField] 
        private float targetThresholdAngle = 5f;
 
        [SerializeField] 
        private bool drawDebugRay;
        
        [SerializeField] 
        private bool drawDebugArcs;
        
        [SerializeField] 
        private float debugArcRadius = 10f;

        public Vector3 TargetDirectionFromTurretBase { get; private set; }
        
        public Vector3 TargetDirectionFromTurretCannon { get; private set; }
        
        private Vector3 _targetPosition = Vector3.zero;
        private float _horizontalLimitedAngle;
        private float _angleToTarget;
        private float _currentVerticalSpeed;
        private bool _isTargetAimed;
        private bool _isBaseAtRest;
        private bool _isCannonAtRest;

        public bool IsExternalUpdate
        {
            get => isExternalUpdate;
            set => isExternalUpdate = value;
        }

        public bool IsIdle
        {
            get => _isIdle;
            set => _isIdle = value;
        }
        public bool IsTargetAimed()
        {
            return _isTargetAimed;
        }

        public Vector3 GetTargetPosition()
        {
            return _targetPosition;
        }
        public void UpdateTargetPosition(Vector3 value)
        {
            _targetPosition = value;
        }

        private bool IsTurretAtRest()
        {
            return _isBaseAtRest;
        }
        private bool IsCannonAtRest()
        {
            return _isCannonAtRest;
        }

        public Transform GetOwner()
        {
            return owner;
        }
        
        public Transform GetTurret()
        {
            return turret;
        }
        
        public Transform GetCannon()
        {
            return cannon;
        }

        private void Update()
        {
            if (isExternalUpdate) return;
            
            ExternalUpdate(Time.deltaTime);
        }
        
        public void ExternalUpdate(float deltaTime)
        {
            TargetDirectionFromTurretBase = _targetPosition - turret.position;
            TargetDirectionFromTurretCannon = _targetPosition - cannon.position;
            
            if (_isIdle)
            {
                if (_isIdleTurretAroundRotate)
                {
                    turret.localEulerAngles += owner.up * (horizontalSpeed * deltaTime);
                    
                    if (!IsCannonAtRest())
                    {
                        RotateCannonToIdle(deltaTime);
                    }
                }
                else
                {
                    if (!IsTurretAtRest())
                    {
                        RotateTurretToIdle(deltaTime);
                    }

                    if (!IsCannonAtRest())
                    {
                        RotateCannonToIdle(deltaTime);
                    }
                }    
                _isTargetAimed = false;
            }
            else
            {
                RotateBaseToFaceTarget(deltaTime);

                if (cannon)
                {
                    RotateCannonToFaceTarget(deltaTime);
                }

                // Turret is considered "aimed" when it's pointed at the target.
                _angleToTarget = GetTurretAngleToTarget();

                // Turret is considered "aimed" when it's pointed at the target.
                _isTargetAimed = _angleToTarget < targetThresholdAngle;

                _isCannonAtRest = false;
                _isBaseAtRest = false;
            }
        }

        private float GetTurretAngleToTarget()
        {
            float angle = 999f;

            if (cannon)
            {
                angle = Vector3.Angle(TargetDirectionFromTurretCannon, cannon.forward);
            }
            else
            {
                Vector3 flattenedTarget = Vector3.ProjectOnPlane(
                    TargetDirectionFromTurretBase,
                    turret.up);

                angle = Vector3.Angle(
                    flattenedTarget - turret.position,
                    turret.forward);
            }

            return angle;
        }


        private void RotateTurretToIdle(float deltaTime)
        {
            if (hasHorizontalLimited)
            {
                _horizontalLimitedAngle = Mathf.MoveTowards(
                    _horizontalLimitedAngle, 0f,
                    horizontalSpeed * deltaTime);

                if (Mathf.Abs(_horizontalLimitedAngle) > Mathf.Epsilon)
                {
                    turret.localEulerAngles = Vector3.up * _horizontalLimitedAngle;
                }
                else
                {
                    _isBaseAtRest = true;
                }
            }
            else
            {
                turret.rotation = Quaternion.RotateTowards(
                    turret.rotation,
                    owner.rotation,
                    horizontalSpeed * deltaTime);

                _isBaseAtRest = Mathf.Abs(turret.localEulerAngles.y) < Mathf.Epsilon;
            }

        }
        private void RotateCannonToIdle(float deltaTime)
        {
            if (cannon)
            {
                _currentVerticalSpeed = Mathf.MoveTowards(_currentVerticalSpeed, 0f, verticalSpeed * deltaTime);
                if (Mathf.Abs(_currentVerticalSpeed) > Mathf.Epsilon)
                {
                    cannon.localEulerAngles = Vector3.right * -_currentVerticalSpeed;
                }
                else
                {
                    _isCannonAtRest = true;
                }
            }
            else
            {
                _isCannonAtRest = true;
            }
        }

        private void RotateCannonToFaceTarget(float deltaTime)
        {
            Vector3 localTargetPos = turret.InverseTransformDirection(TargetDirectionFromTurretCannon);
            Vector3 flattenedVecForCannon = Vector3.ProjectOnPlane(localTargetPos, Vector3.up);

            float targetElevation = Vector3.Angle(flattenedVecForCannon, localTargetPos);
            targetElevation *= Mathf.Sign(localTargetPos.y);

            targetElevation = Mathf.Clamp(targetElevation, -verticalDownLimit, verticalUpLimit);
            _currentVerticalSpeed = Mathf.MoveTowards(_currentVerticalSpeed, targetElevation, verticalSpeed * deltaTime);

            if (Mathf.Abs(_currentVerticalSpeed) > Mathf.Epsilon)
            {
                cannon.localEulerAngles = Vector3.right * -_currentVerticalSpeed;
            }
            
            if (drawDebugRay)
            {
                Debug.DrawRay(cannon.position, cannon.forward * localTargetPos.magnitude, Color.red);
            }
        }

        private void RotateBaseToFaceTarget(float deltaTime)
        {
            Vector3 turretUp = owner.up;

            Vector3 vecToTarget = TargetDirectionFromTurretBase;
            Vector3 flattenedVecForBase = Vector3.ProjectOnPlane(vecToTarget, turretUp);

            if (hasHorizontalLimited)
            {
                Vector3 turretForward = owner.forward;
                float targetSignedAngle = Vector3.SignedAngle(turretForward, flattenedVecForBase, turretUp);

                targetSignedAngle = Mathf.Clamp(targetSignedAngle, -horizontalLeftLimit, horizontalRightLimit);
                
                _horizontalLimitedAngle = Mathf.MoveTowards(
                    _horizontalLimitedAngle,
                    targetSignedAngle,
                    horizontalSpeed * deltaTime);

                if (Mathf.Abs(_horizontalLimitedAngle) > Mathf.Epsilon)
                {
                    turret.localEulerAngles = Vector3.up * _horizontalLimitedAngle;
                }
            }
            else
            {
                turret.rotation = Quaternion.RotateTowards(
                    Quaternion.LookRotation(turret.forward, turretUp),
                    Quaternion.LookRotation(flattenedVecForBase, turretUp),
                    horizontalSpeed * deltaTime);
            }
            
            if (drawDebugRay && !cannon)
            {
                Debug.DrawRay(turret.position,
                    turret.forward * flattenedVecForBase.magnitude,
                    Color.red);
            }
        }
        
        private void OnDrawGizmos()
        {
            if (!drawDebugArcs)
            {
                return;
            }

            if (!turret)
            {
                return;
            }

            YuCoGizmosHelper.DrawHorizontalArcs(
                owner,
                turret,
                hasHorizontalLimited ? horizontalLeftLimit : 180,
                hasHorizontalLimited ? horizontalRightLimit : 180,
                debugArcRadius
                );

            if (cannon == null)
            {
                return;
            }
            
            YuCoGizmosHelper.DrawVerticalArcs(
                turret,
                cannon,
                verticalUpLimit,
                verticalDownLimit,
                debugArcRadius
                );

        }

    }
}