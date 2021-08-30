using Code.Framework.Enums;
using Code.Framework.Interfaces;
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
            unit.TryGetComponent(out IUnit u);
            u.Move(endPos);
        }
    }
}