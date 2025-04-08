using System.Collections.Generic;
using UnityEngine;

namespace YusamCommon
{
    public class YuCoFieldOfViewNearFar : YuCoBaseFieldOfView
    {
        [SerializeField] private bool initOnAwake = true;
        [SerializeField] private float viewRadiusNear = 7.5f;
        [SerializeField] private Material materialNear;
        
        private GameObject _objectMeshRenderNear;
        private MeshFilter _meshFilterNear;
        private readonly List<Transform> _nearTargets = new();

        public void SetMaterialFar(Material value)
        {
            materialNear = value;
        }
        public void SetMaterialNear(Material value)
        {
            materialNear = value;
        }
        
        private void Awake()
        {
            ObjectMeshRenderFar = YuCoMeshHelper.CreateObjectWithMeshRenderComponents(
                "ObjectMeshRenderFar",
                transform, 
                materialFar,
                out MeshFilterFar);
            
            _objectMeshRenderNear = YuCoMeshHelper.CreateObjectWithMeshRenderComponents(
                "ObjectMeshRenderNear",
                transform, 
                materialNear,
                out _meshFilterNear);

            if (initOnAwake)
            {
                YuCoMeshHelper.InitMesh(_meshFilterNear, viewRadiusFar, viewAngle, false, meshPrecision);
                YuCoMeshHelper.InitMesh(MeshFilterFar, viewRadiusFar, viewAngle, true, meshPrecision);
            }
        }

        private void Start()
        {
            FindNearFarTargetsForFieldOfViewByCollider(
                transform,
                viewRadiusNear,
                viewRadiusFar,
                viewAngle,
                findTargetBufferSize,
                _nearTargets,
                FarTargets
            );
        }

        public List<Transform> GetNearTargets()
        {
            return _nearTargets;
        }
        
        private void Update()
        {
            findTargetCountDown.Tick(Time.fixedDeltaTime);
            if (findTargetCountDown.IsExpired())
            {
                FindNearFarTargetsForFieldOfViewByCollider(
                    transform,
                    viewRadiusNear,
                    viewRadiusFar,
                    viewAngle,
                    findTargetBufferSize,
                    _nearTargets,
                    FarTargets
                );
                findTargetCountDown.Reset();
            }
        }

        private void LateUpdate()
        {
            YuCoMeshHelper.SingleMeshLateUpdate(transform, _meshFilterNear, viewRadiusNear, viewAngle, obstacleMask, meshPrecision);
            YuCoMeshHelper.TwiceMeshLateUpdate(transform, MeshFilterFar, viewRadiusNear, viewRadiusFar - viewRadiusNear, viewAngle, obstacleMask, meshPrecision);
        }

        private void OnDrawGizmos()
        {

            if (!isGizmosEnabled) return;
            
            YuCoGizmosHelper.DrawHorizontalArcs(
                transform,
                transform,
                viewAngle/2,
                viewAngle/2,
                viewRadiusFar
            );

            YuCoGizmosHelper.DrawVerticalArcs(
                transform,
                transform,
                viewAngle/2,
                viewAngle/2,
                viewRadiusFar
            );
            
            YuCoGizmosHelper.DrawBorderArcs(transform, transform, viewAngle, viewRadiusNear, Color.black);
            YuCoGizmosHelper.DrawBorderArcs(transform, transform, viewAngle, viewRadiusFar, Color.black);
            
#if UNITY_EDITOR
            var saveColor = UnityEditor.Handles.color;
            foreach (var target in _nearTargets)
            {
                UnityEditor.Handles.color = Color.black;
                UnityEditor.Handles.DrawLine(transform.position, target.position);
            }
            foreach (var target in FarTargets)
            {
                UnityEditor.Handles.color = Color.blue;
                UnityEditor.Handles.DrawLine(transform.position, target.position);
            }

            UnityEditor.Handles.color = saveColor;
#endif
        }

    }
}