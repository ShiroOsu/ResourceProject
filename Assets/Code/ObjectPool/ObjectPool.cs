using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectPool
{
    private readonly uint m_ExpandBy;
    private readonly GameObject m_Prefab;
    private Transform m_Parent;
    public readonly Stack<GameObject> objects = new Stack<GameObject>();

    public ObjectPool(uint initSize, GameObject prefab, uint expandBy = 1, Transform parent = null, bool checkParentForChildren = false)
    {
        m_ExpandBy = expandBy < 1 ? 1 : expandBy;
        m_Prefab = prefab;
        m_Parent = parent;
        Expand(initSize < 1 ? 1 : initSize);
    }

    private void Expand(uint amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject instance = Object.Instantiate(m_Prefab, m_Parent);
            EmittOnDisable emittOnDisable = instance.AddComponent<EmittOnDisable>();
            emittOnDisable.OnDisableGameObject += UnRent;
            instance.SetActive(false);
            objects.Push(instance);
        }
    }

    private void UnRent(GameObject gameObject)
    {
        objects.Push(gameObject);
    }

    public GameObject Rent(bool activate)
    {
        if (objects.Count == 0)
        {
            Expand(m_ExpandBy);
        }

        GameObject instance = objects.Pop();
        instance = instance != null ? instance : Rent(activate);
        instance.SetActive(activate);
        return instance;
    }
}
