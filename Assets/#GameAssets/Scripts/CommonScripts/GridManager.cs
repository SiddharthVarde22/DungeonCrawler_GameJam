using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private static GridManager Instance { get; set; }

    [SerializeField]
    Vector2 m_sizeOfTheCell = Vector2.one, m_sizeOfTheGrid;
    [SerializeField]
    Vector3[,] m_centerPositionsOfCells;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"An instance of a {gameObject.name} already exists on {Instance.gameObject.name} game object");
            return;
        }

        m_centerPositionsOfCells = new Vector3[(int)m_sizeOfTheGrid.x, (int)m_sizeOfTheGrid.y];
        CreateGrid();
    }

    private void CreateGrid()
    {
        Vector3 l_cellPosition = Vector3.zero;

        for(int i = 0; i < m_sizeOfTheGrid.x; i++)
        {
            for(int j = 0; j < m_sizeOfTheGrid.y; j++)
            {
                l_cellPosition.x = transform.position.x + ((m_sizeOfTheCell.x / 2) + (j * m_sizeOfTheCell.x));
                l_cellPosition.z = transform.position.z + ((m_sizeOfTheCell.y / 2) + (i * m_sizeOfTheCell.y));
                m_centerPositionsOfCells[i, j] = l_cellPosition;

                Debug.Log($"Grid {i} , {j} is {m_centerPositionsOfCells[i, j]}");
            }
        }
    }

    public static Vector3 GetGridPosition(int a_row, int a_column)
    {
        if(a_row < Instance.m_sizeOfTheGrid.x && a_row >= 0  && a_column < Instance.m_sizeOfTheGrid.y && a_column >= 0)
        {
            return Instance.m_centerPositionsOfCells[a_row, a_column];
        }

        Debug.LogError($" row should be less than {Instance.m_sizeOfTheGrid.x} && colunm should be less than {Instance.m_sizeOfTheGrid.y}");
        return Vector3.zero;
    }

#if UNITY_EDITOR
    [SerializeField]
    bool m_createGrid = false, m_showGrid = false, m_destroyShowGrid = false;
    [SerializeField]
    GameObject m_gridPosObj;

    private void OnValidate()
    {
        if (m_createGrid)
        {
            if (m_centerPositionsOfCells == null)
            {
                m_centerPositionsOfCells = new Vector3[(int)m_sizeOfTheGrid.x, (int)m_sizeOfTheGrid.y];
            }

            CreateGrid();
            m_createGrid = false;
        }

        //if(m_showGrid)
        //{
        //    ShowGrid();
        //}

        //if (m_destroyShowGrid)
        //{
        //    DestroyShowObjects();
        //}
    }

    void ShowGrid()
    {
        for(int i = 0; i < m_sizeOfTheGrid.x; i++)
        {
            for(int j = 0; j < m_sizeOfTheGrid.y; j++)
            {
                Instantiate(m_gridPosObj, m_centerPositionsOfCells[i,j], Quaternion.identity, transform);
            }
        }

        m_showGrid = false;
    }

    void DestroyShowObjects()
    {
        int l_childCount = transform.childCount;
        for (int i = 0; i < l_childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        m_destroyShowGrid = false;
    }
#endif
}
