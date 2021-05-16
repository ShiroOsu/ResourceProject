using Code.Structures.Castle;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Managers.Structures
{
    public class CastleUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_Image = null;
        [SerializeField] private GameObject m_Info = null;
        [SerializeField] private GameObject m_UI = null;

        [SerializeField] private Button m_SpawnBuilderButton = null;

        private Castle m_CastleRef;

        public void EnableMainUI(bool active, GameObject structure)
        {
            m_CastleRef = structure.GetComponent<Castle>();

            m_SpawnBuilderButton.onClick.AddListener(m_CastleRef.OnSpawnBuilderButton);

            m_Image.SetActive(active);
            m_Info.SetActive(active);
            m_UI.SetActive(active);

            if (!active)
            {
                m_SpawnBuilderButton.onClick.RemoveAllListeners();
            }
        }
    }
}
