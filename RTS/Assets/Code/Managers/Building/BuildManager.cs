using System;
using System.Collections.Generic;
using Code.Framework;
using Code.Framework.Enums;
using Code.Logger;
using Code.Managers.Building;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Managers
{
    public class BuildManager : MonoBehaviour
    {
        private static BuildManager s_Instance;
        public static BuildManager Instance => s_Instance ??= FindObjectOfType<BuildManager>();

        [SerializeField] private BlueprintByEnum m_Blueprints;
        [SerializeField] private Material m_BlueprintMaterialCanNotBuild;
        [SerializeField] private Material m_BlueprintMaterialCanBuild;

        private List<GameObject> m_BuildComponentsList = new List<GameObject>();
        private bool m_CanBuild = false;
        private bool m_DisplayStructurePlacement = false;
        private GameObject m_CurrentBlueprintObject = null;
        private GameObject m_CurrentBuildObject = null;
        private StructureType m_CurrentStructureType = StructureType.None;

        // Might do return m_CanBuild;
        private void CanBuild()
        {
            Log.Message("BuildManager.cs", "CanBuild: " + m_CanBuild); 

            foreach (var component in m_BuildComponentsList)
            {
                component.TryGetComponent(out MeshRenderer meshRenderer);
                meshRenderer.material = m_CanBuild ? m_BlueprintMaterialCanBuild : m_BlueprintMaterialCanNotBuild;
            }
        }

        private void Update()
        {
            HandleBuild();
        }

        public void InitBuild(StructureType type)
        {
            m_CurrentBlueprintObject = Instantiate(m_Blueprints[type]);
            Log.Message("Initialize Build", "build type: " + type);
            m_CurrentBuildObject = PoolManager.Instance.GetPooledStructure(type, false);

            m_CurrentBlueprintObject.TryGetComponent(out BuildComponents bc);
            
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

            if (deActivateBuild)
            {
                m_CurrentBuildObject.SetActive(true);
            }

            UIManager.Instance.StructureSelected(m_CurrentStructureType, false, m_CurrentBuildObject);
        }

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
                var groundPoint = hit.point;

                m_CurrentBlueprintObject.transform.position = m_CurrentBuildObject.transform.position = groundPoint;
                m_CurrentBlueprintObject.SetActive(true);

                // Logic for when can/can not build
                CanBuild();

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
            else
            {
                m_CurrentBlueprintObject.SetActive(false);
            }
        }

    }
}
