using Code.ScriptableObjects;
using Code.Tools.Enums;
using Code.Tools.HelperClasses;
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
