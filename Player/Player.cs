using UnityEngine;
using System.Collections;

public class Player : LivingEntity {

    public float m_movementSpeed;

    private int m_floorMask;
    private GunController m_gunController;
    private PlayerController m_playerController;
    private GameObject m_groundPlane;
    private MapGenerator m_map;

    public void AddToHealth(int i) { m_currentHealth += i; }

    private void Awake()
    {
        m_map = FindObjectOfType<MapGenerator>();
        m_groundPlane = GameObject.FindGameObjectWithTag("Floor");
        m_gunController = GetComponent<GunController>();
        m_playerController = GetComponent<PlayerController>();
    }

    protected override void Start()
    {
        base.Start();
    }


    private void Update()
    {
        Move();
        Turn();
        Shoot();
        ChangeGunFireRate();
    }

    public void SetSpawnPos()
    {
        Transform spawnPos = m_map.GetTileFromPos(Vector3.zero);
        Vector3 heightCorrectedPos = new Vector3(spawnPos.position.x, spawnPos.position.y, spawnPos.position.z) + Vector3.up;
        transform.position = heightCorrectedPos;
    }

    private void Move()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * m_movementSpeed;
        m_playerController.Move(moveVelocity);
    }

    private void Turn()
    {
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDistance;
        if(groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            m_playerController.LookAt(point);
        }
    }

    private void Shoot()
    {
        if(Input.GetButton("Fire1"))
        {
            m_gunController.Shoot();
        }
        
    }

    private void ChangeGunFireRate()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            m_gunController.ChangeGunFireType();
        }
    }

    
}
