using System.Collections.Generic;
using System.Linq;
using Code.Enums;
using Code.Interfaces;
using Code.Managers.Data;
using Code.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Managers.Units
{
    public class GroupUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject getParent;

        private GameObject m_ParentObject;
        private readonly List<Texture> m_TextureList = new();
        private readonly List<GameObject> m_ListOfNewObjects = new();
        private DataManager m_Data;

        private void Awake()
        {
            m_Data = DataManager.Instance;
            m_Data.mouseInputs.OnUpdateUnitList += HandleUnitList; 
            m_Data.mouseInputs.OnDisableUnitImages += DisableUnitImages;

            m_ParentObject = getParent.transform.parent.gameObject;
        }

        private void HandleUnitList(List<GameObject> unitList)
        {
            m_TextureList.Clear();
            DisableUnitImages();
            
            foreach (var unit in unitList)
            {
                unit.TryGetComponent(out IUnit u);
                m_TextureList.Add(AllTextures.Instance.GetTexture(u.GetUnitTexture()));
            }

            ShowTextureList();
        }

        private void ShowTextureList()
        {
            int index = 0; // Current row
            int ySpacing = 0;
            const int xSpacing = 31;
            const int x = 35;

            foreach (var newObject in m_TextureList.Select(tex => CreateNewImage(tex, m_ParentObject.transform)))
            {
                SetPositions(index, xSpacing, ySpacing, newObject);
                index++;

                // After x count of units, create new row
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