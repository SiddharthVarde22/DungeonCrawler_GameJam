using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEnemyState
{
    move,
    attack
}

public class EnemyBehaviour : MonoBehaviour
{
    Vector2 m_gridPosition;
    PlayerMovements m_playerMovementRefrence;
    PlayerBehavior m_playerReference;
    EEnemyState m_enemyState = EEnemyState.move;

    [SerializeField]
    float m_minAttackPower = 15, m_maxAttackPower = 20, m_health = 100;

    public Vector2 GridPosition
    {
        get
        {
            return m_gridPosition;
        }
    }

    public void Init(int a_row, int a_column, PlayerMovements a_playerRef)
    {
        m_gridPosition.x = a_row;
        m_gridPosition.y = a_column;
        ChangePosition(a_row, a_column);
        m_playerMovementRefrence = a_playerRef;
        m_playerReference = a_playerRef.GetComponent<PlayerBehavior>();

        CheckIfNearPlayer();
    }

    public void CheckIfNearPlayer()
    {
        Vector2 l_playerGridPos = m_playerMovementRefrence.GridLocation;
        if ((m_gridPosition.x == l_playerGridPos.x + 1 || m_gridPosition.x == l_playerGridPos.x - 1)
            && (m_gridPosition.y == l_playerGridPos.y))
        {
            GetInCombat();
        }
        else if((m_gridPosition.y == l_playerGridPos.y + 1 || m_gridPosition.y == l_playerGridPos.y - 1)
            && m_gridPosition.x == l_playerGridPos.x)
        {
            GetInCombat();
        }
    }

    private void GetInCombat()
    {
        m_enemyState = EEnemyState.attack;
        InputManager.IsInCombat = true;
    }

    public void TakeTurn()
    {
        switch(m_enemyState)
        {
            case EEnemyState.move:
                Vector2 l_direction = m_playerMovementRefrence.GridLocation - m_gridPosition;
                CheckIfNearPlayer();

                if(m_enemyState == EEnemyState.attack)
                {
                    Debug.Log("Enemy should attack insted of move");
                    TakeTurn();
                }

                int l_row = (int)m_gridPosition.x;
                int l_column = (int)m_gridPosition.y;

                if (m_enemyState == EEnemyState.move && l_direction.x != 0)
                {

                    if (l_direction.x > 0)
                    {
                        l_row = (int)m_gridPosition.x + 1;
                    }
                    else
                    {
                        l_row = (int)m_gridPosition.x - 1;
                    }
                    
                }
                else if(m_enemyState == EEnemyState.move && l_direction.y != 0)
                {
                    if(l_direction.y > 0)
                    {
                        l_column = (int)m_gridPosition.y + 1;
                    }
                    else
                    {
                        l_column = (int)m_gridPosition.y - 1;
                    }

                }

                ChangePosition(l_row, l_column);
                //inform turn manager
                TurnManager.OnEnemyTurnComplete();
                break;

            case EEnemyState.attack:
                float l_attackPow = Random.Range(m_minAttackPower, m_maxAttackPower);
                m_playerReference.GetDamage(l_attackPow);
                //inform turn manager
                TurnManager.OnEnemyTurnComplete();
                break;
        }
    }

    public void TakeDamage(float a_value)
    {
        m_health -= a_value;
        if(m_health <= 0)
        {
            m_health = 0;
            //Enemy dies
            TurnManager.EnemyDied(this);
        }
    }

    private void ChangePosition(int a_row, int a_column)
    {
        Vector3 l_position = Vector3.zero;
        l_position = GridManager.GetGridPosition(a_row, a_column);

        if (l_position != Vector3.zero)
        {
            l_position.y += transform.localScale.y;
            transform.position = l_position;
            m_gridPosition.x = a_row;
            m_gridPosition.y = a_column;
        }
        else
        {
            Debug.LogError(gameObject.name + " can not move at " + a_row);
        }
    }
}
