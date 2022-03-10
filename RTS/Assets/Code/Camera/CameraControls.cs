using Camera;
using Code.Managers;
using Code.Managers.Data;
using Code.Player;
using Code.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Camera
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

        private Vector2 m_ScreenSize;
        private DataManager m_DataManager;
        private MouseInputs m_MouseInputs;
        
        public void Awake()
        {
            m_DataManager = DataManager.Instance;
            m_MouseInputs = m_DataManager.mouseInputs;
            
            m_ScreenSize.x = (Screen.width - 1);
            m_ScreenSize.y = (Screen.height - 1);
            
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
        private void UpdateCameraDirectionByKeys()
        {
            var direction = m_CameraControls.Camera.Movement.ReadValue<Vector2>();

            m_ForwardVector.x = direction.x;
            m_ForwardVector.z = direction.y;
        }

        private void UpdateCameraDirectionByMouse()
        {
            if (!Application.isFocused)
                return;
            
            var direction = m_CameraControls.Camera.MoveCameraMouse.ReadValue<Vector2>();

            if (direction.x >= m_ScreenSize.x)
            {
                m_ForwardVector.x = 1;
            } 
            else if (direction.x <= 1)
            {
                m_ForwardVector.x = -1;
            }
            
            if (direction.y >= m_ScreenSize.y)
            {
                m_ForwardVector.z = 1;
            } 
            else if (direction.y <= 1)
            {
                m_ForwardVector.z = -1;
            }
        }

        public void OnMovement(InputAction.CallbackContext context) { }
        public void OnMoveCameraMouse(InputAction.CallbackContext context) { }

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
            UpdateCameraDirectionByKeys();
            UpdateCameraDirectionByMouse();

            Zoom(deltaTime);
            m_ThisTransform.Rotate(m_RotationDirection * (m_RotationSpeed * deltaTime), Space.Self);

            var forwardDirection = m_ThisTransform.rotation * m_ForwardVector;
            m_ThisTransform.position += forwardDirection.normalized * (m_CameraSpeed * deltaTime);
        }
    }
}