using UnityEngine;
using UnityEngine.InputSystem;

public class Castle : MonoBehaviour, IStructure
{
    // Spawn location for builders
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

    public void OnSpawnBuilderButton()
    {
        SpawnBuilder();
    }

    private void PlaceFlag()
    {
        Ray ray = DataManager.Instance.mouseInputs.PlacementRay;

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            var groundPoint = hit.point + new Vector3(0f, 1.5f, 0f);

            m_Flag.transform.position = groundPoint;

            if (Mouse.current.rightButton.isPressed || Mouse.current.leftButton.isPressed)
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

    public void Destroy()
    {
        Destroy();
    }

    // This is instant spawning, but I want to implement a timer 'progress bar' later
    // for showing how long it takes to spawn a builder
    private void SpawnBuilder()
    {
        GameObject builder = PoolManager.Instance.builderPool.Rent(true);
        //if (builder)
        //{
        //    builder = ReferenceHolder.Instance.builderPool.Rent(true);
        //}

        // This will position the builder inside the Castle
        builder.transform.position = transform.position;

        builder.TryGetComponent(out IUnit unit);

        if (m_UnitSpawnPoint != Vector3.zero)
        {
            unit.Move(m_UnitSpawnPoint);
        }
    }

    public void Upgrade()
    {
        Debug.Log(transform.name + " upgrade");
    }

    public void ShouldSelect(bool select)
    {
        UIManager.Instance.StructureSelected(StructureType.Castle, select, gameObject);
    }
}