using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour, PlayerControls.IPlayerActions
{
    public PlayerData m_Data = null;
    public Camera m_Camera = null;
    private PlayerControls m_PlayerControls;

    // Raycast temp holders
    private LayerMask m_LayerMask;

    // Temp unit stuff
    private List<GameObject> m_Units = null;
    private Vector2 mousePosition;

    private void Awake()
    {
        if (!m_Data)
        {
            m_Data = ScriptableObject.CreateInstance<PlayerData>();
        }

        m_Units = new List<GameObject>();

        m_LayerMask = m_Data.interactionLayer;

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

    public void OnLeftMouse(InputAction.CallbackContext context)
    {
        var leftMouse = context.ReadValueAsButton();
        mousePosition = Mouse.current.position.ReadValue();

        // why check leftmouse?
        if (leftMouse && context.performed)
        {
            Ray ray = m_Camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, m_LayerMask))
            {
                Debug.Log("Clicked on " + hit.transform.name + " (Left Click)");
                
                if (hit.transform.GetComponent<IUnit>() != null)
                {
                    m_Units.Add(hit.transform.gameObject);
                } else { m_Units.Clear(); } // Temp
            } 
        }
    }

    public void OnRightMouse(InputAction.CallbackContext context)
    {
        var rightMouse = context.ReadValueAsButton();
        mousePosition = Mouse.current.position.ReadValue();

        Ray ray = m_Camera.ScreenPointToRay(mousePosition);
        Vector3 newPosition;

        // Temp
        // Click on something else
        if (Physics.Raycast(ray, out RaycastHit hit, m_LayerMask))
        {
            // Check if new position is 'available'
            if (true)
            {

            }

            newPosition = hit.point;
            Debug.Log(newPosition + " (Right Click)");

        } else { return; }

        if (rightMouse && context.performed)
        {
            if (m_Units.Count < 1)
                return;

            foreach (var unit in m_Units)
            {
                unit.GetComponent<IUnit>()?.Move(newPosition);
            }
        }
    }
}