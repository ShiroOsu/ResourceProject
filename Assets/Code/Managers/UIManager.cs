using UnityEngine;

public sealed class UIManager : MonoBehaviour
{
    private static UIManager s_Instance = null;
    public static UIManager Instance => s_Instance ??= FindObjectOfType<UIManager>();

    private UIManager() { }

    [Header("Units")]
    [SerializeField] private BuilderUIManager m_Builder;
    [SerializeField] private SoldierUIManager m_Soldier;

    [Header("Structures")]
    [SerializeField] private CastleUIManager m_Castle;
    [SerializeField] private BarracksUIManager m_Barracks;

    public void UnitSelected(UnitType type, bool select, GameObject unit)
    {
        switch (type)
        {
            case UnitType.Builder:
                BuilderUI(select, unit);
                break;
            case UnitType.Solider:
                SoliderUI(select, unit);
                break;
            default:
                break;
        }
    }

    public void StructureSelected(StructureType type, bool select, GameObject structure)
    {
        switch (type)
        {
            case StructureType.Castle:
                CastleUI(select, structure);
                break;
            case StructureType.Barracks:
                BarracksUI(select, structure);
                break;
            case StructureType.None:
                break;
        }
    }

    private void BuilderUI(bool activate, GameObject unit)
    {
        m_Builder.EnableMainUI(activate, unit);
    }

    private void SoliderUI(bool activate, GameObject unit)
    {
        m_Soldier.EnableMainUI(activate, unit);
    }

    private void CastleUI(bool activate, GameObject structure)
    {
        m_Castle.EnableMainUI(activate, structure);
    }

    private void BarracksUI(bool activate, GameObject structure)
    {
        m_Barracks.EnableMainUI(activate, structure);
    }
}
