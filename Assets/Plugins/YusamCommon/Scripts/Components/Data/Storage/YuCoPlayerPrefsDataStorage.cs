using UnityEngine;

namespace YusamCommon
{
    public class YuCoPlayerPrefsDataStorage : YuCoBaseDataStorage
    {
        public override void SetParameter(string key, float value)
        {
            YuCoPlayerPrefsHelper.SetParameter(key, value);
        }

        public override void SetParameter(string key, int index, float value)
        {
            YuCoPlayerPrefsHelper.SetParameter(key, index, value);
        }

        public override void SetParameter(string key, int value)
        {
            YuCoPlayerPrefsHelper.SetParameter(key, value);
        }

        public override void SetParameter(string key, int index, int value)
        {
            YuCoPlayerPrefsHelper.SetParameter(key, index, value);
        }

        public override void SetParameter(string key, bool value)
        {
            YuCoPlayerPrefsHelper.SetParameter(key, value);
        }

        public override void SetParameter(string key, string id, bool value)
        {
            YuCoPlayerPrefsHelper.SetParameter(key, id, value);
        }

        public override void SetParameter(string key, int index, bool value)
        {
            YuCoPlayerPrefsHelper.SetParameter(key, index, value);
        }

        public override void SetParameter(string key, Vector3 value)
        {
            YuCoPlayerPrefsHelper.SetParameter(key, value);
        }

        public override void SetParameter(string key, string id, Vector3 value)
        {
            YuCoPlayerPrefsHelper.SetParameter(key, id, value);
        }

        public override float GetParameter(string key, float defaultValue)
        {
            return YuCoPlayerPrefsHelper.GetParameter(key, defaultValue);
        }

        public override float GetParameter(string key, int index, float defaultValue)
        {
            return YuCoPlayerPrefsHelper.GetParameter(key, index, defaultValue);
        }

        public override int GetParameter(string key, int defaultValue)
        {
            return YuCoPlayerPrefsHelper.GetParameter(key, defaultValue);
        }

        public override int GetParameter(string key, int index, int defaultValue)
        {
            return YuCoPlayerPrefsHelper.GetParameter(key, index, defaultValue);
        }

        public override bool GetParameter(string key, bool defaultValue)
        {
            return YuCoPlayerPrefsHelper.GetParameter(key, defaultValue);
        }

        public override bool GetParameter(string key, int index, bool defaultValue)
        {
            return YuCoPlayerPrefsHelper.GetParameter(key, index, defaultValue);
        }

        public override bool GetParameter(string key, string id, bool defaultValue)
        {
            return YuCoPlayerPrefsHelper.GetParameter(key, id, defaultValue);
        }

        public override Vector3 GetParameter(string key, Vector3 defaultValue)
        {
            return YuCoPlayerPrefsHelper.GetParameter(key, defaultValue);
        }

        public override Vector3 GetParameter(string key, string id, Vector3 defaultValue)
        {
            return YuCoPlayerPrefsHelper.GetParameter(key, id, defaultValue);
        }
    }
}