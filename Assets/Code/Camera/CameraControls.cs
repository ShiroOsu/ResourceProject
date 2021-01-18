using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private readonly CameraControls m_CameraControls = null;

    public void Awake()
    {
        
    }

    private void OnEnable()
    {
        m_CameraControls.OnEnable();
    }

    private void OnDisable()
    {
        m_CameraControls.OnDisable();
    }
}