using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour, PlayerControls.IPlayerActions
{
    [Header("General")]
    public PlayerData m_Data = null;
    public Camera m_Camera = null;

    [Header("MultiSelection")]
    public float m_HoldTime = 0.05f;
    public RectTransform m_SelectionImage;
    private Vector2 m_BoxStartPos;

    private LayerMask m_InteractionMask;
    private List<GameObject> m_SelectedUnitsList = null;
    private Vector2 mousePosition;
    private PlayerControls m_PlayerControls;

    [Header("Click Animator")]
    public Animator m_Animator;
    private float m_Timer = 1f;

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

        if (m_Animator.gameObject.activeSelf)
        {
            StopClickAnimation();
        }
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
            if (!hit.transform.parent)
            {
                SelectUnits(false);
                return;
            }

            if (hit.transform.parent.GetComponent<IUnit>() != null)
            {
                ClickOnUnit(hit.transform.parent.gameObject);
            }

            if (hit.transform.TryGetComponent(out IStructure structure))
            {
                ClickOnBuilding(structure);
            }
        }
    }

    private void ClickOnBuilding(IStructure structure)
    {
        structure.Selected();
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

    public void OnRightMouse(InputAction.CallbackContext context)
    {
        Ray ray = m_Camera.ScreenPointToRay(mousePosition);
        Vector3 newPosition;

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, m_InteractionMask))
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
        // This is called also OnLeftMouse when it should not
        // Should only check released if LeftMouse was holding down
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

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, m_InteractionMask))
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