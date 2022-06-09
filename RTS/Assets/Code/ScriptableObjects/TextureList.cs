using System;
using System.Collections.Generic;
using Code.Tools.Enums;
using UnityEngine;

namespace Code.ScriptableObjects
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

        private readonly Dictionary<TextureAssetType, Texture> m_Dictionary = new();

        public Tex[] textures;

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
            foreach (var texture in textures)
            {
                m_Dictionary[texture.type] = texture.tex;
            }
        }
    }
}