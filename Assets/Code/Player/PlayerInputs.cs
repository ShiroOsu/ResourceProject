using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour, PlayerControls.IPlayerActions
{
    [Header("General")]
    public PlayerData m_Data = null;
    public Camera m_Camera = null;

    [Header("MultiSelection")]
    public float m_HoldTime = 0.01f;
    public RectTransform m_SelectionImage;
    private Vector2 m_BoxStartPos;

    private LayerMask m_InteractionMask;
    private List<GameObject> m_SelectedUnitsList = null;
    private Vector2 mousePosition;
    private PlayerControls m_PlayerControls;

    private void Awake()
    {
        if (!m_Data)
        {
            m_Data = ScriptableObject.CreateInstance<PlayerData>();
        }

        m_SelectedUnitsList = new List<GameObject>();

        m_InteractionMask = m_Data.interactionLayer;

        m_PlayerControls = new PlayerControls();
        m_PlayerControls.Player.SetCallbacks(this);
    }

    private void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();

        IsLMBHoldingDown();
    }

    #region Enable PlayerControls
    private void OnEnable()
    {
        m_PlayerControls.Enable();
    }

    private void OnDisable()
    {
        m_PlayerControls.Disable();
    }
    #endregion

    public void OnLeftMouse(InputAction.CallbackContext context)
    {
        m_BoxStartPos = mousePosition;

        Ray ray = m_Camera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, m_InteractionMask))
        {
            Debug.Log("Clicked on " + hit.transform.name + " (Left Click)");

            // What if we click on a building?
            if (hit.transform.GetComponent<IUnit>() != null)
            {
                if (m_SelectedUnitsList.Contains(hit.transform.gameObject))
                {
                    m_SelectedUnitsList.Clear();
                }

                m_SelectedUnitsList.Add(hit.transform.gameObject);
            }
            else { m_SelectedUnitsList.Clear(); } 
        }
    }

    public void OnRightMouse(InputAction.CallbackContext context)
    {
        Ray ray = m_Camera.ScreenPointToRay(mousePosition);
        Vector3 newPosition;

        // Temp
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, m_InteractionMask))
        {
            newPosition = hit.point;
            Debug.Log(newPosition + " (Right Click)");
        }
        else { return; }

        if (context.performed)
        {
            if (m_SelectedUnitsList.Count < 1)
                return;
           
            foreach (var unit in m_SelectedUnitsList)
            {
                unit.GetComponent<IUnit>()?.Move(newPosition);
            }
        }
    }

    private void IsLMBHoldingDown()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            m_HoldTime -= Time.deltaTime;

            if (m_HoldTime < 0f)
            {
                MultiSelectionBox();
            }
        }
        else
        {
            if (m_SelectionImage.gameObject.activeInHierarchy)
                m_SelectionImage.gameObject.SetActive(false);
            
            AddUnitsToList();

            m_HoldTime = 0.01f;
        }
    }

    private void AddUnitsToList()
    {
        Vector2 min = m_SelectionImage.anchoredPosition - (m_SelectionImage.sizeDelta * 0.5f);
        Vector2 max = m_SelectionImage.anchoredPosition + (m_SelectionImage.sizeDelta * 0.5f);

        GameObject[] allUnits = FindObjectsOfType<GameObject>();
        foreach (var unit in allUnits)
        {
            if (unit.activeInHierarchy && unit.GetComponent<IUnit>() != null)
            {
                Vector3 unitScreenPos = m_Camera.WorldToScreenPoint(unit.transform.position);

                if (unitScreenPos.x > min.x &&
                    unitScreenPos.x < max.x &&
                    unitScreenPos.y > min.y &&
                    unitScreenPos.y < max.y)
                {
                    m_SelectedUnitsList.Add(unit);
                }
            }
        }
    }

    private void MultiSelectionBox()
    {
        if (!m_SelectionImage.gameObject.activeInHierarchy)
            m_SelectionImage.gameObject.SetActive(true);

        float width = mousePosition.x - m_BoxStartPos.x;
        float height = mousePosition.y - m_BoxStartPos.y;

        m_SelectionImage.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        m_SelectionImage.anchoredPosition = m_BoxStartPos + new Vector2(width * 0.5f, height * 0.5f);
    }
}