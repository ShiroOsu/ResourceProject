using Code.Logger;
using UnityEngine;

public class CustomSizer3D : MonoBehaviour
{
    private Bounds m_Bounds;
    public Vector3 m_Dimensions;
    public Transform m_CenterOfArea;

    private void DrawArea(Color color)
    {
        Gizmos.color = color;

        if (!m_CenterOfArea)
            return;

        SetArea();
        
        Gizmos.DrawWireCube(m_Bounds.center, m_Bounds.size);
    }

    private void SetArea()
    {
        var centerPos = m_CenterOfArea.position;

        m_Bounds.extents = m_Dimensions;
        m_Bounds.center = centerPos;
    }

    /// <summary>
    /// When getting the size it will be divided by GameObjects world scale
    /// So the actual size will correct even if object has been scaled. 
    /// </summary>
    /// <param name="worldScale">GameObjects scale in world (LossyScale)</param>
    /// <returns></returns>
    public Vector3 GetSize(Vector3 worldScale)
    {
        SetArea();

        float avgOfWorldScale = (worldScale.x + worldScale.y + worldScale.z) / 3f;
        
        return m_Bounds.size / avgOfWorldScale;
    }

    public void OnDrawGizmosSelected()
    {
        DrawArea(Color.magenta);
    }
}