using System.Collections.Generic;
using UnityEngine;

namespace YusamCommon
{
    public static class YuCoMeshHelper
    {
        public static GameObject CreateObjectWithMeshRenderComponents(
            string objectName,
            Transform transform, 
            Material material,
            out MeshFilter mesh
            )
        {
            var newObj = new GameObject(objectName)
            {
                transform =
                {
                    position = transform.position,
                    rotation = transform.rotation
                }
            };

            newObj.transform.SetParent(transform);
            

            var render = newObj.AddComponent<MeshRenderer>();
            mesh = newObj.AddComponent<MeshFilter>();
            
            render.sharedMaterial = material;
            render.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            render.receiveShadows = false;
            render.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
            render.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
            render.allowOcclusionWhenDynamic = false;
            render.sortingOrder = 1;
            
            return newObj;
        }
                
        public static void InitMesh(
            MeshFilter mesh,
            float viewRange, 
            float viewAngle, 
            bool twice,
            float precision = 60f
            )
        {
            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            var normals = new List<Vector3>();
            var uv = new List<Vector2>();

            if (!twice)
            {
                vertices.Add(new Vector3(0f, 0f, 0f));
                normals.Add(Vector3.up);
                uv.Add(Vector2.zero);
            }

            var minMaxAngle = Mathf.RoundToInt(viewAngle / 2f);

            var triIndex = 0;
            var stepJump = Mathf.Clamp(viewAngle / precision, 0.01f, minMaxAngle);

            for (float i = -minMaxAngle; i <= minMaxAngle; i += stepJump)
            {
                var angle = (float)(i + 90f) * Mathf.Deg2Rad;
                var dir = new Vector3(Mathf.Cos(angle) * viewRange, 0f, Mathf.Sin(angle) * viewRange);

                vertices.Add(dir);
                normals.Add(Vector2.up);
                uv.Add(Vector2.zero);

                if (twice)
                {
                    vertices.Add(dir);
                    normals.Add(Vector2.up);
                    uv.Add(Vector2.zero);
                }

                if (triIndex > 0)
                {
                    if (twice)
                    {
                        triangles.Add(triIndex);
                        triangles.Add(triIndex+1);
                        triangles.Add(triIndex-2);

                        triangles.Add(triIndex - 2);
                        triangles.Add(triIndex + 1);
                        triangles.Add(triIndex - 1);
                    }
                    else
                    {
                        triangles.Add(0);
                        triangles.Add(triIndex + 1);
                        triangles.Add(triIndex);
                    }
                }
                triIndex += twice ? 2 : 1;
            }

            mesh.mesh.vertices = vertices.ToArray();
            mesh.mesh.triangles = triangles.ToArray();
            mesh.mesh.normals = normals.ToArray();
            mesh.mesh.uv = uv.ToArray();
        }
                
        public static void SingleMeshLateUpdate(
            Transform transform,
            MeshFilter mesh,
            float viewRange, 
            float viewAngle, 
            LayerMask obstacleMask,
            float precision = 60f,
            bool isDebugging = false
            )
        {
            var vertices = new List<Vector3> { new Vector3(0f, 0f, 0f) };

            var minmax = Mathf.RoundToInt(viewAngle / 2f);
            var stepJump = Mathf.Clamp(viewAngle / precision, 0.01f, minmax);
            for (float i = -minmax; i <= minmax; i += stepJump)
            {
                var angle = (float)(i + 90f) * Mathf.Deg2Rad;
                var dir = new Vector3(Mathf.Cos(angle) * viewRange, 0f, Mathf.Sin(angle) * viewRange);

                var posWorld = transform.TransformPoint(Vector3.zero);
                var dirWorld = transform.TransformDirection(dir.normalized);
                
                var ishit = Physics.Raycast(new Ray(posWorld, dirWorld), out var hit, viewRange, obstacleMask.value);
                
                if (ishit)
                {
                    dir = dir.normalized * hit.distance;
                }
                
                if (isDebugging)
                {
                    Debug.DrawRay(posWorld, dirWorld * (ishit ? hit.distance : viewRange));
                }

                vertices.Add(dir);
            }

            mesh.mesh.vertices = vertices.ToArray();
            mesh.mesh.RecalculateBounds();
        }
        
        public static void TwiceMeshLateUpdate(
            Transform transform,
            MeshFilter mesh,
            float offset,
            float viewRange, 
            float viewAngle, 
            LayerMask obstacleMask,
            float precision = 60f,
            bool isDebugging = false
            )
        {
            var vertices = new List<Vector3>();

            var minmax = Mathf.RoundToInt(viewAngle / 2f);
            var stepJump = Mathf.Clamp(viewAngle / precision, 0.01f, minmax);
            for (float i = -minmax; i <= minmax; i += stepJump)
            {
                var angle = (float)(i + 90f) * Mathf.Deg2Rad;
                var dir = new Vector3(Mathf.Cos(angle) * offset, 0f, Mathf.Sin(angle) * offset);

                var posWorld = transform.TransformPoint(Vector3.zero);
                var dirWorld = transform.TransformDirection(dir.normalized);
                var ishit = Physics.Raycast(new Ray(posWorld, dirWorld), out var hit, viewRange + offset, obstacleMask.value);

                var totDist = ishit ? hit.distance : viewRange + offset;
                var dir1 = dir.normalized * offset;
                var dir2 = dir.normalized * Mathf.Max(totDist, offset);

                if (isDebugging)
                {
                    Debug.DrawRay(posWorld + dirWorld * offset, dirWorld * Mathf.Max(totDist - offset, 0f), Color.blue);
                }

                vertices.Add(dir1);
                vertices.Add(dir2);
            }

            mesh.mesh.vertices = vertices.ToArray();
            mesh.mesh.RecalculateBounds();
        }
    }
}