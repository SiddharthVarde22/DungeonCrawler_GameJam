using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDirection
{
    forward,
    right,
    back,
    left
}

public class PlayerMovements : MonoBehaviour
{
    EDirection m_direction = EDirection.forward;
    //int m_row = 0, m_column = 0;
    Vector2 m_gridLocation = Vector2.zero;

    public Vector2 GridLocation { get => m_gridLocation; }

    void Start()
    {
        InputManager.SubscribeToOnKeyPressedEvent(OnForwardPressed, OnBackPressed, onRightPressed, OnLeftPressed);
        if(!MoveToPosition((int)m_gridLocation.x, (int)m_gridLocation.y))
        {
            Debug.LogError("Somthing went wrong");
        }
        RotateInDirection();
    }

    private void OnDestroy()
    {
        InputManager.UnSubscribeFromOnKeyPressedEvent(OnForwardPressed, OnBackPressed, onRightPressed, OnLeftPressed);
    }

    public void OnForwardPressed()
    {
        int l_row = (int)m_gridLocation.x, l_column= (int)m_gridLocation.y;
        switch(m_direction)
        {
            case EDirection.forward:
                l_row++;
                if(MoveToPosition(l_row, l_column))
                {
                    m_gridLocation.x = l_row;
                }
                break;
            case EDirection.right:
                l_column++;
                if (MoveToPosition(l_row, l_column))
                {
                    m_gridLocation.y = l_column;
                }
                break;
            case EDirection.back:
                l_row--;
                if (MoveToPosition(l_row, l_column))
                {
                    m_gridLocation.x = l_row;
                }
                break;
            case EDirection.left:
                l_column--;
                if (MoveToPosition(l_row, l_column))
                {
                    m_gridLocation.y = l_column;
                }
                break;
        }
    }

    public void OnBackPressed()
    {
        int l_row = (int)m_gridLocation.x, l_column = (int)m_gridLocation.y;
        switch (m_direction)
        {
            case EDirection.forward:
                l_row--;
                if (MoveToPosition(l_row, l_column))
                {
                    m_gridLocation.x = l_row;
                }
                break;
            case EDirection.right:
                l_column--;
                if (MoveToPosition(l_row, l_column))
                {
                    m_gridLocation.y = l_column;
                }
                break;
            case EDirection.back:
                l_row++;
                if (MoveToPosition(l_row, l_column))
                {
                    m_gridLocation.x = l_row;
                }
                break;
            case EDirection.left:
                l_column++;
                if (MoveToPosition(l_row, l_column))
                {
                    m_gridLocation.y = l_column;
                }
                break;
        }
    }

    public void onRightPressed()
    {
        m_direction++;
        if(m_direction > EDirection.left)
        {
            m_direction = EDirection.forward;
        }
        RotateInDirection();
    }

    public void OnLeftPressed()
    {
        m_direction--;
        if(m_direction < EDirection.forward)
        {
            m_direction = EDirection.left;
        }

        RotateInDirection();
    }

    private bool MoveToPosition(int a_row, int a_column)
    {
        Vector3 l_positionToMove;

        l_positionToMove = GridManager.GetGridPosition(a_row, a_column);
        if(l_positionToMove != Vector3.zero)
        {
            l_positionToMove.y = transform.position.y;
            transform.position = l_positionToMove;
            return true;
        }

        return false;
    }

    private void RotateInDirection()
    {
        switch(m_direction)
        {
            case EDirection.forward:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case EDirection.right:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case EDirection.back:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case EDirection.left:
                transform.rotation = Quaternion.Euler(0, 270, 0);
                break;
        }
    }
}
