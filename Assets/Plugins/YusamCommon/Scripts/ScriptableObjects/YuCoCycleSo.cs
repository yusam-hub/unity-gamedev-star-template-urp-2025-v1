using UnityEngine;

namespace YusamCommon
{
    public abstract class YuCoCycleSo : YuCoScriptableObject
    {
        public virtual void OnUpdate(in GameObject gameObject, in float deltaTime)
        {
       
        }

        public virtual void OnLateUpdate(in GameObject gameObject, in float deltaTime)
        {
         
        }

        public virtual void OnFixedUpdate(in GameObject gameObject, in float deltaTime)
        {
   
        }

        public virtual void Init(in GameObject gameObject)
        {

        }

        public virtual void Enable(in GameObject gameObject)
        {

        }

        public virtual void Disable(in GameObject gameObject)
        {

        }

        public virtual void Dispose(in GameObject gameObject)
        {

        }
    }
}