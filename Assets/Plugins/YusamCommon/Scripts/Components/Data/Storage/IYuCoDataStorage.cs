using UnityEngine;

namespace YusamCommon
{
    public interface IYuCoDataStorage
    {
        public void SetParameter(string key, float value);
        public void SetParameter(string key, int index, float value);
        public void SetParameter(string key, int value);
        public void SetParameter(string key, int index, int value);
        public void SetParameter(string key, bool value);
        public void SetParameter(string key, string id, bool value);
        public void SetParameter(string key, int index, bool value);
        public void SetParameter(string key, Vector3 value);
        public void SetParameter(string key, string id, Vector3 value);
        
        public float GetParameter(string key, float defaultValue);
        public float GetParameter(string key, int index, float defaultValue);
        public int GetParameter(string key, int defaultValue);
        public int GetParameter(string key, int index, int defaultValue);
        public bool GetParameter(string key, bool defaultValue);
        public bool GetParameter(string key, int index, bool defaultValue);
        public bool GetParameter(string key, string id, bool defaultValue);
        public Vector3 GetParameter(string key, Vector3 defaultValue);
        public Vector3 GetParameter(string key, string id, Vector3 defaultValue);
    }
}