using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField]
    int m_minEnemies = 1, m_maxEnemies = 3;
    [SerializeField]
    PlayerMovements m_playerMovementsRef;
    [SerializeField]
    EnemyBehaviour m_enemyPrefabToCreate;

    int m_numberOfEnemies;
    List<EnemyBehaviour> enemies = new List<EnemyBehaviour>();
    EnemyBehaviour m_currentlyFightingEnemy = null;
    int m_indexOfEnemyTakingturn;

    private static TurnManager Instance { get; set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log($"Turn manager already exists as a {Instance.gameObject.name}");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        m_numberOfEnemies = Random.Range(m_minEnemies, m_maxEnemies + 1);
        Vector2 l_gridSize = GridManager.SizeOfTheGrid;

        for (int i = 0; i < m_numberOfEnemies; i++)
        {
            EnemyBehaviour l_enemy = Instantiate<EnemyBehaviour>(m_enemyPrefabToCreate);
            int l_gridPosX = (int)Random.Range(1f, l_gridSize.x);
            int l_gridPosY = (int)Random.Range(1f, l_gridSize.y);

            l_enemy.Init(l_gridPosX, l_gridPosY, m_playerMovementsRef);
            enemies.Add(l_enemy);
        }

        m_indexOfEnemyTakingturn = 0;
        m_currentlyFightingEnemy = enemies[m_indexOfEnemyTakingturn];
    }

    public static EnemyBehaviour CurrentTurnEnemy
    {
        get => Instance.m_currentlyFightingEnemy;
    }

    public static void EnemyDied(EnemyBehaviour a_enemyThatDied)
    {
        Instance.enemies.Remove(a_enemyThatDied);
        Destroy(a_enemyThatDied.gameObject);
        Instance.m_numberOfEnemies--;
    }
}
