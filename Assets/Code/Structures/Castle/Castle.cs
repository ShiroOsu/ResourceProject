using UnityEngine;

public class Castle : MonoBehaviour, IStructure
{
    // Spawn location for builders
    public Vector3 UnitSpawnPoint { get; set; }
    private GameObject m_Flag = null;

    public void OnFlagButton()
    {
        m_Flag = FlagManager.Instance.SetSpawnFlag(gameObject, StructureType.Castle);
    }

    public void OnSpawnBuilderButton()
    {
        SpawnManager.Instance.SpawnUnit(UnitType.Builder, gameObject.transform.position, UnitSpawnPoint);
    }

    public void Destroy()
    {
        Destroy();
    }

    public void Upgrade()
    {
        Debug.Log(transform.name + " upgrade");
    }

    public void ShouldSelect(bool select)
    {
        UIManager.Instance.StructureSelected(StructureType.Castle, select, gameObject);

        if (m_Flag != null)
            m_Flag.SetActive(select);
    }
}
