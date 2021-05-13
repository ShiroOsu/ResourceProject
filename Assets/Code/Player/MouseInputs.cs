using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MouseInputs : MonoBehaviour, MouseControls.IMouseActions
{
    [Header("General")]
    [SerializeField] private UnityEngine.Camera m_Camera = null;
    [SerializeField] private Animator m_Animator = null;

    [Header("Multi Selection")]
    [SerializeField] private RectTransform m_SelectionImage;
    private Vector2 m_BoxStartPos;
    private bool m_MultiSelect = false;

    // Interaction
    private LayerMask m_UnitMask;
    private LayerMask m_StructureMask;
    private LayerMask m_GroundMask;
    private List<GameObject> m_SelectedUnitsList = null;
    //private GameObject m_CurrentUnit = null;
    private Vector2 m_MousePosition;

    public Ray PlacementRay => m_Camera.ScreenPointToRay(m_MousePosition);

    // Controls
    private MouseControls m_MouseControls;

    // Stop click animation timer
    private float m_Timer = 1f;

    private IStructure m_CurrentStructure;

    private void Awake()
    {
        m_SelectedUnitsList = new List<GameObject>();

        m_UnitMask = DataManager.Instance.mouseData.unitMask;
        m_StructureMask = DataManager.Instance.mouseData.structureMask;
        m_GroundMask = DataManager.Instance.mouseData.groundMask;

        m_MouseControls = new MouseControls();
        m_MouseControls.Mouse.SetCallbacks(this);
    }

    private void Update()
    {
        m_MousePosition = Mouse.current.position.ReadValue();

        if (m_Animator.gameObject.activeSelf)
        {
            StopClickAnimation();
        }

        if (m_MultiSelect)
        {
            MultiSelectionBox();
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
                // Clicking on UI
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

    public void OnLeftMouseButtonHold(InputAction.CallbackContext context)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && context.performed)
        {
            m_MultiSelect = true;
        }
    }

    private void ClickingOnUnitsAndStructures()
    {
        m_BoxStartPos = m_MousePosition;

        Ray ray = m_Camera.ScreenPointToRay(m_MousePosition);

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
        ClearCurrentStructure();

        SelectUnits(false);

        m_CurrentStructure = structure;
        m_CurrentStructure.ShouldSelect(true);
    }

    private void ClickOnUnit(GameObject unit)
    {
        ClearCurrentStructure();
        ClearUnitList();

        m_SelectedUnitsList.Add(unit);
        SelectUnits(true);
    }

    private void MovingSelectedUnits()
    {
        if (m_SelectedUnitsList.Count < 1)
            return;

        Ray ray = m_Camera.ScreenPointToRay(m_MousePosition);
        Vector3 newPosition;

        // When building, check if we hit a structure before ground, so builder unit does not move inside the structure
        if (Physics.Raycast(ray, Mathf.Infinity, m_StructureMask))
            return;

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

    private void AddUnitsInSelectionBox()
    {
        Vector2 rectPosition = new Vector2
        (m_SelectionImage.anchoredPosition.x - m_SelectionImage.sizeDelta.x * 0.5f,
            m_SelectionImage.anchoredPosition.y - m_SelectionImage.sizeDelta.y * 0.5f);

        Rect rect = new Rect(rectPosition, m_SelectionImage.sizeDelta);

        // Temp, because it finds ALL GameObjects in scene then loops through them,
        // then looking for the objects with the IUnit interface
        // Would be ideal to only needing to loop through a list that only contains selectable units
        GameObject[] allUnits = FindObjectsOfType<GameObject>(false);
        foreach (var unit in allUnits)
        {
            if (unit.TryGetComponent(out IUnit _))
            {
                Vector3 unitScreenPos = m_Camera.WorldToScreenPoint(unit.transform.position);

                if (rect.Contains(unitScreenPos))
                {
                    // Add a limit, ex. max group of 10,20, etc..
                    if (!m_SelectedUnitsList.Contains(unit))
                    {
                        m_SelectedUnitsList.Add(unit);
                    }
                }
            }
        }

        ClearCurrentStructure();

        SetUnitGroup();
    }

    private void MultiSelectionBox()
    {
        if (!m_SelectionImage.gameObject.activeInHierarchy)
        {
            m_SelectionImage.gameObject.SetActive(true);
        }

        float width = m_MousePosition.x - m_BoxStartPos.x;
        float height = m_MousePosition.y - m_BoxStartPos.y;

        m_SelectionImage.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        m_SelectionImage.anchoredPosition = m_BoxStartPos + new Vector2(width * 0.5f, height * 0.5f);

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (m_SelectionImage.gameObject.activeInHierarchy)
                m_SelectionImage.gameObject.SetActive(false);

            AddUnitsInSelectionBox();

            m_MultiSelect = false;
        }
    }

    private void SelectUnits(bool select)
    {
        foreach (var unit in m_SelectedUnitsList)
        {
            unit.TryGetComponent(out IUnit u);

            if (select)
            {
                u.ShouldSelect(select);
            }
            else u.ShouldSelect(select);
        }

        if (!select)
        {
            m_SelectedUnitsList.Clear();
        }
    }

    private void ClearUnitList()
    {
        SelectUnits(false);
        m_SelectedUnitsList.Clear();
    }

    private void ClearCurrentStructure()
    {
        // To prevent structure to be in selection mode 
        if (m_CurrentStructure != null)
        {
            m_CurrentStructure.ShouldSelect(false);
        }
    }

    private void SetUnitGroup()
    {
        var firstUnit = m_SelectedUnitsList[0];

        SelectUnits(true);
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

        Ray ray = m_Camera.ScreenPointToRay(m_MousePosition);

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