using System.Collections.Generic;
using System.Linq;
using Code.Framework.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Managers.Units
{
    public class GroupUIManager : MonoBehaviour
    {
        [SerializeField] private Texture m_BuilderTex = null;
        [SerializeField] private Texture m_SoldierTex = null;
        [SerializeField] private GameObject m_GetParent = null;

        private GameObject m_ParentObject = null;
        private readonly List<Texture> m_TextureList = new List<Texture>();
        private readonly List<GameObject> m_ListOfNewObjects = new List<GameObject>();

        private int m_BuilderID;
        private int m_SoldierID;

        private void Awake()
        {
            DataManager.Instance.mouseInputs.OnUpdateUnitList += HandleUnitList;
            DataManager.Instance.mouseInputs.OnDisableUnitImages += DisableUnitImages;

            m_BuilderID = DataManager.Instance.unitData.builderID;
            m_SoldierID = DataManager.Instance.unitData.soldierID;

            m_ParentObject = m_GetParent.transform.parent.gameObject;

        }

        // ReSharper disable Unity.PerformanceAnalysis
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
            int index = 0;
            int ySpacing = 0;
            const int xSpacing = 30;

            foreach (var newObject in m_TextureList.Select(tex => CreateNewImage(tex, m_ParentObject.transform)))
            {
                SetPositions(index, xSpacing, ySpacing, newObject);
                index++;

                int yMod = index % 10;

                if (yMod == 0)
                {
                    ySpacing = 60;
                    index = 0;
                }

                m_ListOfNewObjects.Add(newObject);
            }
        }

        private void SetPositions(int index, int xSpacing, int ySpacing, GameObject newObject)
        {
            newObject.TryGetComponent(out RectTransform rect);
            rect.sizeDelta = new Vector2(50f, 50f);
            var sizeDelta = rect.sizeDelta;
        
            rect.localPosition = new Vector3(sizeDelta.x + index * xSpacing, -sizeDelta.y + -(ySpacing), 0f);
            rect.localRotation = new Quaternion(0f, 0f, 0f, 0f);
            rect.localScale = new Vector3(1f, 1f, 1f);
        }

        private GameObject CreateNewImage(Texture tex, Transform parentTransform)
        {
            var newObject = new GameObject();
            newObject.transform.SetParent(parentTransform);
            newObject.layer = 5; // UI layer
            newObject.AddComponent<RawImage>();
            newObject.TryGetComponent(out RawImage raw);
            raw.texture = tex;

            return newObject;
        }

        private void DisableUnitImages()
        {
            if (m_ListOfNewObjects.Count < 1)
                return;

            for (var i = m_ListOfNewObjects.Count - 1; i >= 0; i--)
            {
                var obj = m_ListOfNewObjects[i];
                m_ListOfNewObjects.RemoveAt(i);

                Destroy(obj);
            }
        }
    }
}