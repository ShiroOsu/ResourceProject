using Code.Framework;
using Code.Framework.Enums;
using Code.Logger;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Managers
{
    public class BuildManager : MonoBehaviour
    {
        private static BuildManager s_Instance;
        public static BuildManager Instance => s_Instance ??= FindObjectOfType<BuildManager>();

        [SerializeField] private BlueprintByEnum m_Blueprints;
        private bool m_DisplayStructurePlacement = false;
        private GameObject m_CurrentBlueprintObject = null;
        private GameObject m_CurrentBuildObject = null;
        private StructureType m_CurrentStructureType = StructureType.None;


        private void Update()
        {
            HandleBuild();
        }

        public void InitBuild(StructureType type)
        {
            m_CurrentBlueprintObject = Instantiate(m_Blueprints[type]);
            Log.Message("Initialize Build", "build type: " + type);
            m_CurrentBuildObject = PoolManager.Instance.GetPooledStructure(type, false);

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

                if (Mouse.current.rightButton.wasPressedThisFrame)
                {
                    DisableBuildPlacement(true);
                }
            }
            else
            {
                m_CurrentBlueprintObject.SetActive(false);
            }
        }

    }
}
