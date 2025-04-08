using UnityEngine;

namespace YusamCommon
{
	public class YuCoFieldOfView : YuCoBaseFieldOfView
    {
        [SerializeField] private bool initOnAwake = true;
        public void SetMaterialFar(Material value)
        {
            materialFar = value;

            if (ObjectMeshRenderFar && ObjectMeshRenderFar.TryGetComponent(out MeshRenderer mr))
            {
                mr.sharedMaterial = materialFar;
            }
        }
        
        private void Awake()
        {
            ObjectMeshRenderFar = YuCoMeshHelper.CreateObjectWithMeshRenderComponents(
                "ObjectMeshRenderFar",
                transform, 
                materialFar,
                out MeshFilterFar);
           
            if (initOnAwake)
            {
                YuCoMeshHelper.InitMesh(MeshFilterFar, viewRadiusFar, viewAngle, false, meshPrecision);
            }
        }

        private void Start()
        {
           FindTargetsForFieldOfViewByCollider(
                transform,
                viewRadiusFar,
                viewAngle,
                findTargetBufferSize,
                FarTargets
            );
        }

        private void Update()
        {
            findTargetCountDown.Tick(Time.fixedDeltaTime);
            if (findTargetCountDown.IsExpired())
            {
                FindTargetsForFieldOfViewByCollider(
                    transform,
                    viewRadiusFar,
                    viewAngle,
                    findTargetBufferSize,
                    FarTargets
                );
                findTargetCountDown.Reset();
            }
        }

        private void LateUpdate()
        {
            YuCoMeshHelper.SingleMeshLateUpdate(transform, MeshFilterFar, viewRadiusFar, viewAngle, obstacleMask, meshPrecision);
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
            
            YuCoGizmosHelper.DrawBorderArcs(transform, transform, viewAngle, viewRadiusFar, Color.black);
            
#if UNITY_EDITOR           
            var saveColor = UnityEditor.Handles.color;
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