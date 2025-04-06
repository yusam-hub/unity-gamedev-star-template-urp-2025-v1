using UnityEngine;

namespace YusamCommon
{
    public static class YuCoGizmosHelper
    {
        private static Color defaultColorHorizontal = new Color(1f, .5f, .5f, .1f);
        private static Color defaultColorVerticalUp = new Color(.5f, 1f, .5f, .1f);
        private static Color defaultColorVerticalDown = new Color(.5f, .5f, 1f, .1f);


        public static void DrawHorizontalArcs(
            Transform owner,
            Transform normal,
            float leftAngleLimit,
            float rightAngleLimit,
            float radius,
            Color color = default
            )
        {
#if UNITY_EDITOR
            UnityEditor.Handles.color = defaultColorHorizontal;
            if (color != default)
            {
                UnityEditor.Handles.color = color;
            }

            if (leftAngleLimit > 0 || rightAngleLimit > 0)
            {
                UnityEditor.Handles.DrawSolidArc(
                    normal.position, normal.up,
                    owner.forward, rightAngleLimit,
                    radius);
                UnityEditor.Handles.DrawSolidArc(
                    normal.position, normal.up,
                    owner.forward, -1 * Mathf.Abs(leftAngleLimit),
                    radius);
            }
            else
            {
                UnityEditor.Handles.DrawSolidArc(
                    normal.position, normal.up,
                    owner.forward, 360f,
                    radius);
            }
#endif
        }
        
        public static void DrawVerticalArcs(
            Transform owner,
            Transform normal,
            float upAngleLimit,
            float downAngleLimit,
            float radius,
            Color colorUp = default,
            Color colorDown = default
        )
        {
#if UNITY_EDITOR
            UnityEditor.Handles.color = defaultColorVerticalUp;
            if (colorUp != default)
            {
                UnityEditor.Handles.color = colorUp;
            }

            UnityEditor.Handles.DrawSolidArc(
                normal.position, normal.right,
                owner.forward, -1 * Mathf.Abs(upAngleLimit),
                radius);


            UnityEditor.Handles.color = defaultColorVerticalDown;
            if (colorDown != default)
            {
                UnityEditor.Handles.color = colorDown;
            }

            UnityEditor.Handles.DrawSolidArc(
                normal.position, normal.right,
                owner.forward, downAngleLimit,
                radius);
#endif
        }
        
        public static void DrawBorderArcs(
            Transform owner,
            Transform normal,
            float viewAngle,
            float viewRadius,
            Color color = default
        )
        {
#if UNITY_EDITOR
            UnityEditor.Handles.color = defaultColorHorizontal;
            if (color != default)
            {
                UnityEditor.Handles.color = color;
            }
            UnityEditor.Handles.DrawWireArc(owner.position, normal.up, normal.forward, viewAngle / 2, viewRadius);
            UnityEditor.Handles.DrawWireArc(owner.position, normal.up, normal.forward, -viewAngle / 2, viewRadius);
            var viewAngleA = YuCoVectorHelper.DirectionFromAngle(owner, -viewAngle / 2, false);
            var viewAngleB = YuCoVectorHelper.DirectionFromAngle(owner, viewAngle / 2, false);

            UnityEditor.Handles.DrawLine(owner.position, owner.position + viewAngleA * viewRadius);
            UnityEditor.Handles.DrawLine(owner.position, owner.position + viewAngleB * viewRadius);
#endif
        }
        
        public static void DrawWireSphere(
            Vector3 position,
            float radius,
            Color color
        )
        {
#if UNITY_EDITOR
            Gizmos.color = color;
            Gizmos.DrawWireSphere(position, radius);
#endif
        }

    }
}