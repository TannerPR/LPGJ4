using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour 
{
    public int m_Width = 5;
    public int m_Height = 6;
    public int m_Counter = 0;
    private float m_HorizontalSpacing;
    private float m_VerticalSpacing;
    public GameObject m_GridCell;
    public GameObject m_ShelfObject;

    private GameObject[,] m_Grid;

    public List<CellInfo> listOfGridCellInfo = new List<CellInfo>();

	void Start () 
    {
        m_Grid = new GameObject[m_Width, m_Height];
        m_HorizontalSpacing = m_GridCell.GetComponent<SpriteRenderer>().bounds.size.x;
        m_VerticalSpacing = m_ShelfObject.GetComponent<SpriteRenderer>().bounds.size.y + m_GridCell.GetComponent<SpriteRenderer>().bounds.size.y;
        GenerateGrid();
	}

    private void GenerateGrid()
    {
        for (int y = 0; y < m_Height; ++y)
        {
            for (int x = 0; x < m_Width; ++x)
            {
                Vector3 offset = new Vector3(x * m_HorizontalSpacing, y * m_VerticalSpacing, 0.0f);

                GameObject gridObj = (GameObject)GameObject.Instantiate(m_GridCell);

                gridObj.transform.position = offset + transform.position;
                gridObj.transform.parent = transform;

                gridObj.GetComponent<SpriteRenderer>().sprite = listOfGridCellInfo[m_Counter].sprite;

                if(listOfGridCellInfo[m_Counter].sprite != null)
                {
                    gridObj.GetComponent<SpriteRenderer>().sprite = listOfGridCellInfo[m_Counter].sprite;
                    gridObj.GetComponent<GridCell>().m_IsABook = true;

                }

                m_Grid[x, y] = gridObj;
                m_Counter++;
            }
        }
    }

    void Update()
    {

    }
}
