using Code.Enums;
using Code.HelperClasses;
using Code.ScriptableObjects;
using UnityEngine;

namespace Code.UI
{
    public class AllTextures : Singleton<AllTextures>
    {
        [SerializeField] private TextureList textures;

        public Texture GetTexture(TextureAssetType type)
        {
            return textures[type];
        }
    }
}
