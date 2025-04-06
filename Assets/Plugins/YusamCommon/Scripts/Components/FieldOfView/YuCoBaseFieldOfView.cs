using System.Buffers;
using System.Collections.Generic;
using UnityEngine;
using YusamCommon;

namespace YusamCommon
{
    public abstract class YuCoBaseFieldOfView : YuCoMonoBehaviour
    {
        [SerializeField] protected int findTargetBufferSize = 16;
        [SerializeField] protected YuCoCountDown findTargetCountDown = new(0.2f);
        [SerializeField] protected bool isGizmosEnabled;
        
        [SerializeField] protected LayerMask targetMask = default;
        [SerializeField] protected LayerMask obstacleMask = default;
        
        [SerializeField] [Range(0, 360)] protected float viewAngle = 90f;
        [SerializeField] protected float viewRadiusFar = 15f;
        
        [SerializeField] protected Material materialFar;
        [SerializeField] protected float meshPrecision = 60f;
        
        protected GameObject ObjectMeshRenderFar;
        protected MeshFilter MeshFilterFar;
        protected readonly List<Transform> FarTargets = new();

        public List<Transform> GetFarTargets()
        {
            return FarTargets;
        }

        public void SetViewAngle(float value)
        {
            viewAngle = value;
        }

        public float GetViewAngle()
        {
            return viewAngle;
        }
        
        public void SetViewRadiusFar(float value)
        {
            viewRadiusFar = value;
        }

        public float GetViewRadiusFar()
        {
            return viewRadiusFar;
        }
        
        protected void FindTargetsForFieldOfViewByCollider(
            Transform center, 
            float radius, 
            float angle, 
            int bufferSize, 
            List<Transform> targets
        ) 
        {
            targets.Clear();

            var arrayPool = ArrayPool<Collider>.Shared;
            var buffer = arrayPool.Rent(bufferSize);
            var count = Physics.OverlapSphereNonAlloc(center.position, radius, buffer, targetMask);
 
            for (var i = 0; i < count; i++)
            {
                var col = buffer[i];
			
                var target = col.transform;
       
                var dirToTarget = (target.position - center.position).normalized;

                if (!(Vector3.Angle(center.forward, dirToTarget) < angle / 2)) continue;
			
                var distanceToTarget = Vector3.Distance (center.position, target.position);
       
                if (!Physics.Raycast(center.position, dirToTarget, distanceToTarget, obstacleMask)) 
                {
                    targets.Add(target);
                }
            }

            arrayPool.Return(buffer);
        }
        
        protected void FindNearFarTargetsForFieldOfViewByCollider(
            Transform center, 
            float radiusNear, 
            float radiusFar, 
            float angle, 
            int bufferSize, 
            List<Transform> nearTargets,
            List<Transform> farTargets
        ) 
        {
            nearTargets.Clear();
            farTargets.Clear();

            var arrayPool = ArrayPool<Collider>.Shared;
            var buffer = arrayPool.Rent(bufferSize);
            var count = Physics.OverlapSphereNonAlloc(center.position, radiusFar, buffer, targetMask);
		
            for (var i = 0; i < count; i++)
            {
                var col = buffer[i];
			
                var target = col.transform;
			
                var dirToTarget = (target.position - center.position).normalized;

                if (!(Vector3.Angle(center.forward, dirToTarget) < angle / 2)) continue;
			
                var distanceToTarget = Vector3.Distance (center.position, target.position);
				
                if (!Physics.Raycast(center.position, dirToTarget, distanceToTarget, obstacleMask)) 
                {
                    if (distanceToTarget >= 0 && distanceToTarget <= radiusNear)
                    {
                        nearTargets.Add(target);  
                    }
                    else
                    {
                        farTargets.Add(target);  
                    }
                }
            }

            arrayPool.Return(buffer);
        }
    }
}