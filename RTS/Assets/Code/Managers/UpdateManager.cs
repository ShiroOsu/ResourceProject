using System;
using Code.Tools.HelperClasses;
using UnityEngine;

namespace Code.Managers
{
    public class UpdateManager : Singleton<UpdateManager>
    {
        public event Action OnUpdate;
        public event Action OnFixedUpdate;
        public event Action OnLateUpdate;
        
        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            OnLateUpdate?.Invoke();
        }
    }
}
