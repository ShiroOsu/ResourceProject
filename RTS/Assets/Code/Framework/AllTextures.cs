using System;
using Code.Framework.Enums;
using Code.Framework.TextureListByEnum;
using UnityEngine;

namespace Code.Framework
{
    public class AllTextures : MonoBehaviour
    {
        private static AllTextures s_Instance;
        public static AllTextures Instance => s_Instance ??= FindObjectOfType<AllTextures>();

        [SerializeField] private TextureList m_Textures;

        public Texture GetUnitTexture(UnitType type)
        {
            var unitTexture = type switch
            {
                UnitType.Builder => m_Textures[TextureAssetType.Builder],
                UnitType.Solider => m_Textures[TextureAssetType.Solider],
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            return unitTexture;
        }

        public Texture GetStructureTexture(StructureType type)
        {
            var structureTex = type switch
            {
                StructureType.Castle => m_Textures[TextureAssetType.Castle],
                StructureType.Barracks => m_Textures[TextureAssetType.Barracks],
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            return structureTex;
        }
    }
}
