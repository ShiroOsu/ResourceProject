using Camera;
using Code.Player.Camera.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Player.Camera
{
    public class CameraControls : MonoBehaviour, Controls.ICameraActions
    {
        // Values set in CameraData
        [SerializeField] private CameraData data;
        private float m_CameraSpeed;
        private float m_RotationSpeed;
        private float m_ZoomSpeed;

        private Controls m_CameraControls;
        private Transform m_ThisTransform;
        private Vector3 m_ForwardVector;
        private Vector3 m_RotationDirection;
        private Vector3 m_ZoomVector;
        [HideInInspector] public bool canZoom = true;

        public void Awake()
        {
            m_ThisTransform = transform;
            
            if (!data)
            {
                data = ScriptableObject.CreateInstance<CameraData>();
            }

            m_CameraSpeed = data.cameraSpeed;
            m_RotationSpeed = data.rotationSpeed;
            m_ZoomSpeed = data.zoomSpeed;

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

        // WASD
        private void UpdateCameraDirection()
        {
            var direction = m_CameraControls.Camera.Movement.ReadValue<Vector2>();

            m_ForwardVector.x = direction.x;
            m_ForwardVector.z = direction.y;
        }

        public void OnMovement(InputAction.CallbackContext context) { }

        public void OnZoom(InputAction.CallbackContext context)
        {
            if (!canZoom)
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
                m_ThisTransform.position -= m_ZoomVector * (m_ZoomSpeed * deltaTime);
            }
            else
            {
                m_ThisTransform.position -= m_ZoomVector * (m_ZoomSpeed * deltaTime);
            }
        }

        public void OnRotation(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<float>();
            m_RotationDirection.y = direction;
        }

        private void MoveCamera(float deltaTime)
        {
            UpdateCameraDirection();

            Zoom(deltaTime);
            m_ThisTransform.Rotate(m_RotationDirection * (m_RotationSpeed * deltaTime), Space.Self);

            var forwardDirection = m_ThisTransform.rotation * m_ForwardVector;
            m_ThisTransform.position += forwardDirection.normalized * (m_CameraSpeed * deltaTime);
        }
    }
}