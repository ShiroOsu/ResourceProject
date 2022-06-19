using Camera;
using Code.Managers;
using Code.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Camera
{
    public class CameraControls : MonoBehaviour, Controls.ICameraActions
    {
        // Values set in CameraData
        [SerializeField] private CameraData data;
        [SerializeField] private Terrain mapTerrain;
        private float m_CameraSpeed;
        private float m_RotationSpeed;

        private Controls m_CameraControls;
        private Transform m_ThisTransform;
        private Vector2 m_MapBorders;
        private Vector3 m_ForwardVector;
        private Vector3 m_RotationDirection;
        private Vector3 m_ZoomVector;
        private Quaternion m_StartRotation;
        [HideInInspector] public bool canZoom = true;
        public bool CanRotate { private get; set; }
        public bool CanMoveCameraWithMouse { private get; set; }
        private bool temp;

        private Vector2 m_ScreenSize;
        
        public void Awake()
        {
            UpdateManager.Instance.OnUpdate += OnUpdate;

            var terrainSize = mapTerrain.terrainData.size;
            m_MapBorders.x = terrainSize.x;
            m_MapBorders.y = terrainSize.z;
            
            m_ScreenSize.x = (Screen.width - 1);
            m_ScreenSize.y = (Screen.height - 1);
            
            m_ThisTransform = transform;
            m_StartRotation = m_ThisTransform.rotation;
            CanMoveCameraWithMouse = true;
            
            if (!data)
            {
                data = ScriptableObject.CreateInstance<CameraData>();
            }

            m_CameraSpeed = data.cameraSpeed;
            m_RotationSpeed = data.rotationSpeed;

            m_CameraControls = new Controls();
            m_CameraControls.Camera.SetCallbacks(this);
        }

        private void OnEnable()
        {
            m_CameraControls.Enable();
            temp = false;
        }

        private void OnDisable()
        {
            m_CameraControls.Disable();
            temp = true;
        }

        private void OnUpdate()
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

            if (temp)
            {
                return;
            }

            if (!CanMoveCameraWithMouse)
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
            Zoom();            
        }

        private void Zoom()
        {
            var zoom = m_ZoomVector * 0.01f;

            if (m_ThisTransform.position.y - zoom.y > 60f || m_ThisTransform.position.y - zoom.y < 10f)
            {
                return;
            }
            
            if (m_ZoomVector.y > 0f)
            {
                m_ThisTransform.position -= zoom;
            }
            else
            {
                m_ThisTransform.position -= zoom;
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

            if (CanRotate)
            {
                m_ThisTransform.Rotate(m_RotationDirection * (m_RotationSpeed * deltaTime), Space.Self);
            }

            var forwardDirection = m_ThisTransform.rotation * m_ForwardVector;

            if (m_ThisTransform.position.x >= m_MapBorders.x || m_ThisTransform.position.z >= m_MapBorders.y)
            {
                return;
            }
            
            m_ThisTransform.position += forwardDirection.normalized * (m_CameraSpeed * deltaTime);
        }

        public void CameraReset()
        {
            m_ThisTransform.rotation = m_StartRotation;
        }
    }
}