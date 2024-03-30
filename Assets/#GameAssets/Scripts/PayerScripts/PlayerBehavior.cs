using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    float m_health = 100, m_minAttackPower = 25, m_maxAttackPower = 30, m_minHeal = 15, m_maxHeal = 20, m_maxHealth = 100;

    public void GetDamage(float a_damage)
    {
        m_health -= a_damage;
        if(m_health <= 0)
        {
            m_health = 0;
            //Player lose
        }

        //update ui
    }

    public void Attack()
    {
        float l_damage = Random.Range(m_minAttackPower, m_maxAttackPower);
        // damage enemy
        TurnManager.CurrentTurnEnemy.TakeDamage(l_damage);
        //inform turn manager
        TurnManager.OnPlayerTurnComplete();
    }

    public void Heal()
    {
        float l_healPower = Random.Range(m_minHeal, m_maxHeal);
        m_health += l_healPower;

        if(m_health >= m_maxHealth)
        {
            m_health = m_maxHealth;
        }

        //update UI
        //inform turn manager
        TurnManager.OnPlayerTurnComplete();
    }

    public void TakeTurn()
    {
        // activate combat ui if in combat
        // Else activate movement ui
    }
}
