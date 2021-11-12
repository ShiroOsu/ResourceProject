using System;
using Code.Framework.Enums;
using Code.Framework.TextureListByEnum;
using UnityEngine;

namespace Code.Framework.UI
{
    public class AllTextures : Singleton<AllTextures>
    {
        [SerializeField] private TextureList m_Textures;

        public Texture GetTexture(TextureAssetType type)
        {
            var tex = type switch
            {
                TextureAssetType.Builder => m_Textures[TextureAssetType.Builder],
                TextureAssetType.Solider => m_Textures[TextureAssetType.Solider],
                TextureAssetType.Horse => m_Textures[TextureAssetType.Horse],
                TextureAssetType.Castle => m_Textures[TextureAssetType.Castle],
                TextureAssetType.Barracks => m_Textures[TextureAssetType.Barracks],
                TextureAssetType.Flag => m_Textures[TextureAssetType.Flag],
                TextureAssetType.Build => m_Textures[TextureAssetType.Build],
                TextureAssetType.Back => m_Textures[TextureAssetType.Back],
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            return tex;
        }
    }
}
