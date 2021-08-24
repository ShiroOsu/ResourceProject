using Code.Framework.Enums;
using Code.Framework.Interfaces;
using Code.Logger;
using UnityEditor.Connect;
using UnityEngine;

namespace Code.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        private static SpawnManager s_Instance = null;
        public static SpawnManager Instance => s_Instance ??= FindObjectOfType<SpawnManager>();

        public void SpawnUnit(UnitType type, Vector3 startPos, Vector3 endPos)
        {
            var unit = PoolManager.Instance.GetPooledUnit(type, true);
            unit.transform.position = startPos;

            // Spawn flag not set
            if (endPos == Vector3.zero)
            {
                Log.Message("SpawnManager", "Spawn flag not set for " + type);
                endPos = (startPos + new Vector3(10f, 0f, 0f));
            }

            unit.TryGetComponent(out IUnit u);
            u.Move(endPos);
        }
    }
}