using System;
using Code.Enums;
using Code.HelperClasses;
using Code.TextureListByEnum;
using UnityEngine;

namespace Code.UI
{
    public class AllTextures : Singleton<AllTextures>
    {
        [SerializeField] private TextureList textures;

        public Texture GetTexture(TextureAssetType type)
        {
            var tex = type switch
            {
                TextureAssetType.Builder => textures[TextureAssetType.Builder],
                TextureAssetType.Solider => textures[TextureAssetType.Solider],
                TextureAssetType.Horse => textures[TextureAssetType.Horse],
                TextureAssetType.Castle => textures[TextureAssetType.Castle],
                TextureAssetType.Barracks => textures[TextureAssetType.Barracks],
                TextureAssetType.Flag => textures[TextureAssetType.Flag],
                TextureAssetType.Build => textures[TextureAssetType.Build],
                TextureAssetType.Back => textures[TextureAssetType.Back],
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            return tex;
        }
    }
}
