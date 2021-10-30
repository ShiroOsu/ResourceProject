using System;
using System.Collections.Generic;
using Code.Framework.Interfaces;
using Code.Managers;
using Code.Managers.Building;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Code.Player
{
    public class MouseInputs : MonoBehaviour, MouseControls.IMouseActions
    {
        [Header("General")] [SerializeField] private UnityEngine.Camera m_Camera;
        [SerializeField] private Animator m_Animator;

        [Header("Multi Selection")] [SerializeField]
        private RectTransform m_SelectionImage;

        private Vector2 m_BoxStartPos;
        private bool m_MultiSelect;

        // Interaction
        private LayerMask m_UnitMask;
        private LayerMask m_StructureMask;
        private LayerMask m_GroundMask;

        private List<GameObject> m_SelectedUnitsList;

        //private GameObject m_CurrentUnit = null;
        private Vector2 m_MousePosition;

        // Public stuff
        public Ray PlacementRay => m_Camera.ScreenPointToRay(m_MousePosition);
        public bool IsBuilding { get; set; }
        public event Action<List<GameObject>> OnUpdateUnitList;
        public event Action OnDisableUnitImages;

        // Controls
        private MouseControls m_MouseControls;

        // Stop click animation timer
        private float m_Timer = 1f;

        private IStructure m_CurrentStructure;
        private static readonly int Clicked = Animator.StringToHash("Clicked");

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
                //TODO: Calling IsPointerOverGameObject will query UI state from last frame
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
            // TODO: Calling IsPointerOverGameObject will query UI state from last frame
            if (!EventSystem.current.IsPointerOverGameObject() && context.performed)
            {
                m_MultiSelect = true;
            }
        }

        private void ClickingOnUnitsAndStructures()
        {
            m_BoxStartPos = m_MousePosition;

            var ray = m_Camera.ScreenPointToRay(m_MousePosition);

            if (Physics.Raycast(ray, out var s_Hit, Mathf.Infinity, m_StructureMask))
            {
                // Click on blueprint prefab, return to avoid error on mouse callback
                if (s_Hit.transform.TryGetComponent(out BuildComponents _))
                {
                    return;
                }

                if (s_Hit.transform.parent.parent.TryGetComponent(out IStructure structure))
                {
                    ClickOnBuilding(structure);
                }
            }

            if (!Physics.Raycast(ray, out var u_Hit, Mathf.Infinity, m_UnitMask))
                return;

            if (u_Hit.transform.parent.TryGetComponent(out IUnit _))
            {
                ClickOnUnit(u_Hit.transform.parent.gameObject);
            }
        }

        private void ClickOnBuilding(IStructure structure)
        {
            if (structure == m_CurrentStructure)
                return;

            ClearCurrentStructure();
            ClearUnitList();

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

            var ray = m_Camera.ScreenPointToRay(m_MousePosition);
            Vector3 newPosition;

            // When building, Temp?
            if (IsBuilding)
                return;

            if (Physics.Raycast(ray, out _, Mathf.Infinity, m_StructureMask))
            {
                return;
            }

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, m_GroundMask))
            {
                newPosition = hit.point;
                PlayClickAnimation(true);
            }
            else
            {
                return;
            }

            foreach (var unit in m_SelectedUnitsList)
            {
                unit.TryGetComponent(out IUnit u);
                u.Move(newPosition);
            }
        }

        private void AddUnitsInSelectionBox()
        {
            Vector2 sizeDelta;

            var rectPosition = new Vector2
            (m_SelectionImage.anchoredPosition.x - m_SelectionImage.sizeDelta.x * 0.5f,
                m_SelectionImage.anchoredPosition.y - (sizeDelta = m_SelectionImage.sizeDelta).y * 0.5f);

            var rect = new Rect(rectPosition, sizeDelta);

            // Temp, because it finds ALL GameObjects in scene then loops through them,
            // then looking for the objects with the IUnit interface
            // Would be ideal to only needing to loop through a list that only contains selectable units
            var allUnits = FindObjectsOfType<GameObject>(false);

            // If multiSelecting when last action was multiSelect it will not clear lists 
            ClearUnitList();

            foreach (var unit in allUnits)
            {
                if (!unit.TryGetComponent(out IUnit _)) continue;

                var unitScreenPos = m_Camera.WorldToScreenPoint(unit.transform.position);

                if (!rect.Contains(unitScreenPos)) continue;

                // Limit determined by amount on units fits on the UI middle border (35x3)
                if (!m_SelectedUnitsList.Contains(unit) && m_SelectedUnitsList.Count < 105)
                {
                    m_SelectedUnitsList.Add(unit);
                }
            }

            // From structure selection to unit, un select current structure
            ClearCurrentStructure();

            // If there is only one unit in selected, single select that unit, do not multi select
            if (m_SelectedUnitsList.Count == 1)
            {
                SelectUnits(true);
                return;
            }

            SetUnitGroup(true);
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

                u.ShouldSelect(select);
            }

            if (select)
            {
                return;
            }

            m_SelectedUnitsList.Clear();
        }

        private void MultiSelectUnits(bool select)
        {
            foreach (var unit in m_SelectedUnitsList)
            {
                unit.TryGetComponent(out IUnit u);
                u.ActivateSelectionCircle(true);
            }

            var firstUnit = m_SelectedUnitsList[0];
            firstUnit.TryGetComponent(out IUnit iu);
            iu.ShouldSelect(true);

            if (select)
            {
                return;
            }

            m_SelectedUnitsList.Clear();
        }

        private void ClearUnitList()
        {
            SelectUnits(false);
            OnDisableUnitImages?.Invoke();
        }

        private void ClearCurrentStructure()
        {
            // To prevent structure to be in selection mode 
            m_CurrentStructure?.ShouldSelect(false);
            m_CurrentStructure = null;
        }

        private void SetUnitGroup(bool select)
        {
            if (m_SelectedUnitsList.Count < 1)
                return;

            OnUpdateUnitList?.Invoke(m_SelectedUnitsList);
            MultiSelectUnits(select);
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
                m_Animator.gameObject.SetActive(false);
                m_Animator.SetBool(Clicked, false);
                return;
            }

            var ray = m_Camera.ScreenPointToRay(m_MousePosition);

            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, m_GroundMask))
                return;

            if (!hit.transform.TryGetComponent(out Terrain _))
                return;

            m_Animator.gameObject.transform.position = hit.point + new Vector3(0f, 0.1f, 0f);
            m_Animator.gameObject.transform.rotation = Quaternion.LookRotation(-hit.normal);
            m_Animator.gameObject.SetActive(true);
            m_Animator.SetBool(Clicked, true);
        }
    }
}