using Code.Tools.HelperClasses;
using UnityEngine;

namespace Code.UI
{
    public class UIImage : Singleton<UIImage>
    {
        [SerializeField] private GameObject image;
        
        public void SetImage(GameObject gameObject)
        {
            image = gameObject;
            image.SetActive(true);
        }
    }
}