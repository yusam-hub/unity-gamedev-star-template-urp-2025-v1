using UnityEngine;

namespace YusamCommon
{
    public class YuCoDropdown : PropertyAttribute
    {
        public string valuesGetterName;

        public YuCoDropdown(string valuesGetterName)
        {
            this.valuesGetterName = valuesGetterName;
        }
    }
}