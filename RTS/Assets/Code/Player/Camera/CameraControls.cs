using Camera;
using Code.Player.Camera.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Player.Camera
{
    public class CameraControls : MonoBehaviour, Controls.ICameraActions
    {
        // Values set in CameraData
        public CameraData m_Data;
        private float m_CameraSpeed;
        private float m_RotationSpeed;
        private float m_ZoomSpeed;

        private Controls m_CameraControls;
        private Vector3 m_ForwardVector;
        private Vector3 m_RotationDirection;
        private Vector3 m_ZoomVector;
        [HideInInspector] public bool m_CanZoom = true;

        public void Awake()
        {
            if (!m_Data)
            {
                m_Data = ScriptableObject.CreateInstance<CameraData>();
            }

            m_CameraSpeed = m_Data.cameraSpeed;
            m_RotationSpeed = m_Data.rotationSpeed;
            m_ZoomSpeed = m_Data.zoomSpeed;

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

            m_ForwardVector.x = direction.x;
            m_ForwardVector.z = direction.y;
        }

        public void OnZoom(InputAction.CallbackContext context)
        {
            if (!m_CanZoom)
                return;
            
            var scrollValue = context.ReadValue<float>();
            m_ZoomVector.y = scrollValue;
        }

        // TODO: SMOOTH ZOOMING?, CLAMP ZOOM
        private void Zoom(float deltaTime)
        {
            // Add a limit
            // lerp for smooth zoom maybe
            // Zoom towards mouse pointers current position?

            if (m_ZoomVector.y > 0f)
            {
                transform.position -= m_ZoomVector * (m_ZoomSpeed * deltaTime);
            }
            else
            {
                transform.position -= m_ZoomVector * (m_ZoomSpeed * deltaTime);
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
            transform.Rotate(m_RotationDirection * (m_RotationSpeed * deltaTime), Space.Self);

            var forwardDirection = transform.rotation * m_ForwardVector;
            transform.position += forwardDirection.normalized * (m_CameraSpeed * deltaTime);
        }
    }
}
