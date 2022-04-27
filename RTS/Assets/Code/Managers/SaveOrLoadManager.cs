using Code.HelperClasses;

namespace Code.Managers
{
    public enum SaveOrLoadState
    {
        Save,
        Load
    }
    
    public class SaveOrLoadManager : Singleton<SaveOrLoadManager>
    {
        public SaveOrLoadState GetCurrentSaveOrLoadState { get; private set; }

        private void Awake() => SetState(SaveOrLoadState.Load);

        private void Update()
        {
            if (GameManager.Instance.GetCurrentGameState == GameState.Running && GetCurrentSaveOrLoadState == SaveOrLoadState.Load)
            {
                SetState(SaveOrLoadState.Save);
            }
        }

        private void SetState(SaveOrLoadState state)
        {
            if (GetCurrentSaveOrLoadState == state) return;
            GetCurrentSaveOrLoadState = state;
        }
    }
}
