using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

    public float m_rotationSpeed;
    public float m_minLightAmount;
    public float m_maxLightAmount;
    public float m_lightTime;
    public int m_health;

    private Light m_light;
    private Player m_player;
    private PickUpSpawner m_pickUpSpawner;

    private void Awake()
    {
        m_light = GetComponent<Light>();
    }

    private void Start()
    {
        m_player = FindObjectOfType<Player>();
        m_pickUpSpawner = FindObjectOfType<PickUpSpawner>();
        StartCoroutine(AdjustLight());
    }

    private void Update()
    {
        transform.Rotate(Vector3.right * m_rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            m_player.AddToHealth(m_health);
            m_pickUpSpawner.ReducePickUpCount();
            Destroy(gameObject);
        }
    }

    IEnumerator AdjustLight()
    {
        while (true)
        {
            m_light.intensity = Mathf.Lerp(m_minLightAmount, m_maxLightAmount, Mathf.PingPong(Time.time, m_lightTime));

            yield return null;
        }
    }
}
