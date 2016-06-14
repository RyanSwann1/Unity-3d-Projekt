using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

    private Player m_player;

    public event System.Action ChangeOfMap;
    private MapGenerator m_map;

    private void Awake()
    {
        m_player = FindObjectOfType<Player>();
        m_map = FindObjectOfType<MapGenerator>();
    }

    private Transform GetSpawnPos()
    {

        Transform spawnPos = m_map.GetTileFromPos(Vector3.zero);
        return spawnPos;
    }

}
