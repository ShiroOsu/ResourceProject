using System;
using System.Collections.Generic;
using Code.Interfaces;
using Code.Managers;
using Code.Managers.Building;
using Code.Tools.Enums;
using Code.Tools.HelperClasses;
using Code.UI;
using Code.Units.Builder;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Code.Player
{
    public class MouseInputs : MonoBehaviour, MouseControls.IMouseActions
    {
        [Header("General")] [SerializeField] private new UnityEngine.Camera camera;
        [SerializeField] private Animator animator;

        [Header("Multi Selection")]
        [SerializeField] private GameObject selectionGameObject;
        [SerializeField] private RectTransform selectionImage;

        private Vector2 m_BoxStartPos;
        private bool m_MultiSelect;
        private bool m_SetBoxStartPos;

        // Interaction
        private LayerMask m_UnitMask;
        private LayerMask m_StructureMask;
        private LayerMask m_ResourceMask;
        private LayerMask m_GroundMask;

        private Vector2 m_MousePosition;

        // Public stuff
        public Ray PlacementRay => camera.ScreenPointToRay(m_MousePosition);
        public List<GameObject> SelectedUnitsList { get; private set; }

        public bool IsBuilding { get; set; }
        public event Action<List<GameObject>> OnUpdateUnitList;
        public event Action OnDisableUnitImages;
        
        // Controls
        private MouseControls m_MouseControls;

        // Stop click animation timer
        private float m_Timer = 1f;

        private IStructure m_CurrentStructure;
        public IResource CurrentResource;
        private static readonly int Clicked = Animator.StringToHash("Clicked");

        private DataManager m_Data;
        
        private void Awake()
        {
            UpdateManager.Instance.OnUpdate += OnUpdate;
            
            m_Data = DataManager.Instance;
            SelectedUnitsList = new List<GameObject>();

            m_UnitMask = m_Data.mouseData.unitMask;
            m_StructureMask = m_Data.mouseData.structureMask;
            m_ResourceMask = m_Data.mouseData.resourceMask;
            m_GroundMask = m_Data.mouseData.groundMask;

            m_MouseControls = new MouseControls();
            m_MouseControls.Mouse.SetCallbacks(this);
            m_SetBoxStartPos = true;
        }

        private void OnUpdate()
        {
            m_MousePosition = Mouse.current.position.ReadValue();
            CheckMultiSelect();

            if (animator.gameObject.activeSelf)
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
                {
                    ClickingOnUnitsAndStructures();
                }
            }
        }

        public void OnRightMouse(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                MovingSelectedUnits();
            }
        }

        public void OnLeftMouseButtonHold(InputAction.CallbackContext context)
        {
        }

        // OnLeftMouseButtonHold, temp
        private void CheckMultiSelect()
        {
            if (!EventSystem.current.IsPointerOverGameObject() && Extensions.WasLeftMousePressed() && KeyCode.LeftShift.IsKeyPressing())
            {
                SetSelectionBoxStartPos();
                m_MultiSelect = true;
            }
        }

        private void ClickingOnUnitsAndStructures()
        {
            var ray = camera.ScreenPointToRay(m_MousePosition);

            if (Physics.Raycast(ray, out var sHit, Mathf.Infinity, m_StructureMask))
            {
                // Click on blueprint prefab, return to avoid error on mouse callback
                if (sHit.transform.TryGetComponent(out BuildComponents _))
                {
                    return;
                }

                if (sHit.transform.parent.parent.TryGetComponent(out IStructure structure))
                {
                    ClickOnBuilding(structure);
                }
            }

            if (Physics.Raycast(ray, out var rHit, Mathf.Infinity, m_ResourceMask))
            {
                if (rHit.transform.parent.TryGetComponent(out IResource resource))
                {
                    ClickOnResource(resource);
                }
            }

            if (!Physics.Raycast(ray, out var uHit, Mathf.Infinity, m_UnitMask))
                return;

            if (uHit.transform.parent.TryGetComponent(out IUnit _))
            {
                ClickOnUnit(uHit.transform.parent.gameObject);
            }
        }

        private void ClickOnBuilding(IStructure structure)
        {
            if (structure == m_CurrentStructure)
                return;

            ClearCurrentResource();
            ClearCurrentStructure();
            ClearUnitList();

            SelectUnits(false);

            m_CurrentStructure = structure;
            m_CurrentStructure.ShouldSelect(true);
        }

        private void ClickOnResource(IResource resource)
        {
            if (resource == CurrentResource) return;
            
            ClearUnitList();
            ClearCurrentStructure();
            ClearCurrentResource();
            
            SelectUnits(false);

            CurrentResource = resource;
            CurrentResource.ShouldSelect(true);
        }

        private void ClickOnUnit(GameObject unit)
        {
            ClearCurrentStructure();
            ClearCurrentResource();
            ClearUnitList();

            SelectedUnitsList.Add(unit);
            SelectUnits(true);
        }

        private void MovingSelectedUnits()
        {
            if (SelectedUnitsList.Count < 1)
                return;

            var ray = camera.ScreenPointToRay(m_MousePosition);

            // When building, Temp?
            if (IsBuilding)
                return;

            if (Physics.Raycast(ray, out _, Mathf.Infinity, m_StructureMask))
            {
                return;
            }

            Vector3 newPosition;
            
            if (Physics.Raycast(ray, out var rHit, Mathf.Infinity, m_ResourceMask))
            {
                newPosition = rHit.point;
                var resource = rHit.transform.parent.gameObject.ExGetComponent<IResource>();

                foreach (var unit in SelectedUnitsList)
                {
                    unit.TryGetComponent(out IUnit u);
                    if (u.GetUnitType() != UnitType.Builder)
                        continue;
                    
                    var builder = u.GetUnitObject().ExGetComponent<BuilderUnit>();
                    builder.MoveToResource(newPosition, resource);
                }

                ClearUnitList();
                resource.ShouldSelect(true);
                CurrentResource = resource;

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

            foreach (var unit in SelectedUnitsList)
            {
                unit.TryGetComponent(out IUnit u);
                u.Move(newPosition);
            }
        }

        private void AddUnitsInSelectionBox()
        {
            Vector2 sizeDelta;

            var rectPosition = new Vector2
            (selectionImage.anchoredPosition.x - selectionImage.sizeDelta.x * 0.5f,
                selectionImage.anchoredPosition.y - (sizeDelta = selectionImage.sizeDelta).y * 0.5f);

            var rect = new Rect(rectPosition, sizeDelta);

            // Temp, because it finds ALL GameObjects in scene then loops through them,
            // then looking for the objects with the IUnit interface
            // Would be ideal to only needing to loop through a list that only contains selectable units
            var allUnits = FindObjectsOfType<GameObject>(false);

            // If multiSelecting was last action it will not clear lists 
            ClearUnitList();

            foreach (var unit in allUnits)
            {
                if (!unit.TryGetComponent(out IUnit _)) continue;

                var unitScreenPos = camera.WorldToScreenPoint(unit.transform.position);

                if (!rect.Contains(unitScreenPos)) continue;

                // Limit determined by amount on units fits on the UI middle border (35x3)
                if (!SelectedUnitsList.Contains(unit) && SelectedUnitsList.Count < 105)
                {
                    SelectedUnitsList.Add(unit);
                }
            }

            // From structure selection to unit, un select current structure/resource
            ClearCurrentStructure();
            ClearCurrentResource();

            // If there is only one unit in selected, single select that unit, do not multi select
            if (SelectedUnitsList.Count == 1)
            {
                SelectUnits(true);
                return;
            }

            SetUnitGroup(true);
        }

        private void MultiSelectionBox()
        {
            if (!selectionGameObject.activeInHierarchy)
            {
                selectionGameObject.SetActive(true);
            }

            var width = m_MousePosition.x - m_BoxStartPos.x;
            var height = m_MousePosition.y - m_BoxStartPos.y;

            selectionImage.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
            selectionImage.anchoredPosition = m_BoxStartPos + new Vector2(width * 0.5f, height * 0.5f);

            if (Extensions.WasLeftMouseReleased())
            {
                selectionGameObject.SetActive(false);

                AddUnitsInSelectionBox();

                m_MultiSelect = false;
                m_SetBoxStartPos = true;
            }
        }

        private void SelectUnits(bool select)
        {
            foreach (var unit in SelectedUnitsList)
            {
                unit.TryGetComponent(out IUnit u);

                u.ShouldSelect(select);
            }

            if (select)
            {
                return;
            }

            SelectedUnitsList.Clear();
        }

        private void MultiSelectUnits(bool select)
        {
            foreach (var unit in SelectedUnitsList)
            {
                unit.TryGetComponent(out IUnit u);
                u.ActivateSelectionCircle(true);
            }

            var firstUnit = SelectedUnitsList[0];
            firstUnit.TryGetComponent(out IUnit u1);
            UIImage.Instance.SetImage(u1.GetUnitImage());

            if (select)
            {
                return;
            }

            SelectedUnitsList.Clear();
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

        private void ClearCurrentResource()
        {
            CurrentResource?.ShouldSelect(false);
            CurrentResource = null;
        }

        private void SetUnitGroup(bool select)
        {
            if (SelectedUnitsList.Count < 1)
                return;

            OnUpdateUnitList?.Invoke(SelectedUnitsList);
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

        private void SetSelectionBoxStartPos()
        {
            if (m_SetBoxStartPos)
            {
                m_BoxStartPos = m_MousePosition;
                m_SetBoxStartPos = false;
            }
        }
        
        // Animation Extension Class ? 
        private void PlayClickAnimation(bool active)
        {
            if (!active)
            {
                animator.gameObject.SetActive(false);
                animator.SetBool(Clicked, false);
                return;
            }

            var ray = camera.ScreenPointToRay(m_MousePosition);

            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, m_GroundMask))
                return;

            if (!hit.transform.TryGetComponent(out Terrain _))
                return;

            animator.gameObject.transform.position = hit.point + new Vector3(0f, 0.1f, 0f);
            animator.gameObject.transform.rotation = Quaternion.LookRotation(-hit.normal);
            animator.gameObject.SetActive(true);
            animator.SetBool(Clicked, true);
        }
    }
}