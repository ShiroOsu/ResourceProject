using UnityEngine;

public class Barracks : MonoBehaviour, IStructure
{
    public Vector3 UnitSpawnPoint { get; set; }
    private GameObject m_Flag = null;

    public void OnFlagButton()
    {
        m_Flag = FlagManager.Instance.SetSpawnFlag(gameObject, StructureType.Barracks);
    }

    public void SpawnSoldier()
    {
        SpawnManager.Instance.SpawnUnit(UnitType.Solider, gameObject.transform.position, UnitSpawnPoint);
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
