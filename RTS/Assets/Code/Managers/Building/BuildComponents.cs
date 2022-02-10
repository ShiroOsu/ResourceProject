using Code.Framework.Tools;
using UnityEngine;

namespace Code.Managers.Building
{
    public class BuildComponents : MonoBehaviour
    {
        public GameObject buildComponents;
        public CustomSizer3D buildingBounds;
        public BoxCollider boxCollider;
        [HideInInspector] public bool inTrigger;

        private void Awake()
        {
            SetupColliderBounds();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out TerrainCollider _))
            {
                inTrigger = true;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent(out TerrainCollider _))
            {
                inTrigger = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out TerrainCollider _))
            {
                inTrigger = false;
            }
        }

        private void SetupColliderBounds()
        {
            boxCollider.center = buildingBounds.centerOfArea.position;

            if (transform.localScale.x > 1f
            || transform.localScale.y > 1f
            || transform.localScale.z > 1f)
            {
                boxCollider.size = buildingBounds.GetSize(transform.lossyScale);
            }
            else
            {
                boxCollider.size = buildingBounds.GetSize();
            }
        }
    }
}
