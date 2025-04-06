using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace YusamCommon
{
    public static class YuCoVectorHelper
    {
        //[-179,180]
        public static float Angle180Signed(Vector3 a, Vector3 b, Vector3 normal)
        {
            // angle in [0,180]
            var angle = Vector3.Angle(a,b);
            var sign = Mathf.Sign(Vector3.Dot(normal,Vector3.Cross(a,b)));
            // angle in [-179,180]
            var signed_angle = angle * sign;
            // angle in [0,360] (not used but included here for completeness)
            //float angle360 =  (signed_angle + 180) % 360;
            return signed_angle;
        }
        
        //[0,360]
        public static float Angle360(Vector3 a, Vector3 b, Vector3 normal)
        {
            // angle in [0,180]
            var angle = Vector3.Angle(a,b);
            var sign = Mathf.Sign(Vector3.Dot(normal,Vector3.Cross(a,b)));
            // angle in [-179,180]
            var signed_angle = angle * sign;
            // angle in [0,360] (not used but included here for completeness)
            var angle360 =  (signed_angle + 180) % 360;
            return angle360;
        }
        
        public static Vector3 ConvertVectorUpwardsToVector3(YuCoVectorUpwardsEnum atCoVectorUpwardsEnum)
        {
            switch (atCoVectorUpwardsEnum)
            {
                case YuCoVectorUpwardsEnum.Up: return Vector3.up;
                case YuCoVectorUpwardsEnum.Down: return Vector3.down;
                case YuCoVectorUpwardsEnum.Forward: return Vector3.forward;
                case YuCoVectorUpwardsEnum.Back: return Vector3.back;
                case YuCoVectorUpwardsEnum.Right: return Vector3.right;
                case YuCoVectorUpwardsEnum.Left: return Vector3.left;              
            }
            
            return Vector3.zero;
        }
        
        public static Vector3 TruncateY(Vector3 value)
        {
            return new Vector3(value.x, 0, value.z);
        }
        
        public static Vector3 CreateY(float y)
        {
            return new Vector3(0, y, 0);
        }
        
        public static Vector3 TruncateXZ(Vector3 value)
        {
            return new Vector3(0, value.y, 0);
        }
        
        public static Vector3 CreateXZ(float x, float z)
        {
            return new Vector3(x, 0, z);
        }
        
        /*
         * Получаем рэндомную точну на окружности с радиусом
         */
        public static Vector2 GetRandomPointOnRadius(float radius)
        {
            float randAng = Random.Range(0, Mathf.PI * 2);
            return GetPointOnRadius(randAng, radius);
        }
        
        /*
         * Получаем рэндомную точну на окружности с радиусом
         */
        public static Vector2 GetPointOnRadius(float angle, float radius)
        {
            return new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
        }
        
        public static Vector3 GetPositionOnRadius(Vector3 centerPosition, float angle, float radius)
        {
            var vector2 = GetPointOnRadius(angle, radius);
            return new Vector3(centerPosition.x+vector2.x, centerPosition.y, centerPosition.z+vector2.y);
        }
        
        /*
         * Получаем рэндомную точну на окружности радиусом в пространстве XZ c постоянным Y 
         */
        public static Vector3 GetRandomPositionOnRadius(Vector3 centerPosition, float radius)
        {
            var vector2 = GetRandomPointOnRadius(radius);
            return new Vector3(centerPosition.x+vector2.x, centerPosition.y, centerPosition.z+vector2.y);
        }
        
        /*
         * Получаем рэндомную точну внутри окружности радиуса в пространстве XZ c постоянным Y
         */
        public static Vector3 GetRandomPositionInRadius(Vector3 centerPosition, float radius)
        {
            //Random.onUnitSphere;
            //Random.insideUnitCircle
            //Random.insideUnitSphere
            var endPoint = GetRandomPositionOnRadius(centerPosition, radius);
            var dir = centerPosition - endPoint;
            var distance = Vector3.Distance(centerPosition, endPoint);
            var newDistance = Random.Range(0, distance);
            return centerPosition + dir.normalized * newDistance;
        }
        
        
        public static Quaternion ToQ(Vector3 v)
        {
            return ToQ (v.y, v.x, v.z);
        }
        
        
        public static Quaternion ToQ (float yaw, float pitch, float roll)
        {
            yaw *= Mathf.Deg2Rad;
            pitch *= Mathf.Deg2Rad;
            roll *= Mathf.Deg2Rad;
            float rollOver2 = roll * 0.5f;
            float sinRollOver2 = (float)Math.Sin ((double)rollOver2);
            float cosRollOver2 = (float)Math.Cos ((double)rollOver2);
            float pitchOver2 = pitch * 0.5f;
            float sinPitchOver2 = (float)Math.Sin ((double)pitchOver2);
            float cosPitchOver2 = (float)Math.Cos ((double)pitchOver2);
            float yawOver2 = yaw * 0.5f;
            float sinYawOver2 = (float)Math.Sin ((double)yawOver2);
            float cosYawOver2 = (float)Math.Cos ((double)yawOver2);
            Quaternion result;
            result.w = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
            result.x = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
            result.y = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
            result.z = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;
            return result;
        }
        
        
        public static Vector3 FromQ2(Quaternion q1)
        {
            float sqw = q1.w * q1.w;
            float sqx = q1.x * q1.x;
            float sqy = q1.y * q1.y;
            float sqz = q1.z * q1.z;
            float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
            float test = q1.x * q1.w - q1.y * q1.z;
            Vector3 v;
            if (test>0.4995f*unit) { // singularity at north pole
                v.y = 2f * Mathf.Atan2 (q1.y, q1.x);
                v.x = Mathf.PI / 2;
                v.z = 0;
                return NormalizeAngles (v * Mathf.Rad2Deg);
            }
            if (test<-0.4995f*unit) { // singularity at south pole
                v.y = -2f * Mathf.Atan2 (q1.y, q1.x);
                v.x = -Mathf.PI / 2;
                v.z = 0;
                return NormalizeAngles (v * Mathf.Rad2Deg);
            }
            Quaternion q = new Quaternion (q1.w, q1.z, q1.x, q1.y);
            v.y = (float)Math.Atan2 (2f * q.x * q.w + 2f * q.y * q.z, 1 - 2f * (q.z * q.z + q.w * q.w));     // Yaw
            v.x = (float)Math.Asin (2f * (q.x * q.z - q.w * q.y));                             // Pitch
            v.z = (float)Math.Atan2 (2f * q.x * q.y + 2f * q.z * q.w, 1 - 2f * (q.y * q.y + q.z * q.z));      // Roll
            return NormalizeAngles (v * Mathf.Rad2Deg);
        }
        static Vector3 NormalizeAngles(Vector3 angles)
        {
            angles.x = NormalizeAngle (angles.x);
            angles.y = NormalizeAngle (angles.y);
            angles.z = NormalizeAngle (angles.z);
            return angles;
        }
        static float NormalizeAngle(float angle)
        {
            while (angle > 360)
                angle -= 360;
            while (angle < 0)
                angle += 360;
            return angle;
        }    
        
        public static Vector3 DirectionFromAngle(Transform transform, float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
        
        public static Vector3 FindCenterOfPoints(Vector3[] points)
        {
            var sum = points.Aggregate(Vector3.zero, (current, point) => current + point);

            return sum / points.Length;
        }
        
        public static Vector3 FindCenterOfPoints(Transform[] transforms)
        {
            var sum = transforms.Aggregate(Vector3.zero, (current, point) => current + point.position);

            return sum / transforms.Length;
        }
        
        public static Vector3 DirectionToAngleXZ(Transform transform, float angle)
        {
            return DirectionToAngleXZ(transform.position, angle);
        }
        
        public static Vector3 DirectionToAngleXZ(Vector3 point, float angle)
        {
            var radians = angle * Mathf.Deg2Rad;
            var x = point.x + 1 * Mathf.Cos(radians);  // x = r * cos(угол)
            var z = point.z + 1 * Mathf.Sin(radians);  // z = r * sin(угол)
            var targetPoint = new Vector3(x, point.y, z);
            return targetPoint - point;
        }
    }
}