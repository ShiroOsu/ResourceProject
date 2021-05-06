using UnityEngine;
using UnityEngine.UI;

public class CastleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject m_Image = null;
    [SerializeField] private GameObject m_Info = null;
    [SerializeField] private GameObject m_UI = null;

    [SerializeField] private Button m_FlagButton = null;
    [SerializeField] private Button m_SpawnBuilderButton = null;

    private Castle m_CastleRef;
    
    public void EnableMainUI(bool active, GameObject structure)
    {
        m_CastleRef = structure.GetComponent<Castle>();

        m_FlagButton.onClick.AddListener(m_CastleRef.OnFlagButton);
        m_SpawnBuilderButton.onClick.AddListener(m_CastleRef.OnSpawnBuilderButton);

        m_Image.SetActive(active);
        m_Info.SetActive(active);
        m_UI.SetActive(active);

        if (active is false)
        {
            m_FlagButton.onClick.RemoveAllListeners();
            m_SpawnBuilderButton.onClick.RemoveAllListeners();
        }
    }
}