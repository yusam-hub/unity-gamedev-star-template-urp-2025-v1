using UnityEngine;

namespace YusamCommon
{
    public abstract class YuCoBaseDataStorage : MonoBehaviour, IYuCoDataStorage
    {
        public abstract void SetParameter(string key, float value);
        public abstract void SetParameter(string key, int index, float value);
        public abstract void SetParameter(string key, int value);
        public abstract void SetParameter(string key, int index, int value);
        public abstract void SetParameter(string key, bool value);
        public abstract void SetParameter(string key, string id, bool value);
        public abstract void SetParameter(string key, int index, bool value);
        public abstract void SetParameter(string key, Vector3 value);
        public abstract void SetParameter(string key, string id, Vector3 value);
        
        public abstract float GetParameter(string key, float defaultValue);
        public abstract float GetParameter(string key, int index, float defaultValue);
        public abstract int GetParameter(string key, int defaultValue);
        public abstract int GetParameter(string key, int index, int defaultValue);
        public abstract bool GetParameter(string key, bool defaultValue);
        public abstract bool GetParameter(string key, int index, bool defaultValue);
        public abstract bool GetParameter(string key, string id, bool defaultValue);
        public abstract Vector3 GetParameter(string key, Vector3 defaultValue);
        public abstract Vector3 GetParameter(string key, string id, Vector3 defaultValue);
    }
}