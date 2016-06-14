using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour, IDamageable {

    public int m_health;
    protected int m_currentHealth;
    protected bool m_isDead;


    public event System.Action OnDeath;

    private ParticleSystem m_particleSystem;

    private void Awake()
    {
        m_particleSystem = GetComponent<ParticleSystem>();
    }

    protected virtual void Start()
    {
        m_currentHealth = m_health;
        m_isDead = false;
    }

    public virtual void TakeHit(int damage, Vector3 hitPoint, Vector3 impactDirection)
    {
        TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        m_currentHealth -= damage;
        if(m_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        m_isDead = true;
        if(OnDeath != null)
        {
            OnDeath();
        }
        
        Destroy(gameObject);
    }

    public int GetHealth()
    {
        return m_currentHealth;
    }
}
