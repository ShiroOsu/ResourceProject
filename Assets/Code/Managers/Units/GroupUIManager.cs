using System.Collections.Generic;
using System.Linq;
using Code.Framework.Enums;
using Code.Framework.Interfaces;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Managers.Units
{
    public class GroupUIManager : MonoBehaviour
    {
        [SerializeField] private Texture m_BuilderTex;
        [SerializeField] private Texture m_SoldierTex;
        [SerializeField] private GameObject m_GetParent;

        private GameObject m_ParentObject;
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
            int index = 0; // Current row
            int ySpacing = 0;
            const int xSpacing = 30;
            const int x = 35;

            foreach (var newObject in m_TextureList.Select(tex => CreateNewImage(tex, m_ParentObject.transform)))
            {
                SetPositions(index, xSpacing, ySpacing, newObject);
                index++;

                // After x amount of units, create new row
                int yMod = index % x;

                if (yMod == 0)
                {
                    ySpacing += 60; // Amount to space for next row
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
        
            rect.localPosition = new Vector3(sizeDelta.x + index * xSpacing, -sizeDelta.y + -(ySpacing), 0f); // ?
            rect.localRotation = new Quaternion(0f, 0f, 0f, 0f);
            rect.localScale = new Vector3(1f, 1f, 1f);
        }

        private GameObject CreateNewImage(Texture tex, Transform parentTransform)
        {
            var newObject = new GameObject();
            newObject.transform.SetParent(parentTransform);
            newObject.layer = (int)UnityLayer.UI;
            
            var raw = newObject.AddComponent<RawImage>();
            raw.texture = tex;

            return newObject;
        }

        // Clear list of images to show and texture
        private void DisableUnitImages()
        {
            if (m_ListOfNewObjects.Count < 1) return;

            for (var i = m_ListOfNewObjects.Count - 1; i >= 0; i--)
            {
                // i = index at the end of list
                var obj = m_ListOfNewObjects[i];
                m_ListOfNewObjects.RemoveAt(i);
                
                Destroy(obj);
            }

            if (m_TextureList.Count < 1) return;

            for (int i = m_TextureList.Count - 1; i >= 0; i--)
            {
                //var tex = m_TextureList[i]; // destroy?
                m_TextureList.RemoveAt(i);
            }
        }
    }
}