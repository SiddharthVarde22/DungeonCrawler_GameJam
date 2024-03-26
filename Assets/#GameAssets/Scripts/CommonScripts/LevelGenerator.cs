using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private static LevelGenerator Instance { get; set; }
    Vector2 m_sizeOfTheGrid;

    [SerializeField]
    GameObject m_floorObject, m_wallObject, m_doorObject, m_cornerCube;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Instance of Level Generator already exists");
            return;
        }
    }

    private void Start()
    {
        m_sizeOfTheGrid = GridManager.SizeOfTheGrid;
        GenerateLevel();
    }

    public static void GenerateLevel()
    {
        int l_lastPosX = (int)Instance.m_sizeOfTheGrid.x, l_lastPosY = (int)Instance.m_sizeOfTheGrid.y;
        int l_centerX = l_lastPosX / 2, l_centerY = l_lastPosY / 2;
        Vector3 l_rotation = Vector3.zero;

        for (int i = 0; i < Instance.m_sizeOfTheGrid.x; i++)
        {
            for(int j = 0; j < Instance.m_sizeOfTheGrid.y; j++)
            {
                if(i == 0 || j == 0 || i == (Instance.m_sizeOfTheGrid.x - 1) || j == (Instance.m_sizeOfTheGrid.y - 1))
                {
                    GameObject l_generatedObject = null;
                    if (i == l_centerX || j == l_centerY)
                    {
                        l_generatedObject = Instantiate(Instance.m_doorObject, GridManager.GetGridPosition(i, j), Quaternion.identity, Instance.transform);
                    }
                    else if((i == 0 && j == 0) || (i == 0 && j == (l_lastPosY - 1)) || 
                        (i == (l_lastPosX - 1) && j == 0) || (i == (l_lastPosX - 1) && j == (l_lastPosY - 1)))
                    {
                        l_generatedObject = Instantiate(Instance.m_cornerCube, GridManager.GetGridPosition(i, j), Quaternion.identity, Instance.transform);
                    }
                    else
                    {
                        l_generatedObject = Instantiate(Instance.m_wallObject, GridManager.GetGridPosition(i, j), Quaternion.identity, Instance.transform);
                    }

                    if (i == 0 && j == 0)
                    {
                        l_rotation = new Vector3(0, 90, 0);
                    }
                    else if(i == l_lastPosX - 1 && j == l_lastPosY -1)
                    {
                        l_rotation = new Vector3(0, 270, 0);
                    }
                    else if(i == 0)
                    {
                        l_rotation = Vector3.zero;
                    }
                    else if(i == (l_lastPosX - 1))
                    {
                        l_rotation = new Vector3(0, 180, 0);
                    }
                    else if(j == 0)
                    {
                        l_rotation = new Vector3(0, 90, 0);
                    }
                    else if(j == (l_lastPosY - 1))
                    {
                        l_rotation = new Vector3(0, 270, 0);
                    }

                    l_generatedObject.transform.rotation = Quaternion.Euler(l_rotation);
                }
                else
                {
                    Instantiate(Instance.m_floorObject, GridManager.GetGridPosition(i, j), Quaternion.identity, Instance.transform);
                }
            }
        }
    }
}
