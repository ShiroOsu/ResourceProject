using Code.Tools.Enums;
using TMPro;
using UnityEngine;

namespace Code.Resources
{
    public class ResourcePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text goldAmount;
        [SerializeField] private TMP_Text stoneAmount;
        // [SerializeField] private TMP_Text woodAmount;
        // [SerializeField] private TMP_Text foodAmount;
        // [SerializeField] private TMP_Text unitsAmount;

        private void Start()
        {
            PlayerResources.Instance.OnUpdateResourcePanel += OnUpdate;
            
            goldAmount.SetText(PlayerResources.Instance.GetResource(ResourceType.Gold));
            stoneAmount.SetText(PlayerResources.Instance.GetResource(ResourceType.Stone));
            // woodAmount.SetText(PlayerResources.Instance.GetResource(ResourceType.Wood));
            // foodAmount.SetText(PlayerResources.Instance.GetResource(ResourceType.Food));
            // unitsAmount.SetText(PlayerResources.Instance.GetResource(ResourceType.Units));
        }

        private void OnUpdate()
        {
            goldAmount.SetText(PlayerResources.Instance.GetResource(ResourceType.Gold));
            stoneAmount.SetText(PlayerResources.Instance.GetResource(ResourceType.Stone));
            // woodAmount.SetText(PlayerResources.Instance.GetResource(ResourceType.Wood));
            // foodAmount.SetText(PlayerResources.Instance.GetResource(ResourceType.Food));
            // unitsAmount.SetText(PlayerResources.Instance.GetResource(ResourceType.Units));
        }
    }
}
