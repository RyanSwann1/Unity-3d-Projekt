using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    private float m_movementSpeed;
    private float m_destroyTime;
    private int m_shootableMask;
    private int m_damage;

    private Rigidbody m_rigidbody;
    private bool m_onHit; //To ensure that it doesn't hit an shootable object twice

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_shootableMask = LayerMask.GetMask("Shootable");
    }

    private void Start()
    {
        m_onHit = false;
        m_destroyTime = 3.5f;
        Destroy(gameObject, m_destroyTime);
        m_rigidbody.AddForce(transform.forward * 2000);
        //If initial collisons on spawn detected
        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, 0.1f);
        if (initialCollisions.Length > 0)
        {
            //OnHit(initialCollisions[0], transform.position);
        }
    }

    private void Update()
    {
        float moveDistance = m_movementSpeed * Time.deltaTime;
        CheckCollisions(moveDistance);
        //Move(moveDistance);
    }


    private void Move(float moveDistance)
    {
        transform.Translate(Vector3.forward * moveDistance);
    }

    private void CheckCollisions(float moveDistance)
    {
        if(!m_onHit)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, moveDistance, m_shootableMask))
            {
                OnHit(rayHit.collider, rayHit.point);
                m_onHit = true;
            }
        }

    }

    private void OnHit(Collider c, Vector3 hitPoint)
    {
        if (c.gameObject.tag != "Shell")
        {
            IDamageable damageableGameObject = c.GetComponent<IDamageable>();
            if (damageableGameObject != null)
            {
                damageableGameObject.TakeHit(m_damage, hitPoint, transform.forward);
            }
            Destroy(gameObject, m_destroyTime);
            m_onHit = true;
        }
    }

    public void SetSpeed(float newSpeed)
    {
        m_movementSpeed = newSpeed;
    }

    public void SetDamage(int newDamage)
    {
        m_damage = newDamage;
    }
}