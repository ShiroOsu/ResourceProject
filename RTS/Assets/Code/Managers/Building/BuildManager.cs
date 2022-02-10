using System.Collections.Generic;
using Code.Framework;
using Code.Framework.Blueprint;
using Code.Framework.Enums;
using Code.Framework.ExtensionFolder;
using Code.Framework.Logger;
using Code.Player;
using Code.Player.Camera;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Managers.Building
{
    public class BuildManager : Singleton<BuildManager>
    {
        [SerializeField] private BlueprintByEnum blueprints;
        [SerializeField] private Material blueprintMaterialCanNotBuild;
        [SerializeField] private Material blueprintMaterialCanBuild;

        private CameraControls m_CameraControls;
        private MouseInputs m_MouseInputs;
        
        private readonly List<GameObject> m_BuildComponentsList = new();
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
                meshRenderer.material = m_CanBuild ? blueprintMaterialCanBuild : blueprintMaterialCanNotBuild;
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
            
            m_CurrentBlueprintObject = Instantiate(blueprints[type]);
            Log.Message("Initialize Build", "build type: " + type);
            m_CurrentBuildObject = PoolManager.Instance.GetPooledStructure(type, false);

            m_CurrentBlueprintObject.TryGetComponent(out BuildComponents bc);
            m_CurrentBlueprintBuildComponents = bc;
            
            foreach (Transform child in bc.buildComponents.transform)
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
            m_CameraControls.canZoom = true;

            if (deActivateBuild)
            {
                m_CurrentBuildObject.transform.rotation = m_CurrentBlueprintObject.transform.rotation;
                m_CurrentBuildObject.SetActive(true);
            }
            m_MouseInputs.IsBuilding = false;
        }

        // 20f is just a random amount of degrees it should rotate each time
        private void RotateBuilding(Vector2 scroll)
        {
            m_CurrentBlueprintObject.transform.Rotate(Vector3.up, scroll.normalized.y * 20f);
        }

        private void HandleBuild()
        {
            if (!m_DisplayStructurePlacement)
                return;

            if (Keyboard.current.escapeKey.isPressed)
            {
                Log.Message("BuildManager.cs", "DisableBuildPlacement by ESC key");
                DisableBuildPlacement(false);
                return;
            }

            var ray = DataManager.Instance.mouseInputs.PlacementRay;

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                m_CameraControls.canZoom = false;

                var groundPoint = hit.point;
                if (m_CurrentStructureType == StructureType.Castle)
                {
                    // Temp, stuff changed in 2021.2.0f1 update
                    // Because 0,0,0 in Castle transform is the middle and not on the ground
                    groundPoint.y += 5f;
                }

                m_CurrentBlueprintObject.transform.position = m_CurrentBuildObject.transform.position = groundPoint;
                m_CurrentBlueprintObject.SetActive(true);

                m_CanBuild = !m_CurrentBlueprintBuildComponents.inTrigger;

                var scroll = Mouse.current.scroll.ReadValue();

                RotateBuilding(scroll);
                
                if (CanBuild())
                {
                    if (Extensions.WasMousePressedThisFrame())
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
                    if (Extensions.WasMousePressedThisFrame())
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