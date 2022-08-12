using System;
using Code.Interfaces;
using Code.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Code.SaveSystem.Data
{
    [Serializable]
    public class WorkerData : IUnitData
    {
        public Vector3 position;
        public Quaternion rotation;
        
        public void Save(GameObject gameObject)
        {
            position = gameObject.transform.position;
            rotation = gameObject.transform.rotation;
        }

        public void Instantiate(GameObject prefab)
        {
            var newObj = PoolManager.Instance.workerPool.Rent(true);
            newObj.TryGetComponent<NavMeshAgent>(out var agent);
            agent.Warp(position);
        }
    }
}