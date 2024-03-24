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
    int m_row = 0, m_column = 0;
    void Start()
    {
        InputManager.SubscribeToOnKeyPressedEvent(OnForwardPressed, OnBackPressed, onRightPressed, OnLeftPressed);
        if(!MoveToPosition(m_row, m_column))
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
        int l_row = m_row, l_column= m_column;
        switch(m_direction)
        {
            case EDirection.forward:
                l_row++;
                if(MoveToPosition(l_row, l_column))
                {
                    m_row = l_row;
                }
                break;
            case EDirection.right:
                l_column++;
                if (MoveToPosition(l_row, l_column))
                {
                    m_column = l_column;
                }
                break;
            case EDirection.back:
                l_row--;
                if (MoveToPosition(l_row, l_column))
                {
                    m_row = l_row;
                }
                break;
            case EDirection.left:
                l_column--;
                if (MoveToPosition(l_row, l_column))
                {
                    m_column = l_column;
                }
                break;
        }
    }

    public void OnBackPressed()
    {
        int l_row = m_row, l_column = m_column;
        switch (m_direction)
        {
            case EDirection.forward:
                l_row--;
                if (MoveToPosition(l_row, l_column))
                {
                    m_row = l_row;
                }
                break;
            case EDirection.right:
                l_column--;
                if (MoveToPosition(l_row, l_column))
                {
                    m_column = l_column;
                }
                break;
            case EDirection.back:
                l_row++;
                if (MoveToPosition(l_row, l_column))
                {
                    m_row = l_row;
                }
                break;
            case EDirection.left:
                l_column++;
                if (MoveToPosition(l_row, l_column))
                {
                    m_column = l_column;
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
