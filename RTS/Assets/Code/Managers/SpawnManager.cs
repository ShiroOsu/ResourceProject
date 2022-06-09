using Code.Interfaces;
using Code.Tools.Enums;
using Code.Tools.HelperClasses;
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