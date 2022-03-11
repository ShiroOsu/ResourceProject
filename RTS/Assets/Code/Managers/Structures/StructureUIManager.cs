using Code.Enums;
using Code.ScriptableObjects;
using UnityEngine;

namespace Code.Managers.Structures
{
    public abstract class StructureUIManager
    {
        protected StructureUIManager() { }

        public abstract StructureType Type { get; }

        public abstract void EnableMainUI(bool active, GameObject structure, StructureType type, GameObject image,
            StructureData data);

        protected abstract void BindButtons();
    }
}