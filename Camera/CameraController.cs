using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private MapGenerator m_map;

    private void Awake()
    {
        m_map = FindObjectOfType<MapGenerator>();
    }

    public void ChangeCamPos()
    {
        Vector2 mapSize = m_map.GetMapSize();

        transform.position = new Vector3(0, 20f, -mapSize.y);
    }
}
