using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

    private KeyCode[] numericalKeyCodes = new KeyCode[]
    {
        KeyCode.Alpha0,
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3
    };
   
    public Transform m_weaponHold;
    public Gun[] m_equippedGuns;

    private Gun m_equippedGun;
    private bool m_isGunEquipped;

    public int GetGunAmmoCount() { return m_equippedGun.GetCurrentMagazineSize(); } //For UI

    private void Start()
    {
        //for(int i = 0; )
        EquipGun(m_equippedGuns[0]);

    }

    private void Update()
    {
        for(int i = 0; i < numericalKeyCodes.Length; i++)
        {
            if (Input.GetKeyDown(numericalKeyCodes[i]))
            {
                EquipGun(m_equippedGuns[i - 1]);
            }
        }       
    }

    public void EquipGun(Gun newGun)
    {
        if(m_equippedGun != null)
        {
            Destroy(m_equippedGun.gameObject); //gameObject.seactive() to remember ammo count
            //m_equippedGun.gameObject.SetActive(true);
            
        }
        
        //Equip the new gun
        m_equippedGun = Instantiate(newGun, m_weaponHold.position, m_weaponHold.rotation) as Gun;
        m_equippedGun.transform.parent = m_weaponHold;
        m_isGunEquipped = true;
    }

    public void Shoot()
    {
        //Shoot gun if equipped
        if(m_isGunEquipped)
        {
            m_equippedGun.Shoot();
        }
    }

    public void ChangeGunFireType()
    {
        if(m_isGunEquipped)
        {
            m_equippedGun.ChangeGunFireType();
        }
        
    }
}
