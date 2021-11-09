using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Code.Framework.ObjectPool
{
    public abstract class ObjectPoolBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        private T m_Prefab;
        private ObjectPool<T> m_ObjectPool;

        private ObjectPool<T> m_Pool
        {
            get
            {
                if (m_ObjectPool == null)
                {
                    throw new InvalidOperationException("You need to call InitPool before using it.");
                }

                return m_ObjectPool;
            }
            set => m_ObjectPool = value;
        }

        protected void InitPool(T prefab, int initial = 10, int max = 100, bool collectionChecks = false)
        {
            m_Prefab = prefab;
            m_Pool = new ObjectPool<T>(CreateSetup, GetSetup, ReleaseSetup, DestroySetup, collectionChecks, initial, max);
        }

        #region Overrides

        protected virtual T CreateSetup() => Instantiate(m_Prefab);
        protected virtual void GetSetup(T obj) => obj.gameObject.SetActive(true);
        protected virtual void ReleaseSetup(T obj) => obj.gameObject.SetActive(false);
        protected virtual void DestroySetup(T obj) => Destroy(obj);

        #endregion
        
        #region Getters

        public T Get() => m_Pool.Get();
        public void Release(T obj) => m_Pool.Release(obj);

        #endregion
    }
}