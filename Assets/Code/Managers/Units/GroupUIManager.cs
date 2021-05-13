using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupUIManager : MonoBehaviour
{
    [SerializeField] private Texture m_BuilderTex = null;
    [SerializeField] private Texture m_SoldierTex = null;
    [SerializeField] private RawImage m_RawImage = null;

    private GameObject m_ParentObject = null;
    private List<RawImage> m_RawImageList = new List<RawImage>();
    private List<Texture> m_TextureList = new List<Texture>();

    private int m_BuilderID;
    private int m_SoldierID;

    private void Awake()
    {
        DataManager.Instance.mouseInputs.OnUpdateUnitList += HandleUnitList;

        m_BuilderID = DataManager.Instance.unitData.builderID;
        m_SoldierID = DataManager.Instance.unitData.soldierID;

        m_ParentObject = m_RawImage.transform.parent.gameObject;

    }

    private void HandleUnitList(List<GameObject> unitList)
    {
        foreach (var unit in unitList)
        {
            unit.TryGetComponent(out IUnit u);

            if (u.GetUnitID() == m_BuilderID)
            {
                m_TextureList.Add(m_BuilderTex);
            }

            if (u.GetUnitID() == m_SoldierID)
            {
                m_TextureList.Add(m_SoldierTex);
            }
        }

        ShowTextureList();

    }

    private void ShowTextureList()
    {
        foreach (var tex in m_TextureList)
        {
            m_RawImage.texture = tex;
            m_RawImageList.Add(m_RawImage);
        }

        for (int i = 0; i < m_RawImageList.Count; i++)
        {
            var newGo = new GameObject();
            newGo.transform.SetParent(m_ParentObject.transform);

            newGo.AddComponent<RawImage>();
            newGo.TryGetComponent(out RawImage raw);

            raw.texture = m_RawImageList[i].texture;
            raw.uvRect = new UnityEngine.Rect(i * 50, 0, 50, 50);
        }
    }
}