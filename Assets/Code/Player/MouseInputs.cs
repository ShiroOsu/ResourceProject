using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MouseInputs : MonoBehaviour, MouseControls.IMouseActions
{
    [Header("General")]
    [SerializeField] private MouseData m_Data = null;
    [SerializeField] private Camera m_Camera = null;
    [SerializeField] private Animator m_Animator = null;

    [Header("Multi Selection")]
    [SerializeField] private RectTransform m_SelectionImage;
    private Vector2 m_BoxStartPos;
    private float m_HoldTime = 0.05f;

    // Interaction
    private LayerMask m_UnitMask;
    private LayerMask m_StructureMask;
    private LayerMask m_GroundMask;
    private List<GameObject> m_SelectedUnitsList = null;
    private Vector2 mousePosition;

    // Controls
    private MouseControls m_MouseControls;

    // Stop click animation timer
    private float m_Timer = 1f;

    private IStructure m_CurrentStructure;

    private void Awake()
    {
        if (!m_Data)
        {
            m_Data = ScriptableObject.CreateInstance<MouseData>();
        }

        m_SelectedUnitsList = new List<GameObject>();

        m_UnitMask = m_Data.unitMask;
        m_StructureMask = m_Data.structureMask;
        m_GroundMask = m_Data.groundMask;

        m_MouseControls = new MouseControls();
        m_MouseControls.Mouse.SetCallbacks(this);
    }
    private void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();

        // Not a complete fix
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            IsLMBHoldingDown();
        }

        if (m_Animator.gameObject.activeSelf)
        {
            StopClickAnimation();
        }
    }

    #region Enable PlayerControls
    private void OnEnable()
    {
        m_MouseControls.Enable();
    }

    private void OnDisable()
    {
        m_MouseControls.Disable();
    }
    #endregion

    public void OnLeftMouse(InputAction.CallbackContext context)
    {
        // Context is checked to make sure it is clicked once
        if (context.started)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("UI CLICK");
            } 
            else
            {
                ClickingOnUnitsAndStructures();
            }
        }
    }

    public void OnRightMouse(InputAction.CallbackContext context)
    {
        MovingSelectedUnits();
    }

    private void ClickingOnUnitsAndStructures()
    {
        m_BoxStartPos = mousePosition;

        Ray ray = m_Camera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, Mathf.Infinity, m_GroundMask))
        {
            if (m_CurrentStructure != null)
            {
                m_CurrentStructure.Unselect();
                m_CurrentStructure = null;
            }

            SelectUnits(false);
        }

        if (Physics.Raycast(ray, out RaycastHit s_Hit, Mathf.Infinity, m_StructureMask))
        {
            if (s_Hit.transform.parent.TryGetComponent(out IStructure structure))
            {
                ClickOnBuilding(structure);
            }
        }

        if (Physics.Raycast(ray, out RaycastHit u_Hit, Mathf.Infinity, m_UnitMask))
        {
            if (u_Hit.transform.parent.GetComponent<IUnit>() != null)
            {
                ClickOnUnit(u_Hit.transform.parent.gameObject);
            }
        }
    }

    private void ClickOnBuilding(IStructure structure)
    {
        // When clicking on another building while another is currently selected
        // unselect the previous building
        if (m_CurrentStructure != null)
        {
            m_CurrentStructure.Unselect();
        }

        m_CurrentStructure = structure;
        m_CurrentStructure.Selected();
    }

    private void ClickOnUnit(GameObject unit)
    {
        if (!m_SelectedUnitsList.Contains(unit))
        {
            m_SelectedUnitsList.Add(unit);
            SelectUnits(true);
        }
        else SelectUnits(false);
    }

    private void MovingSelectedUnits()
    {
        if (m_SelectedUnitsList.Count < 1)
            return;

        Ray ray = m_Camera.ScreenPointToRay(mousePosition);
        Vector3 newPosition;

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, m_GroundMask))
        {
            newPosition = hit.point;
            PlayClickAnimation(true);
        }
        else { return; }

        foreach (var unit in m_SelectedUnitsList)
        {
            unit.TryGetComponent(out IUnit u);
            u.Move(newPosition);
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
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (m_SelectionImage.gameObject.activeInHierarchy)
                m_SelectionImage.gameObject.SetActive(false);

            AddUnitsInSelectionBox();

            m_HoldTime = 0.05f;
        }
    }

    private void AddUnitsInSelectionBox()
    {
        Vector2 min = m_SelectionImage.anchoredPosition - (m_SelectionImage.sizeDelta * 0.5f);
        Vector2 max = m_SelectionImage.anchoredPosition + (m_SelectionImage.sizeDelta * 0.5f);

        // Temp, because it finds ALL GameObjects in scene then loops through them,
        // then looking for the objects with the IUnit interface
        // Would be ideal to only needing to loop through a list that only contains units
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
                    // Add a limit? ex. max group of 10,20, etc..
                    if (!m_SelectedUnitsList.Contains(unit))
                    {
                        m_SelectedUnitsList.Add(unit);
                    }
                }
            }
        }
        SelectUnits(true);
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

    private void SelectUnits(bool select)
    {
        foreach (var unit in m_SelectedUnitsList)
        {
            unit.TryGetComponent(out IUnit u);

            if (select)
            {
                u.Selected();
            }
            else u.Unselect();
        }

        if (!select)
        {
            m_SelectedUnitsList.Clear();
        }
    }

    public Vector3 SetStructureUnitSpawnFlag(GameObject flag)
    {
        Ray ray = m_Camera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, m_GroundMask))
        {
            flag.transform.position = hit.point + new Vector3(0f, 1.5f, 0f);
            return flag.transform.position;
        }

        return Vector3.zero;
    }

    private void StopClickAnimation()
    {
        m_Timer -= Time.deltaTime;

        if (m_Timer < 0f)
        {
            PlayClickAnimation(false);
            m_Timer = 1f;
        }
    }

    private void PlayClickAnimation(bool active)
    {
        if (!active)
        {
            m_Animator.gameObject.SetActive(active);
            m_Animator.SetBool("Clicked", active);
            return;
        }

        Ray ray = m_Camera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, m_GroundMask))
        {
            if (hit.transform.GetComponent<Terrain>() != null)
            {
                m_Animator.gameObject.transform.position = hit.point + new Vector3(0f, 0.1f, 0f);
                m_Animator.gameObject.transform.rotation = Quaternion.LookRotation(-hit.normal);
                m_Animator.gameObject.SetActive(true);
                m_Animator.SetBool("Clicked", true);
            }
        }
    }
}