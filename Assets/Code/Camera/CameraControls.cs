using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour, Controls.ICameraActions
{
    private Controls m_CameraControls = null;
    private Vector3 m_DirectionVector;
    private Vector3 m_ZoomDirection;
    private float m_CameraSpeed = 10f;

    public void Awake()
    {
        m_CameraControls = new Controls();
        m_CameraControls.Camera.SetCallbacks(this);
    }

    private void OnEnable()
    {
        m_CameraControls.Enable();
    }

    private void OnDisable()
    {
        m_CameraControls.Disable();
    }

    public void Update()
    {
        if (m_ZoomDirection.y > 0f)
        {
            transform.position -= m_ZoomDirection * Time.deltaTime;
        } 
        else
        {
            transform.position -= m_ZoomDirection * Time.deltaTime; 
        }

        transform.position += m_DirectionVector * m_CameraSpeed * Time.deltaTime;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>();

        m_DirectionVector.x = direction.x;
        m_DirectionVector.z = direction.y;
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        var scroll = context.ReadValue<float>();
        m_ZoomDirection.y = scroll;
    }

    public void OnRotation(InputAction.CallbackContext context)
    {
        var qe = context.ReadValue<float>();
        Debug.Log(qe);
    }
}