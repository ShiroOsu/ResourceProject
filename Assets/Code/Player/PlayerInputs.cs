using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour, PlayerControls.IPlayerActions
{
    [Header("General")]
    public PlayerData m_Data = null;
    public Camera m_Camera = null;
    private PlayerControls m_PlayerControls;

    // Raycast temp holders
    private LayerMask m_InteractionMask;

    // Temp unit stuff
    private List<GameObject> m_Units = null;
    private Vector2 mousePosition;

    // MultiSelectionBox
    private Vector2 m_BoxStartPos;
    private Vector2 m_BoxEndPos;

    [Header("MultiSelectionBox Texture")]
    public Texture m_Image;
    public RectTransform m_SquareImage;
    private LayerMask m_MultiSelectionMask;

    // holding time for multiSelection box
    private float m_HoldTime = 1f;

    private void Awake()
    {
        if (!m_Data)
        {
            m_Data = ScriptableObject.CreateInstance<PlayerData>();
        }

        m_Units = new List<GameObject>();

        m_InteractionMask = m_Data.interactionLayer;
        m_MultiSelectionMask = m_Data.multiSelectionLayer;

        m_PlayerControls = new PlayerControls();
        m_PlayerControls.Player.SetCallbacks(this);
    }

    private void Update()
    {
        IsLMBHoldingDown();
    }

    #region Enable PlayerControls
    private void OnEnable()
    {
        m_PlayerControls.Enable();
    }

    private void OnDisable()
    {
        m_PlayerControls.Disable();
    }
    #endregion

    public void OnLeftMouse(InputAction.CallbackContext context)
    {
        mousePosition = Mouse.current.position.ReadValue();

        // 'only' want to check this if clicked
        Ray ray = m_Camera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, m_InteractionMask))
        {
            Debug.Log("Clicked on " + hit.transform.name + " (Left Click)");

            // Temp, because this only check units, but what if we click on a building?
            // Interface or Layer to be able to click on it?
            // Also hold down left click to select multiple units by 'drawing a box'.
            if (hit.transform.GetComponent<IUnit>() != null)
            {
                m_Units.Add(hit.transform.gameObject);
            }
            else { m_Units.Clear(); } // Temp
        }
    }

    public void OnRightMouse(InputAction.CallbackContext context)
    {
        mousePosition = Mouse.current.position.ReadValue();

        Ray ray = m_Camera.ScreenPointToRay(mousePosition);
        Vector3 newPosition;

        // Temp
        if (Physics.Raycast(ray, out RaycastHit hit, m_InteractionMask))
        {
            // Check if new position is 'available'
            // Aka you try to move units to a place they can not reach
            // Make them not move? or something
            if (true)
            {

            }

            newPosition = hit.point;
            Debug.Log(newPosition + " (Right Click)");
        }
        else { return; }

        if (context.performed)
        {
            if (m_Units.Count < 1)
                return;
           
            foreach (var unit in m_Units)
            {
                unit.GetComponent<IUnit>()?.Move(newPosition);
            }
        }
    }

    private void IsLMBHoldingDown()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            m_HoldTime -= Time.deltaTime;

            if (m_HoldTime < 0f)
            {
                MultiSelectionBox();
            }
        }
        else
        {
            // When releasing LMB SquareImage might still be active when it should not be
            if (m_SquareImage.gameObject.activeInHierarchy)
            {
                m_SquareImage.gameObject.SetActive(false);
            }

            m_HoldTime = 1f;
        }
    }

    // Don't like the name of the function
    private void MultiSelectionBox()
    {
        var LMB = Mouse.current.leftButton;
        var mousePosition = Mouse.current.position.ReadValue();

        if (LMB.isPressed)
        {
            m_SquareImage.gameObject.SetActive(true);
        }
        else { m_SquareImage.gameObject.SetActive(false); }

        if (LMB.isPressed)
        {
            Ray ray = m_Camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, m_MultiSelectionMask))
            {
                //Debug.Log(hit.point);
            }

            //if (Physics.Raycast(m_Camera.ScreenPointToRay(mousePosition), out RaycastHit hit))
            //{
            //    m_BoxStartPos = new Vector2(hit.point.x, hit.point.z);
            //}
        }
        else
        {
            //if (Physics.Raycast(m_Camera.ScreenPointToRay(mousePosition), out RaycastHit hit))
            //{
            //    m_BoxEndPos = new Vector2(hit.point.x, hit.point.z);
            //}
        }

        Vector3 squareStart = m_Camera.WorldToScreenPoint(m_BoxStartPos);

        Vector3 center = (squareStart + (Vector3)m_BoxEndPos) / 2f;

        m_SquareImage.position = center;

        float sizeX = Mathf.Abs(squareStart.x - m_BoxEndPos.x);
        float sizeY = Mathf.Abs(squareStart.y - m_BoxEndPos.y);

        m_SquareImage.sizeDelta = new Vector2(sizeX, sizeY);

        //if (m_BoxEndPos != Vector2.zero && m_BoxStartPos != Vector2.zero)
        //{
        //    m_BoxStartPos = m_BoxEndPos = Vector2.zero;
        //}
    }


    //private void OnGUI()
    //{
    //    // draw selection box
    //    //if (m_BoxEndPos != Vector2.zero && m_BoxStartPos != Vector2.zero)
    //    //{
    //    //    var rect = new Rect(m_BoxStartPos.x, Screen.height - m_BoxStartPos.y, m_BoxEndPos.x - m_BoxStartPos.x, -1 * (m_BoxEndPos.y - m_BoxStartPos.y));
    //    //    Debug.Log(m_Image);
    //    //    GUI.DrawTexture(rect, m_Image);
    //    //}
    //}
}