using System;
using UnityEngine;

namespace Code.Tools.ObjectPool
{
    [ExecuteAlways]
    public class EmitOnDisable : MonoBehaviour
    {
        public event Action<GameObject> OnDisableGameObject;

        private void OnDisable()
        {
            OnDisableGameObject?.Invoke(gameObject);
        }

        public void ClearAction()
        {
            OnDisableGameObject = null;
        }
    }
}
