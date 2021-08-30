using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Managers
{
    public class FlagManager : MonoBehaviour
    {
        private static FlagManager s_Instance;
        public static FlagManager Instance => s_Instance ??= FindObjectOfType<FlagManager>();

        [SerializeField] private GameObject m_FlagPrefab;

        public void SetFlagPosition(GameObject flag)
        {
            flag.SetActive(true);
            PlaceFlag(flag);
        }

        public GameObject InstantiateNewFlag()
        {
            var newFlag = Instantiate(m_FlagPrefab);
            newFlag.SetActive(false);
            
            return newFlag;
        }

        private void PlaceFlag(GameObject flag)
        {
            var ray = DataManager.Instance.mouseInputs.PlacementRay;

            // Might want to set default position if this happens ?
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