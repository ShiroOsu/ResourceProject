using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour, Controls.ICameraActions
{
    // Expose to inspector
    public float m_CameraSpeed = 10f;
    public float m_RotateSpeed = 5f;

    private Controls m_CameraControls = null;
    private Vector3 m_MoveDirection;
    private Vector3 m_ZoomDirection;
    private Vector3 m_RotationDirection;

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
        MoveCamera(Time.deltaTime);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>();

        m_MoveDirection.x = direction.x;
        m_MoveDirection.z = direction.y;
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        var scroll = context.ReadValue<float>();
        m_ZoomDirection.y = scroll;
    }

    private void Zoom(float time)
    {
        // Add a limit
        // lerp for smooth zoom maybe

        if (m_ZoomDirection.y > 0f)
        {
            transform.position -= m_ZoomDirection * time;
        }
        else
        {
            transform.position -= m_ZoomDirection * time;
        }
    }

    public void OnRotation(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<float>();
        m_RotationDirection.y = direction;
    }

    private void MoveCamera(float deltaTime)
    {
        Zoom(deltaTime);
        transform.position += m_MoveDirection * m_CameraSpeed * deltaTime;
        transform.Rotate(m_RotationDirection * m_RotateSpeed * deltaTime, Space.Self);
    }
}