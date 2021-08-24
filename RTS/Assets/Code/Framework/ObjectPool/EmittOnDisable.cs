using System;
using UnityEngine;

namespace Code.Framework.ObjectPool
{
    [ExecuteAlways]
    public class EmittOnDisable : MonoBehaviour
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
