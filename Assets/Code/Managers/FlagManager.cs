using UnityEngine.InputSystem;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    private static FlagManager s_Instance = null;
    public static FlagManager Instance => s_Instance ??= FindObjectOfType<FlagManager>();

    private GameObject m_Flag = null;

    public GameObject SetSpawnFlag()
    {
        m_Flag ??= PoolManager.Instance.flagPool.Rent(true);
        
        PlaceFlag();

        return m_Flag;
    }

    private void PlaceFlag()
    {
        Ray ray = DataManager.Instance.mouseInputs.PlacementRay;

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            var groundPoint = hit.point + new Vector3(0f, 1.5f, 0f);

            m_Flag.transform.position = groundPoint;

            if (Mouse.current.rightButton.isPressed)
            {
                m_Flag.transform.position = groundPoint;
            }
        }
    }
}