using UnityEngine;
using UnityEngine.InputSystem;

public class Barracks : MonoBehaviour, IStructure
{
    private Vector3 m_UnitSpawnPoint;

    private GameObject m_Flag = null;
    private bool m_ShowFlagPlacement = false;

    private void Update()
    {
        if (m_ShowFlagPlacement)
        {
            PlaceFlag();
        }
    }

    public void OnFlagButton()
    {
        m_ShowFlagPlacement = true;
        m_Flag = PoolManager.Instance.flagPool.Rent(true);
    }

    private void PlaceFlag()
    {
        Ray ray = DataManager.Instance.mouseInputs.PlacementRay;

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            var groundPoint = hit.point + new Vector3(0f, 1.5f, 0f);

            m_Flag.transform.position = groundPoint;

            if (Mouse.current.rightButton.isPressed)
            {
                m_Flag.transform.position = groundPoint;
                m_UnitSpawnPoint = groundPoint;

                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    m_Flag.SetActive(false);
                }

                m_ShowFlagPlacement = false;
            }
        }
    }

    public void SpawnSoldier()
    {
        GameObject soldier = PoolManager.Instance.soldierPool.Rent(true);

        // This will position the soldier inside the Barracks
        soldier.transform.position = transform.position;

        soldier.TryGetComponent(out IUnit unit);

        if (m_UnitSpawnPoint != Vector3.zero)
        {
            unit.Move(m_UnitSpawnPoint);
        }
    }

    public void Destroy()
    {
    }

    public void ShouldSelect(bool select)
    {
        UIManager.Instance.StructureSelected(StructureType.Barracks, select, gameObject);
        if (m_Flag != null)
            m_Flag.SetActive(select);
    }

    public void Upgrade()
    {
    }
}