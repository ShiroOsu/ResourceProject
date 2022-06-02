using Code.Enums;
using Code.HelperClasses;
using Code.Interfaces;
using UnityEngine;

namespace Code.Managers
{
    public class SpawnManager : Singleton<SpawnManager>
    {
        public void SpawnUnit(TextureAssetType type, Vector3 startPos, Vector3 endPos)
        {
            var unit = PoolManager.Instance.GetPooledUnit(type, true);
            unit.transform.position = startPos;
            unit.TryGetComponent(out IUnit u);
            u.StopAgent(false);
            u.Move(endPos);
        }
    }
}