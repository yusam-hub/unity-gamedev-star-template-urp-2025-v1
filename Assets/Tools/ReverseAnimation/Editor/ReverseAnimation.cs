#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Tools.ReverseAnimation.Editor
{
    public class ReverseAnimation : global::UnityEditor.Editor
    {
        public static AnimationClip GetSelectedClip()
        {
            var clips = Selection.GetFiltered(typeof(AnimationClip),SelectionMode.Assets);
            if (clips.Length > 0)
            {
                return clips[0] as AnimationClip;
            }
            return null;
        }

        [MenuItem("Tools/AnimationClip/Reverse Selected")]
        public static void Reverse()
        {
            var clip = GetSelectedClip();
            
            if (clip == null)
            {
                Debug.Log("No one clip selected!");
                return;
            }
            var clipLength = clip.length;

            var curves = new List<AnimationCurve>();
            EditorCurveBinding[] editorCurveBindings = AnimationUtility.GetCurveBindings(clip);
            foreach (EditorCurveBinding i in editorCurveBindings)
            {
                var curve = AnimationUtility.GetEditorCurve(clip, i);
                curves.Add(curve);
            }

            clip.ClearCurves();
            for (var i = 0; i < curves.Count; i++)
            {
                var curve = curves[i];
                var binding = editorCurveBindings[i];

                var keys = curve.keys;
                var keyCount = keys.Length;
                (curve.postWrapMode, curve.preWrapMode) = (curve.preWrapMode, curve.postWrapMode);
                for (var j = 0; j < keyCount; j++ )
                {
                    var k = keys[j];
                    k.time = clipLength - k.time;
                    var tmp = -k.inTangent;
                    k.inTangent = -k.outTangent;
                    k.outTangent = tmp;
                    keys[j] = k;
                }
                curve.keys = keys;
                clip.SetCurve(binding.path, binding.type, binding.propertyName, curve);
            }

            var events = AnimationUtility.GetAnimationEvents(clip);
            if (events.Length > 0)
            {
                foreach (var t in events)
                {
                    t.time = clipLength - t.time;
                }

                AnimationUtility.SetAnimationEvents(clip,events);
            }
            Debug.Log("Animation reversed!");
        }
    }
}


#endif