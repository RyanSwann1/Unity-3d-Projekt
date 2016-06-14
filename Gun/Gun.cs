//using UnityEngine;
//using System.Collections;

//public class Gun : MonoBehaviour {

//    public enum FireType
//    {
//        SINGLE,
//        BURST,
//        AUTO,

//        COUNT
//    }

//    public FireType m_fireType;
//    public Light m_fireLight;
//    public Shell m_shell;
//    public Projectile m_projectile;
//    public Transform m_shellEjection;
//    public Transform[] m_firePoints;
//    public float m_muzzleVelocity;


//    public float m_reloadTime;
//    public float m_muzzleFlashTime;
//    public float m_shotWaitTime;
//    public int m_magazineSize;
//    public int m_damage;

//    private FireType m_currentFireType;

//    private int m_currentFireTypeIndex;
//    private float m_nextShotTime;
//    private int m_currentMagazineSize;
//    private int m_maxBurstFireShots;
//    private int m_currentBurstFireShots;
//    private bool m_triggerHeld;

//    public int GetCurrentMagazineSize() { return m_currentMagazineSize; }

//    public void TriggerReleased() { m_triggerHeld = false; }

//    private void Start()
//    {
//        m_currentMagazineSize = m_magazineSize;
//        m_fireLight.enabled = false;
//        m_maxBurstFireShots = 3;
//        m_currentBurstFireShots = 0;
//        m_currentFireType = m_fireType;
//    }

//    public void Shoot()
//    {
//        Debug.Log("Shoot Single");
//        //If single shot
//        if (m_currentFireType == FireType.SINGLE)
//        {

//            FireProjectile();
//            m_triggerHeld = false;
//            return;
//        }

//        //If burst shot
//        else if (m_currentFireType == FireType.BURST)
//        {
//            if (m_currentBurstFireShots < m_maxBurstFireShots)
//            {
//                FireProjectile();
//                m_currentBurstFireShots++;
//            }
//            else
//            {
//                m_triggerHeld = false;
//                m_currentBurstFireShots = 0;
//                return;
//            }
//        }

//        //If auto shot
//        else if (m_currentFireType == FireType.AUTO)
//        {
//            Debug.Log("Shoot");
//            FireProjectile();
//        }


//        //If no ammo
//        if (m_currentMagazineSize <= 0)
//        {
//            m_triggerHeld = false;
//            StartCoroutine(Reload());
//        }



//    }

//    IEnumerator Reload()
//    {
//        yield return new WaitForSeconds(m_reloadTime);

//        m_currentMagazineSize = m_magazineSize;
//    }

//    private void SpawnShell()
//    {
//        Instantiate(m_shell, m_shellEjection.position, m_firePoints[0].rotation);
//    }

//    private void DisableLight()
//    {
//        m_fireLight.enabled = false;
//    }

//    private void HandleFireType()
//    {
//        ////Gun only has one fire type
//        //if(m_fireType.Length <= 1)
//        //{

//        //}
//        //else
//        //{
//        //    if (m_currentFireType == FireType.AUTO)
//        //    {
//        //        m_nextShotTime = Time.time + m_shotWaitTime[(int)FireType.AUTO]; //m_autoShotTimeWait;
//        //    }

//        //    else if (m_currentFireType == FireType.BURST)
//        //    {
//        //        m_nextShotTime = Time.time + m_autoShotTimeWait;

//        //        m_currentBurstFireShots++;
//        //        if (m_currentBurstFireShots >= m_maxBurstFireShots)
//        //        {
//        //            m_nextShotTime = Time.time + m_burstShotTimeWait;
//        //            m_currentBurstFireShots = 0;
//        //        }
//        //    }

//        //    else if (m_currentFireType == FireType.SINGLE)
//        //    {
//        //        m_nextShotTime = Time.time + m_singleShotTimeWait;
//        //    }
//        //}

//    }

//    private void FireProjectile()
//    {
//        if (Time.time > m_nextShotTime && m_currentMagazineSize > 0)
//        {
//            m_nextShotTime = Time.time + m_shotWaitTime;
//            for (int i = 0; i < m_firePoints.Length; i++)
//            {
//                //Act accordingly to type of gun

//                //Fire projectile
//                Projectile newProjectile = Instantiate(m_projectile, m_firePoints[i].position, m_firePoints[i].rotation) as Projectile;
//                newProjectile.SetSpeed(m_muzzleVelocity);
//                newProjectile.SetDamage(m_damage);

//                //Conrtol behaviour
//                m_currentMagazineSize--;
//                SpawnShell();
//                m_fireLight.enabled = true;
//                Invoke("DisableLight", m_muzzleFlashTime);
//            }
//        }
//    }
//    public void TriggerHeld()
//    {
//        m_triggerHeld = true;
//        Shoot();
//    }


//}

using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{

    public enum FireType
    {
        SINGLE,
        BURST,
        AUTO,

        //COUNT
    }

    public FireType[] m_fireType;
    public float[] m_shotWaitTime;
    public Light m_fireLight;
    public Shell m_shell;
    public Projectile m_projectile;
    public Transform m_shellEjection;
    public Transform[] m_firePoints;
    public float m_muzzleVelocity;

    
    //public float[] m_shotTimeeWait = new float[m_enabledFireTypes.Length];
    public float m_reloadTime;
    public float m_muzzleFlashTime;
    public int m_magazineSize;
    public int m_damage;

    private FireType m_currentFireType;
    private float m_nextShotTime;
    private int m_currentMagazineSize;
    private float m_autoShotTimeWait;
    private float m_burstShotTimeWait;
    private float m_singleShotTimeWait;
    private int m_maxBurstFireShots;
    private int m_currentBurstFireShots;
    private int m_fireTypeIndex;

    public int GetCurrentMagazineSize() { return m_currentMagazineSize; }

    private void Start()
    {
        m_currentMagazineSize = m_magazineSize;
        m_fireLight.enabled = false;
        m_maxBurstFireShots = 3;
        m_currentBurstFireShots = 0;
        m_autoShotTimeWait = 0.15f;
        m_burstShotTimeWait = 1.2f;
        m_singleShotTimeWait = 1.0f;
        m_currentFireType = m_fireType[0];
        m_fireTypeIndex = 1;
    }

    public void Shoot()
    {
        if (Time.time > m_nextShotTime && m_currentMagazineSize > 0)
        {
            for (int i = 0; i < m_firePoints.Length; i++)
            {
                //Act accordingly to type of gun
                HandleFireType();
                //Fire projectile
                Projectile newProjectile = Instantiate(m_projectile, m_firePoints[i].position, m_firePoints[i].rotation) as Projectile;
                newProjectile.SetSpeed(m_muzzleVelocity);
                newProjectile.SetDamage(m_damage);

                //Conrtol behaviour
                m_currentMagazineSize--;
                SpawnShell();
                m_fireLight.enabled = true;
                Invoke("DisableLight", m_muzzleFlashTime);
            }

        }

        if (m_currentMagazineSize <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    //m_fireTypeIndex = 1;
    public void ChangeGunFireType()
    {
        m_fireTypeIndex++;
        
        //If firetype exists
        if (m_fireTypeIndex - 1 < m_fireType.Length)
        {
            m_currentFireType = m_fireType[m_fireTypeIndex - 1];
            Debug.Log("Index: " + m_fireTypeIndex);
        }
        else
        {
            Debug.Log("Reset");
            m_fireTypeIndex = 1;
            m_currentFireType = m_fireType[m_fireTypeIndex - 1];
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(m_reloadTime);

        m_currentMagazineSize = m_magazineSize;
    }

    private void SpawnShell()
    {
        Instantiate(m_shell, m_shellEjection.position, m_firePoints[0].rotation);
    }

    private void DisableLight()
    {
        m_fireLight.enabled = false;
    }

    private void HandleFireType()
    {
        if (m_currentFireType == FireType.AUTO)
        {
            m_nextShotTime = Time.time + m_autoShotTimeWait;
        }

        else if (m_currentFireType == FireType.BURST)
        {
            m_nextShotTime = Time.time + m_autoShotTimeWait;

            m_currentBurstFireShots++;
            if (m_currentBurstFireShots >= m_maxBurstFireShots)
            {
                m_nextShotTime = Time.time + m_burstShotTimeWait;
                m_currentBurstFireShots = 0;
            }
        }

        else if (m_currentFireType == FireType.SINGLE)
        {
            m_nextShotTime = Time.time + m_singleShotTimeWait;
        }
    }


}
