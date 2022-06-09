using Code.ScriptableObjects;
using Code.Tools.Enums;
using UnityEngine;

namespace Code.UI.Resource
{
    public abstract class ResourceUIManager
    {
        protected ResourceUIManager() { }
        public abstract ResourceType Type { get; }
        public abstract void EnableMainUI(bool active, GameObject resource, ResourceType type, GameObject image,
            ResourceData data);

        protected abstract void BindButtons();
    }
}