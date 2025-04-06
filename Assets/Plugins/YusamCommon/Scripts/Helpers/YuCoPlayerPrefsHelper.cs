using UnityEngine;

namespace YusamCommon
{
    public static class YuCoPlayerPrefsHelper
    {
        public static readonly bool IsDebugging = false;
        public static void SetParameter(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
            if (IsDebugging)
            {
                Debug.Log($"SetParameter {key} => {value}");
            }
        }

        public static void SetParameter(string key, int index, float value)
        {
            PlayerPrefs.SetFloat(key + "_" + index, value);
            PlayerPrefs.Save();
            if (IsDebugging)
            {
                Debug.Log($"SetParameter {key} => {index} => {value}");
            }
        }

        public static void SetParameter(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
            if (IsDebugging)
            {
                Debug.Log($"SetParameter {key} => {value}");
            }
        }

        public static void SetParameter(string key, int index, int value)
        {
            PlayerPrefs.SetInt(key + "_" + index, value);
            PlayerPrefs.Save();
            if (IsDebugging)
            {
                Debug.Log($"SetParameter {key} => {index} => {value}");
            }
        }

        public static void SetParameter(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
            PlayerPrefs.Save();
            if (IsDebugging)
            {
                Debug.Log($"SetParameter {key} => {value}");
            }
        }

        public static void SetParameter(string key, string id, bool value)
        {
            PlayerPrefs.SetInt(key + "_" + id, value ? 1 : 0);
            PlayerPrefs.Save();
            if (IsDebugging)
            {
                Debug.Log($"SetParameter {key} => {id} => {value}");
            }
        }
		
        public static void SetParameter(string key, int index, bool value)
        {
            PlayerPrefs.SetInt(key + "_" + index, value ? 1 : 0);
            PlayerPrefs.Save();
            if (IsDebugging)
            {
                Debug.Log($"SetParameter {key} => {index} => {value}");
            }
        }

        public static void SetParameter(string key, Vector3 value)
        {
            string nameX = key + "_" + "X";
            string nameY = key + "_" + "Y";
            string nameZ = key + "_" + "Z";

            PlayerPrefs.SetFloat(nameX, value.x);
            PlayerPrefs.SetFloat(nameY, value.y);
            PlayerPrefs.SetFloat(nameZ, value.z);

            PlayerPrefs.Save();
            
            if (IsDebugging)
            {
                Debug.Log($"SetParameter {key} => {value}");
            }
        }

        public static void SetParameter(string key, string id, Vector3 value)
        {
            string nameX = key + "_" + id + "_" + "X";
            string nameY = key + "_" + id + "_" + "Y";
            string nameZ = key + "_" + id + "_" + "Z";

            PlayerPrefs.SetFloat(nameX, value.x);
            PlayerPrefs.SetFloat(nameY, value.y);
            PlayerPrefs.SetFloat(nameZ, value.z);

            PlayerPrefs.Save();
            
            if (IsDebugging)
            {
                Debug.Log($"SetParameter {key} => {id} => {value}");
            }
        }

        public static float GetParameter(string key, float defaultValue)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                return defaultValue;
            }
            
            var value = PlayerPrefs.GetFloat(key);
            if (IsDebugging)
            {
                Debug.Log($"GetParameter {key} => {value}");
            }
            return value;
        }

        public static float GetParameter(string key, int index, float defaultValue)
        {
            if (!PlayerPrefs.HasKey(key + "_" + index))
            {
                return defaultValue;
            }

            var value = PlayerPrefs.GetFloat(key + "_" + index);
            if (IsDebugging)
            {
                Debug.Log($"GetParameter {key} => {index} => {value}");
            }
            return value;
        }

        public static int GetParameter(string key, int defaultValue)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                return defaultValue;
            }

            var value = PlayerPrefs.GetInt(key);
            if (IsDebugging)
            {
                Debug.Log($"GetParameter {key} => {value}");
            }
            return value;
        }

        public static int GetParameter(string key, int index, int defaultValue)
        {
            if (!PlayerPrefs.HasKey(key + "_" + index))
            {
                return defaultValue;
            }

            var value = PlayerPrefs.GetInt(key + "_" + index);
            if (IsDebugging)
            {
                Debug.Log($"GetParameter {key} => {index} => {value}");
            }
            return value;
        }

        public static bool GetParameter(string key, bool defaultValue)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                return defaultValue;
            }
            var value = PlayerPrefs.GetInt(key);
            if (IsDebugging)
            {
                Debug.Log($"GetParameter {key} => {value}");
            }
            return value == 1;
        }

        public static bool GetParameter(string key, int index, bool defaultValue)
        {
            if (!PlayerPrefs.HasKey(key + "_" + index))
            {
                return defaultValue;
            }
            var value = PlayerPrefs.GetInt(key + "_" + index);
            if (IsDebugging)
            {
                Debug.Log($"GetParameter {key} => {index} => {value}");
            }
            return value == 1;
        }
		
        public static bool GetParameter(string key, string id, bool defaultValue)
        {
            if (!PlayerPrefs.HasKey(key + "_" + id))
            {
                return defaultValue;
            }
            var value = PlayerPrefs.GetInt(key + "_" + id);
            if (IsDebugging)
            {
                Debug.Log($"GetParameter {key} => {id} => {value}");
            }
            return value == 1;
        }

        public static Vector3 GetParameter(string key, Vector3 defaultValue)
        {
            if (!PlayerPrefs.HasKey(key + "_" + "X") 
                || 
                !PlayerPrefs.HasKey(key + "_" + "Y")
                ||
                !PlayerPrefs.HasKey(key + "_" + "Z"))
            {
                return defaultValue;
            }
            
            Vector3 value = Vector3.zero;

            value.x = PlayerPrefs.GetFloat(key + "_" + "X");
            value.y = PlayerPrefs.GetFloat(key + "_" + "Y");
            value.z = PlayerPrefs.GetFloat(key + "_" + "Z");

            if (IsDebugging)
            {
                Debug.Log($"GetParameter {key} => {value}");
            }
            
            return value;
        }

        public static Vector3 GetParameter(string key, string id, Vector3 defaultValue)
        {
            if (!PlayerPrefs.HasKey(key + "_"  +id + "_" + "X") 
                || 
                !PlayerPrefs.HasKey(key + "_"  +id + "_" + "Y")
                ||
                !PlayerPrefs.HasKey(key + "_"  +id + "_" + "Z"))
            {
                return defaultValue;
            }
            Vector3 value = Vector3.zero;

            value.x = PlayerPrefs.GetFloat(key + "_" + id + "_" + "X");
            value.y = PlayerPrefs.GetFloat(key + "_" + id + "_" + "Y");
            value.z = PlayerPrefs.GetFloat(key + "_" + id + "_" + "Z");

            if (IsDebugging)
            {
                Debug.Log($"GetParameter {key} => {value}");
            }
            
            return value;
        }
    }
}