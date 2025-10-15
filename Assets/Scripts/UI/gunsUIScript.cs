using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunsUIScript : MonoBehaviour
{

    private WeaponScript weaponscript;

    public GameObject gun;
    public GameObject shotGun;
    public GameObject ak47;
    public GameObject bazooka;

    private void Awake()
    {
        weaponscript = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponScript>();
    }

    private void Update()
    {
        if(weaponscript.currentWeapon == 1)
        {
            gun.SetActive(true);
            shotGun.SetActive(false);
            ak47.SetActive(false);
            bazooka.SetActive(false);
        }
        if (weaponscript.currentWeapon == 2)
        {
            gun.SetActive(false);
            shotGun.SetActive(true);
            ak47.SetActive(false);
            bazooka.SetActive(false);
        }
        if (weaponscript.currentWeapon == 3)
        {
            gun.SetActive(false);
            shotGun.SetActive(false);
            ak47.SetActive(true);
            bazooka.SetActive(false);
        }
        if (weaponscript.currentWeapon == 4)
        {
            gun.SetActive(false);
            shotGun.SetActive(false);
            ak47.SetActive(false);
            bazooka.SetActive(true);
        }
    }

}
