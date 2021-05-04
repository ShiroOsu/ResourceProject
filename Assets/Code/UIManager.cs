using UnityEngine;

public sealed class UIManager : MonoBehaviour
{
    private static UIManager s_Instance = null;
    public static UIManager Instance => s_Instance ??= FindObjectOfType<UIManager>();

    [SerializeField] private GameObject m_TempObject;

    private UIManager() { }

    public void UnitSelected(UnitType type)
    {
        switch (type)
        {
            case UnitType.Builder:
                BuilderUI();
                break;
            case UnitType.Solider:
                SoliderUI();
                break;
            default:
                break;
        }
    }

    public void StructureSelected(StructureType type)
    {
        switch (type)
        {
            case StructureType.Castle:
                CastleUI();
                break;
            case StructureType.Barracks:
                BarracksUI();
                break;
            default:
                break;
        }
    }
    
    private void BuilderUI()
    {

    }

    private void SoliderUI()
    {

    }

    private void CastleUI()
    {
    }

    private void BarracksUI()
    {

    }
}