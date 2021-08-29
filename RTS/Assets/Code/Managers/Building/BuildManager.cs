using System.Collections.Generic;
using Code.Framework;
using Code.Framework.Enums;
using Code.Logger;
using Code.Player;
using Code.Player.Camera;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Managers.Building
{
    public class BuildManager : MonoBehaviour
    {
        private static BuildManager s_Instance;
        public static BuildManager Instance => s_Instance ??= FindObjectOfType<BuildManager>();

        [SerializeField] private BlueprintByEnum m_Blueprints;
        [SerializeField] private Material m_BlueprintMaterialCanNotBuild;
        [SerializeField] private Material m_BlueprintMaterialCanBuild;

        private CameraControls m_CameraControls;
        private MouseInputs m_MouseInputs;
        
        private List<GameObject> m_BuildComponentsList = new List<GameObject>();
        private bool m_CanBuild;
        private bool m_DisplayStructurePlacement;
        private GameObject m_CurrentBlueprintObject;
        private BuildComponents m_CurrentBlueprintBuildComponents;
        private GameObject m_CurrentBuildObject;
        private StructureType m_CurrentStructureType;

        private void Awake()
        {
            m_CameraControls = FindObjectOfType<CameraControls>();
            m_MouseInputs = DataManager.Instance.mouseInputs;
        }

        private bool CanBuild()
        {
            foreach (var component in m_BuildComponentsList)
            {
                component.TryGetComponent(out MeshRenderer meshRenderer);
                meshRenderer.material = m_CanBuild ? m_BlueprintMaterialCanBuild : m_BlueprintMaterialCanNotBuild;
            }

            return m_CanBuild;
        }

        private void Update()
        {
            HandleBuild();
        }

        public void InitBuild(StructureType type)
        {
            m_MouseInputs.IsBuilding = true;
            
            m_CurrentBlueprintObject = Instantiate(m_Blueprints[type]);
            Log.Message("Initialize Build", "build type: " + type);
            m_CurrentBuildObject = PoolManager.Instance.GetPooledStructure(type, false);

            m_CurrentBlueprintObject.TryGetComponent(out BuildComponents bc);
            m_CurrentBlueprintBuildComponents = bc;
            
            foreach (Transform child in bc.m_BuildComponents.transform)
            {
                m_BuildComponentsList.Add(child.gameObject);
            }

            m_CurrentStructureType = type;
            m_DisplayStructurePlacement = true;
        }

        private void DisableBuildPlacement(bool deActivateBuild)
        {
            m_CurrentBlueprintObject.SetActive(deActivateBuild);
            m_DisplayStructurePlacement = false;
            m_CameraControls.m_CanZoom = true;

            if (deActivateBuild)
            {
                m_CurrentBuildObject.transform.rotation = m_CurrentBlueprintObject.transform.rotation;
                m_CurrentBuildObject.SetActive(true);
            }

            UIManager.Instance.StructureSelected(m_CurrentStructureType, false, m_CurrentBuildObject);
            m_MouseInputs.IsBuilding = false;
        }

        private void RotateBuilding(Vector2 v)
        {
            m_CurrentBlueprintObject.transform.Rotate(Vector3.up, v.normalized.y * 20f);
        }

        // When building place blueprint first, do a TimeEvent
        // when TimeEvent.IsEventDone() == true, remove blueprint and place buildObject
        private void HandleBuild()
        {
            if (!m_DisplayStructurePlacement)
                return;

            if (Mouse.current.leftButton.isPressed)
            {
                Log.Message("BuildManager.cs", "DisableBuildPlacement by left click is this intended?");
                DisableBuildPlacement(false);
                return;
            }

            Ray ray = DataManager.Instance.mouseInputs.PlacementRay;

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                m_CameraControls.m_CanZoom = false;

                var groundPoint = hit.point;
                if (m_CurrentStructureType == StructureType.Castle)
                {
                    groundPoint.y += 5f;
                }

                m_CurrentBlueprintObject.transform.position = m_CurrentBuildObject.transform.position = groundPoint;
                m_CurrentBlueprintObject.SetActive(true);

                m_CanBuild = !m_CurrentBlueprintBuildComponents.m_InTrigger;

                var scroll = Mouse.current.scroll.ReadValue();

                RotateBuilding(scroll);
                
                if (CanBuild())
                {
                    if (Mouse.current.rightButton.wasPressedThisFrame)
                    {
                        DisableBuildPlacement(true);

                        if (m_CurrentBlueprintObject != null)
                        {
                            Destroy(m_CurrentBlueprintObject);
                            m_BuildComponentsList.Clear();
                        }
                    }
                }

                if (!CanBuild())
                {
                    if (Mouse.current.rightButton.wasPressedThisFrame)
                    {
                       Log.Error("BuildManager.cs", "Can not place building here!");
                    }               
                }
            }
            else
            {
                m_CurrentBlueprintObject.SetActive(false);
            }
        }
    }
}