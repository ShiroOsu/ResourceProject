using Code.HelperClasses;
using Code.Managers.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Managers
{
    public class FlagManager : Singleton<FlagManager>
    {
        [SerializeField] private GameObject flagPrefab;

        public void SetFlagPosition(GameObject flag)
        {
            flag.SetActive(true);
            PlaceFlag(flag);
        }

        public GameObject InstantiateNewFlag()
        {
            var newFlag = Instantiate(flagPrefab);
            newFlag.SetActive(false);
            
            return newFlag;
        }

        private void PlaceFlag(GameObject flag)
        {
            var ray = DataManager.Instance.mouseInputs.PlacementRay;

            // Might want to set default position if this happens ? object allocation params layerNames
            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
                return;
            
            var groundPoint = hit.point + new Vector3(0f, 1.5f, 0f);

            flag.transform.position = groundPoint;

            if (Mouse.current.rightButton.isPressed)
            {
                flag.transform.position = groundPoint;
            }
        }
    }
}