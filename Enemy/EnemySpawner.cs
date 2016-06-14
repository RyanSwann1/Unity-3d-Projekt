using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

    public Enemy m_enemy;
    public float m_tileFlashSpeed;
    public float m_playerStateRefreshRate;
    public float m_playerCampingThreshold;
    public float m_campTimerDuration;
    
    private Transform m_enemyHolder;
    private Transform m_playerT;
    private int m_currentMapIndex;
    private int m_enemiesToSpawn;
    private int m_enemiesAlive;
    private MapGenerator m_map;
    private LivingEntity m_player;
    private bool m_disabled;
    private bool m_isPlayerCamping;
    private Vector3 m_playerOldPos;
    private Vector3 m_playerNewPos;

    private int m_numbOfSpawns;
    private bool m_onCurrentWave;
    public event System.Action OnNextWave;
    

    private void Start()
    {
        m_playerT = GameObject.FindGameObjectWithTag("Player").transform;
        m_player = FindObjectOfType<LivingEntity>();
        m_player.OnDeath += OnPlayerDeath;
        m_currentMapIndex = 0;
        string enemyHolderName = "Enemies";
        m_enemyHolder = new GameObject(enemyHolderName).transform;
        m_map = FindObjectOfType<MapGenerator>();
        m_disabled = false;
        m_numbOfSpawns = m_map.GetNumberOfSpawns();
        NextWave();
    }
    
    
    private void Update()
    {
        if (m_enemiesAlive <= 0)
        {
            m_onCurrentWave = false;
            NextWave();
        }

    }

    public void NextWave()
    {
        m_currentMapIndex++;
        //Check if map exists
        if (m_currentMapIndex - 1 < m_map.m_maps.Length)
        {
            OnNextWave();
            int spawnCount = m_map.getCurrentMap().m_enemies.m_spawnCount;
            Debug.Log(spawnCount);
            for (int i = 0; i < spawnCount; i++)
            {
                m_enemiesToSpawn = m_map.getCurrentMap().m_enemies.m_enemyCount;
                m_enemiesAlive = m_enemiesToSpawn;
                StartCoroutine(SpawnEnemy());
            }
        }
    }


    public void ClearEnemies()
    { 
        for(int i = 0; i < m_enemyHolder.childCount; i++)
        {
            Destroy(m_enemyHolder.GetChild(i).gameObject);
        }
    }

    private IEnumerator SpawnEnemy()
    {
        while (!m_disabled && m_enemiesToSpawn > 0)
        {

            //Get position of the tile to spawn on
            Transform spawnTile = m_map.getRandomOpenTile();

            Material spawnTileMat = spawnTile.GetComponent<Renderer>().material;

            //Change colour of tile
            Color initialColor = Color.white;
            Color newColor = Color.red;
            float spawnDelay = 1f;
            float spawnTimer = 0;
            while (spawnTimer < spawnDelay)
            {
                spawnTimer += Time.deltaTime;
                spawnTileMat.color = Color.Lerp(initialColor, newColor, Mathf.PingPong(spawnTimer * m_tileFlashSpeed, spawnDelay));
                yield return null;
            }
            
            if(spawnTile != null)
            {
                //Spawn new enemy
                Enemy newEnemy = Instantiate(m_enemy, spawnTile.position, Quaternion.identity) as Enemy;
                newEnemy.SetMovementSpeed(m_map.getCurrentMap().m_enemies.m_movementSpeed);
                newEnemy.transform.parent = m_enemyHolder;
                newEnemy.OnDeath += OnEnemyDeath;
                m_enemiesToSpawn--;
            }
            float spawnTime = Random.Range(m_map.getCurrentMap().m_enemies.m_minSpawnTime, m_map.getCurrentMap().m_enemies.m_maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void OnPlayerDeath()
    {
        m_disabled = true;
        m_enemiesToSpawn = 0;
    }

    private void OnEnemyDeath()
    {
        m_enemiesAlive--;
    }

    public int GetEnemiesAlive()
    {
        return m_enemiesAlive;
    }
}