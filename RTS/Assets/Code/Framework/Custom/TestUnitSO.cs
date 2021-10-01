using UnityEngine;

namespace Code.Framework.Custom
{
    [CreateAssetMenu(fileName = "TestUnitSO", menuName = "ScriptableObjects/TestUnitSO")]
    public class TestUnitSO : ScriptableObject
    {
        public string UnitName;

        [Multiline(30)]
        public string preCode;
    }
}
