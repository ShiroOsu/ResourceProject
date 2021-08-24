using System;
using System.Collections.Generic;
using Code.Framework.Enums;
using UnityEngine;

namespace Code.Framework.TextureListByEnum
{
    [CreateAssetMenu(fileName = "TextureList", menuName = "ScriptableObjects/TextureList")]
    public class TextureList : ScriptableObject
    {
        [Serializable]
        public struct Tex
        {
            public Texture tex;
            public TextureAssetType type;
        }

        private readonly Dictionary<TextureAssetType, Texture> m_Dictionary =
            new Dictionary<TextureAssetType, Texture>();

        public Tex[] m_Textures;

        public Texture this[TextureAssetType type]
        {
            get
            {
                Init();
                return m_Dictionary[type];
            }
        }

        private void Init()
        {
            foreach (var texture in m_Textures)
            {
                m_Dictionary[texture.type] = texture.tex;
            }
        }
    }
}