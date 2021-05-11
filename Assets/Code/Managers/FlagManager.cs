using UnityEngine.InputSystem;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    private static FlagManager s_Instance = null;
    public static FlagManager Instance => s_Instance ??= FindObjectOfType<FlagManager>();

    private GameObject m_Flag = null;
    private GameObject m_CurrentStructure = null;
    private StructureType m_CurrentStructureType = StructureType.None;
    private bool m_ShowFlagPlacement = false;

    private void Update()
    {
        if (!m_ShowFlagPlacement)
            return;

        PlaceFlag();

    }

    public GameObject SetSpawnFlag(GameObject currentStructure, StructureType type)
    {
        m_CurrentStructure = currentStructure;
        m_CurrentStructureType = type;

        m_ShowFlagPlacement = true;
        m_Flag = PoolManager.Instance.flagPool.Rent(true);

        return m_Flag;
    }

    private void PlaceFlag()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            m_ShowFlagPlacement = false;
            return;
        }

        Ray ray = DataManager.Instance.mouseInputs.PlacementRay;

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            var groundPoint = hit.point + new Vector3(0f, 1.5f, 0f);

            m_Flag.transform.position = groundPoint;

            if (Mouse.current.rightButton.isPressed)
            {
                m_Flag.transform.position = groundPoint;
                SetSpawnPosForCurrentStructure(m_CurrentStructureType, m_CurrentStructure, groundPoint);

                m_ShowFlagPlacement = false;
            }
        }
    }

    private void SetSpawnPosForCurrentStructure(StructureType type, GameObject structure, Vector3 pos)
    {
        switch (type)
        {
            case StructureType.None:
                break;
            case StructureType.Castle:
                structure.TryGetComponent(out Castle castle);
                castle.UnitSpawnPoint = pos;
                break;
            case StructureType.Barracks:
                structure.TryGetComponent(out Barracks barracks);
                barracks.UnitSpawnPoint = pos;
                break;
        }
    }
}