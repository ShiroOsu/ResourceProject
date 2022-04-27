using UnityEngine;

namespace Code.Scenes.LoadingScene
{
    public class LoadingSceneGameObject : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;

        private void Awake()
        {
            canvas.worldCamera = UnityEngine.Camera.current;
        }
    }
}