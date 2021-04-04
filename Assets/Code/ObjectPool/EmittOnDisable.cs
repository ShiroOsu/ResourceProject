using UnityEngine;
using System;

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
