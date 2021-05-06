using UnityEngine;
using UnityEngine.UI;

public class BarracksUIManager : MonoBehaviour
{
    [SerializeField] private GameObject m_Image;
    [SerializeField] private GameObject m_UI;
    [SerializeField] private Button m_SpawnSoldier;

    private Barracks m_BarracksRef;

    public void EnableMainUI(bool active, GameObject structure)
    {
        m_BarracksRef = structure.GetComponent<Barracks>();

        m_SpawnSoldier.onClick.AddListener(m_BarracksRef.SpawnSoldier);

        m_Image.SetActive(active);
        m_UI.SetActive(active);
    }
}