using System;
using Code.Interfaces;
using Code.Tools.Enums;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Units
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class BaseUnit : MonoBehaviour, IUnit, ISavable
    {
        protected NavMeshAgent Agent;
        protected UnitType UnitType;
        protected TextureAssetType TextureAssetType;

        public virtual void ShouldSelect(bool @select) => throw new NotImplementedException();
        public virtual void ActivateSelectionCircle(bool active) => throw new NotImplementedException();
        public virtual void OnDestroy() => Destroy(this);
        public virtual UnitType GetUnitType() => UnitType;
        public virtual TextureAssetType GetUnitTexture() => TextureAssetType;
        public virtual GameObject GetUnitImage() => throw new NotImplementedException();
        public virtual GameObject GetUnitObject() => gameObject;
        public virtual void Move(Vector3 destination) => Agent.SetDestination(destination);
        public virtual void StopAgent(bool stop) => Agent.isStopped = stop;
        public virtual void Save() => throw new NotImplementedException();
    }
}