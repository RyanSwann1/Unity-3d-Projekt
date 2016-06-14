using UnityEngine;
using System.Collections;

public class PickUpSpawner : MonoBehaviour {

    public PickUp m_pickUp;
    public float m_spawnStartWait;
    public float m_timeBetweenSpawn;
    public int m_maxPickUpCount;
    public event System.Action m_pickUpDestroyed;

    private MapGenerator m_map;
    private Transform m_PickUpHolder;
    private int m_currentPickUpCount;

    public void ReducePickUpCount() { m_currentPickUpCount--; }

    private void Awake()
    {
        m_map = FindObjectOfType<MapGenerator>();
    }

    private void Start()
    {
        m_currentPickUpCount = 0;
        InvokeRepeating("SpawnPickUp", m_spawnStartWait, m_timeBetweenSpawn);
        m_PickUpHolder = new GameObject("Pick Ups").transform;
    }

    private void SpawnPickUp()
    {
        if(m_currentPickUpCount < m_maxPickUpCount)
        {
            Transform spawnPos = m_map.getRandomOpenTile();
            PickUp newPickUp = Instantiate(m_pickUp, spawnPos.position, m_pickUp.transform.rotation) as PickUp;
            newPickUp.transform.position = new Vector3(newPickUp.transform.position.x, newPickUp.transform.position.y + 1f, newPickUp.transform.position.z);
            newPickUp.transform.parent = m_PickUpHolder;
            m_currentPickUpCount++;
        }
    }

    public void ClearPickUps()
    {
        for(int i = 0; i < m_PickUpHolder.childCount; i++)
        {
            Destroy(m_PickUpHolder.GetChild(i).gameObject);
        }
        m_currentPickUpCount = 0;
    }

    
}
