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

        if (pressed)
        {
            Physics.Raycast(m_Origin, m_Direction, maxDistance:Mathf.Infinity, m_LayerMask);
            Debug.Log("Left mouse button pressed");
        }
    }
}