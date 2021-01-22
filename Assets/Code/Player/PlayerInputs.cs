using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour, PlayerControls.IPlayerActions
{
    public PlayerData m_Data = null;
    private PlayerControls m_PlayerControls;

    // Raycast temp holders
    private LayerMask m_LayerMask;
    private Vector3 m_Origin;
    private Vector3 m_Direction;


    private void Awake()
    {
        if (!m_Data)
        {
            m_Data = ScriptableObject.CreateInstance<PlayerData>();
        }

        m_LayerMask = LayerMask.GetMask("InteractionLayer");

        m_PlayerControls = new PlayerControls();
        m_PlayerControls.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        m_PlayerControls.Enable();
    }

    private void OnDisable()
    {
        m_PlayerControls.Disable();
    }

    public void OnMouse(InputAction.CallbackContext context)
    {
        var pressed = context.ReadValueAsButton();
        var mousePos = Mouse.current.position.ReadValue();

        if (pressed)
        {
            m_Direction = new Vector3(mousePos.x, 0f, mousePos.y);
            bool ray = Physics.Raycast(m_Origin, m_Direction, out RaycastHit hit, maxDistance:Mathf.Infinity, m_LayerMask);

            if (ray)
            {
                Debug.Log(hit.collider.name);
            }

            Debug.Log("Left mouse button pressed");
        }
    }
}