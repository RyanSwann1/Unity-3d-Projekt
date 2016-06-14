using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

    public MapGenerator m_map;
    public PickUpSpawner m_pickUpSpawner;
    public EnemySpawner m_enemySpawner;

    private void Awake()
    {
        FindObjectOfType<EnemySpawner>().OnNextWave += OnNextWave;
    }



    private void OnNextWave()
    {
        m_map.NextMap();
        m_enemySpawner.ClearEnemies();
        m_pickUpSpawner.ClearPickUps();
    }

}
