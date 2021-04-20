using UnityEngine;
using UnityEngine.InputSystem;

public class Castle : MonoBehaviour, IStructure
{
    [SerializeField] private GameObject m_MouseControls = null;
    [SerializeField] private CastleStats m_CastleData = null;

    [Header("UI")]
    [SerializeField] private GameObject m_CastleImage = null;
    [SerializeField] private GameObject m_CastleUI = null;
    [SerializeField] private GameObject m_CastleInfo = null;

    // Spawn location for builders
    private Vector3 m_UnitSpawnPoint;

    private GameObject m_Flag = null;
    private MouseInputs m_MouseInputs = null;

    private bool ShowFlagPlacement = false;

    private void Start()
    {
        if (!m_CastleData) { m_CastleData = ScriptableObject.CreateInstance<CastleStats>(); }

        m_MouseInputs = m_MouseControls.GetComponent<MouseInputs>();

        m_Flag = ReferenceHolder.Instance.flagPool.Rent(false);
    }

    private void Update()
    {
        if (ShowFlagPlacement)
        {
            PlaceFlag();
        }
    }

    public void OnFlagButton()
    {
        ShowFlagPlacement = true;
        m_Flag.SetActive(true);
    }

    public void OnSpawnBuilderButton()
    {
        SpawnBuilder();
    }

    // m_UnitSpawnPoint (for flag) Not working for more than one Castle structure
    private void PlaceFlag()
    {
        Ray ray = m_MouseInputs.PlacementRay;

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            var groundPoint = hit.point + new Vector3(0f, 1.5f, 0f);

            m_Flag.transform.position = groundPoint;

            if (Mouse.current.rightButton.isPressed || Mouse.current.leftButton.isPressed)
            {
                m_Flag.transform.position = groundPoint;
                m_UnitSpawnPoint = groundPoint;

                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    m_Flag.SetActive(false);
                }

                ShowFlagPlacement = false;
            }
        }
    }

    public void Destroy()
    {
        Destroy();
    }

    public void Unselect()
    {
        EnableUIStuff(false);
    }

    public void Selected()
    {
        CastleUI();
    }

    // This is instant spawning, but I want to implement a timer 'progress bar' later
    // for showing how long it takes to spawn a builder
    private void SpawnBuilder()
    {
        GameObject builder = ReferenceHolder.Instance.builderPool.Rent(true);

        // This will position the builder inside the Castle
        builder.transform.position = transform.position;

        builder.TryGetComponent(out IUnit unit);

        if (m_UnitSpawnPoint != Vector3.zero)
        {
            unit.Move(m_UnitSpawnPoint);
        }
    }

    public void Upgrade()
    {
        Debug.Log(transform.name + " upgrade");
    }

    // Buttons in the left corner of UI, what you can do (in a 5x3 pattern)
    // ex. Spawn builder, Upgrade Castle, Upgrade other stuff etc...
    private void CastleUI()
    {
        EnableUIStuff(true);
    }

    private void EnableUIStuff(bool active)
    {
        m_CastleImage.SetActive(active);
        m_CastleUI.SetActive(active);
        m_CastleInfo.SetActive(active);

        if (m_UnitSpawnPoint != Vector3.zero)
        {
            m_Flag.SetActive(active);
        }
    }
}
