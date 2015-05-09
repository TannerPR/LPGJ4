using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Grid : MonoBehaviour 
{
    public float m_GridWidth = 32.0f;
    public float m_GridHeight = 32.0f;

    public Color m_GridColor = Color.white;


    void OnDrawGizmos()
    {
        Vector3 pos = Camera.current.transform.position;
        Gizmos.color = m_GridColor;

        for (float y = pos.y - 800.0f; y < pos.y + 800.0f; y += m_GridHeight)
        {
            Gizmos.DrawLine(new Vector3(-1000000.0f, Mathf.Floor(y / m_GridHeight) * m_GridHeight, 0.0f),
                            new Vector3(1000000.0f, Mathf.Floor(y / m_GridHeight) * m_GridHeight, 0.0f));
        }

        for (float x = pos.x - 1200.0f; x < pos.x + 1200.0f; x += m_GridWidth)
        {
            Gizmos.DrawLine(new Vector3(Mathf.Floor(x / m_GridWidth) * m_GridWidth, -1000000.0f, 0.0f),
                            new Vector3(Mathf.Floor(x / m_GridWidth) * m_GridWidth, 1000000.0f, 0.0f));
        }
    }


}
