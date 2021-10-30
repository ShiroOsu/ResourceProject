using UnityEngine;

namespace Code.Framework.Tools
{
    [CreateAssetMenu(fileName = "AutoCreateScriptableObject", menuName = "ScriptableObjects/AutoCreateScriptableObject")]
    public class AutoCreateScriptableObject : ScriptableObject
    {
        [Multiline(30)]
        public string preCode;

        [Multiline(20)]
        public string managerCode;
    }
}