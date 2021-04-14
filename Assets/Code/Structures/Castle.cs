using UnityEngine;
using UnityEngine.InputSystem;

public class Castle : MonoBehaviour, IStructure
{
    [SerializeField] private GameManager m_GameManager = null;
    [SerializeField] private GameObject m_MouseControls = null;
    
    [Header("Image in UI")]
    [SerializeField] private GameObject m_CastleImage = null;
    [SerializeField] private GameObject m_CastleUI = null;

    // Spawn location for builders
    private Vector3 m_UnitSpawnPoint;

    private GameObject m_Flag = null;
    private MouseInputs m_MouseInputs = null;

    private void Start()
    {
        m_MouseInputs = m_MouseControls.GetComponent<MouseInputs>();

        // If possible to build castles, when building make sure it has the game manager,
        // if manager has the builder objectPool
        if (!m_GameManager) { m_GameManager = FindObjectOfType<GameManager>(); }

        m_Flag = m_GameManager.flagPool.Rent(false);
    }

    private void OnFlagButtonRightClick()
    {
        m_UnitSpawnPoint = m_MouseInputs.SetStructureUnitSpawnFlag(m_Flag);
    }

    public void OnFlagButtonLeftClick()
    {
        // needs function to actively move a Flag before placed

        Debug.Log("OnFlagButtonLeftClick");

        //if (Mouse.current.rightButton.isPressed)
        //{
        //    OnFlagButtonRightClick();
        //}
    }

    public void Destroy()
    {
        // Temp
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
        GameObject builder = m_GameManager.builderPool.Rent(true);

        builder.transform.position = transform.position; // This will position the builder inside the Castle
        
        builder.TryGetComponent(out IUnit unit);
        unit.Move(m_UnitSpawnPoint);
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
    }
}
