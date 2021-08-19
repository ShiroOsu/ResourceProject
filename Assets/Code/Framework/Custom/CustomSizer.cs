using System;
using UnityEngine;

public class CustomSizer : MonoBehaviour
{
    private Rect m_SizeArea;
    public Vector2 m_WidthAndHeight;
    public Transform m_CenterOfArea;

    private void DrawArea(Color color)
    {
        Gizmos.color = color;

        if (!m_CenterOfArea)
            return;
        
        // Show Area in Scene view
        ShowAreaInScene();

        var y = m_CenterOfArea.position.y;
        var a = new Vector3(m_SizeArea.xMin, y, m_SizeArea.yMin);
        var b = new Vector3(m_SizeArea.xMax, y, m_SizeArea.yMin);
        var c = new Vector3(m_SizeArea.xMax, y, m_SizeArea.yMax);
        var d = new Vector3(m_SizeArea.xMin, y, m_SizeArea.yMax);
        Gizmos.DrawLine(a, b);
        Gizmos.DrawLine(b, c);
        Gizmos.DrawLine(c, d);
        Gizmos.DrawLine(d, a);
    }

    private void ShowAreaInScene()
    {
        var centerPos = m_CenterOfArea.position;

        m_SizeArea.width = m_WidthAndHeight.x;
        m_SizeArea.height = m_WidthAndHeight.y;
        m_SizeArea.center = new Vector2(centerPos.x, centerPos.z);
    }

    public Rect GetSizeArea()
    {
        return m_SizeArea;
    }

    public void OnDrawGizmosSelected()
    {
        DrawArea(Color.magenta);
    }
}