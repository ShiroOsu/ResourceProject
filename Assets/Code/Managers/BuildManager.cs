using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildManager : MonoBehaviour
{
    private static BuildManager s_Instance = null;
    public static BuildManager Instance => s_Instance ??= FindObjectOfType<BuildManager>();

    private bool m_DisplayStructurePlacement = false;
    private GameObject m_CurrentBuildObject = null;
    private StructureType m_CurrentStructureType = StructureType.None;

    [SerializeField] private List<GameObject> m_Structures = new List<GameObject>();

    private void Awake()
    {
        // Instantiate prefabs in m_Structures, then deactivate
        // (First element is null)
        for (int i = 1; i < m_Structures.Count; i++)
        {
            bool active = m_Structures[i].activeSelf;
            m_Structures[i].SetActive(false);
            GameObject preLoadedStructures = Instantiate(m_Structures[i]);
            m_Structures[i].SetActive(active);
            m_Structures[i] = preLoadedStructures;
        }
    }

    private void Update()
    {
        HandleBuild();
    }

    public void InitBuild(StructureType type)
    {
        m_CurrentBuildObject = m_Structures[(int)type];
        m_CurrentBuildObject.SetActive(true);
        m_CurrentStructureType = type;
        m_DisplayStructurePlacement = true;
    }

    private void DisableBuildPlacement(bool deActivateBuild)
    {
        m_CurrentBuildObject.SetActive(deActivateBuild);
        m_DisplayStructurePlacement = false;

        UIManager.Instance.StructureSelected(m_CurrentStructureType, false, m_CurrentBuildObject);
    }

    private void HandleBuild()
    {
        if (!m_DisplayStructurePlacement)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            DisableBuildPlacement(false);
            return;
        }

        Ray ray = DataManager.Instance.mouseInputs.PlacementRay;

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            var groundPoint = hit.point;

            m_CurrentBuildObject.transform.position = groundPoint;
            m_CurrentBuildObject.SetActive(true);

            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                DisableBuildPlacement(true);
            }
        }
        else
        {
            m_CurrentBuildObject.SetActive(false);
        }
    }

}