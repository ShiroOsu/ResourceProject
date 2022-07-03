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
        [SerializeField] private UnityEngine.Camera _camera;
        private float m_CameraSpeed;
        private float m_RotationSpeed;

        private Controls m_CameraControls;
        private Vector2 m_MapBorders;
        private readonly Vector2[] m_CameraBounds = new Vector2[4];
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
            
            m_StartRotation = transform.rotation;
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

        private void CalculateCameraBounds()
        {
            var bottomLeftRay = _camera.ViewportPointToRay(Vector3.zero);
            var bottomRightRay = _camera.ViewportPointToRay(Vector3.right);
            var topLeftRay = _camera.ViewportPointToRay(Vector3.up);
            var topRightRay = _camera.ViewportPointToRay(new Vector3(1, 1, 0));

            m_CameraBounds[0] = GetPointAtHeight(bottomLeftRay, 0);
            m_CameraBounds[1] = GetPointAtHeight(topLeftRay, 0);
            m_CameraBounds[2] = GetPointAtHeight(topRightRay, 0);
            m_CameraBounds[3] = GetPointAtHeight(bottomRightRay, 0); // not used
        }

        private Vector3 KeepCameraInBounds(ref Vector3 nextDirection)
        {
            var nextX = nextDirection.x * 10f;
            var nextY = nextDirection.z * 10f;
            
            // Camera can still move up or down if its at the left or right edge
            if (m_CameraBounds[1].x + nextX <= 0f && !(m_CameraBounds[0].y + nextY <= 0f || m_CameraBounds[1].y + nextY >= m_MapBorders.y)
                || m_CameraBounds[2].x + nextX >= m_MapBorders.x && !(m_CameraBounds[0].y + nextY <= 0f || m_CameraBounds[1].y + nextY >= m_MapBorders.y))
            {
                return new Vector3(0f, 0f, nextDirection.z);
            }

            // Camera can still move left or right if its at the up or down edge
            if (m_CameraBounds[0].y + nextY <= 0f && !(m_CameraBounds[1].x + nextX <= 0f || m_CameraBounds[2].x + nextX >= m_MapBorders.x)
                || m_CameraBounds[1].y + nextY >= m_MapBorders.y && !(m_CameraBounds[1].x + nextX <= 0f || m_CameraBounds[2].x + nextX >= m_MapBorders.x))
            {
                return new Vector3(nextDirection.x, 0f, 0f);
            }
            
            // Left side, Right side, Down, Up
            if (m_CameraBounds[1].x + nextX <= 0f || m_CameraBounds[2].x + nextX >= m_MapBorders.x || 
                m_CameraBounds[0].y + nextY <= 0f || m_CameraBounds[1].y + nextY >= m_MapBorders.y)
            {
                return Vector3.zero;
            }

            return nextDirection;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(_camera.ViewportPointToRay(Vector3.zero));
            Gizmos.DrawRay(_camera.ViewportPointToRay(Vector3.right));
            Gizmos.DrawRay(_camera.ViewportPointToRay(Vector3.up));
            Gizmos.DrawRay(_camera.ViewportPointToRay(new Vector3(1, 1, 0)));
        }

        private static Vector2 GetPointAtHeight(Ray ray, float height)
        {
            var point = ray.origin + ((ray.origin.y - height) / -ray.direction.y) * ray.direction;
            return new Vector2(point.x, point.z);
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

            if (transform.position.y - zoom.y > 60f || transform.position.y - zoom.y < 10f)
            {
                return;
            }
            
            if (m_ZoomVector.y > 0f)
            {
                transform.position -= zoom;
            }
            else
            {
                transform.position -= zoom;
            }
        }

        public void OnRotation(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<float>();
            m_RotationDirection.y = direction;
        }

        private void MoveCamera(float deltaTime)
        {
            CalculateCameraBounds();
            UpdateCameraDirectionByKeys();
            UpdateCameraDirectionByMouse();

            var t = transform;

            if (CanRotate)
            {
                t.Rotate(m_RotationDirection * (m_RotationSpeed * deltaTime), Space.Self);
            }

            
            var forwardDirection = t.rotation * m_ForwardVector;
            var nextDirection = forwardDirection.normalized * (m_CameraSpeed * deltaTime);

            nextDirection = KeepCameraInBounds(ref nextDirection);
            
            t.position += nextDirection;
        }

        public void CameraReset()
        {
            transform.rotation = m_StartRotation;
        }
    }
}