using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private Shooting shooting;

    public GameObject gun;
    public GameObject shotgun;
    public GameObject ak47;
    public GameObject bazooka;

    public float weaponLevel;

    public float damage;
    public float timeBetweenFiring;
    public float currentWeapon;


    private void Start()
    {
        shooting = GetComponent<Shooting>();
        currentWeapon = 1;
        WeaponChanger(1);
        weaponLevel = PlayerPrefs.GetInt("chestCount") + 1;
    }

    private void Update()
    {
        if (Input.GetKey("1"))
        {
            currentWeapon = 1;
            WeaponChanger(1);
        }
        if (Input.GetKey("2") && weaponLevel >= 2)
        {
            currentWeapon = 2;
            WeaponChanger(2);
        }
        if (Input.GetKey("3") && weaponLevel >= 3)
        {
            currentWeapon = 3;
            WeaponChanger(3);
        }
        if (Input.GetKey("4") && weaponLevel >= 4)
        {
            currentWeapon = 4;
            WeaponChanger(4);
        }
    }
    public void WeaponLevelUp()
    {
        setActiveFalse();

        weaponLevel++;

        switch (weaponLevel)
        {
            case 2:
                currentWeapon = 2;
                shotgun.SetActive(true);
                damage = 21;
                timeBetweenFiring = 0.3f;
                break;
            case 3:
                currentWeapon = 3;
                ak47.SetActive(true);
                damage = 7;
                timeBetweenFiring = .1f;
                break;
            case 4:
                currentWeapon = 4;
                bazooka.SetActive(true);
                damage = 0;
                timeBetweenFiring = .7f;
                break;
        }
    }

    public void WeaponChanger(int weaponNumber)
    {
        switch (weaponNumber)
        {
            case 1:
                setActiveFalse();
                gun.SetActive(true);
                damage = 10;
                timeBetweenFiring = .2f;
                break;
            case 2:
                setActiveFalse();
                shotgun.SetActive(true);
                damage = 21;
                timeBetweenFiring = 0.3f;
                break;
            case 3:
                setActiveFalse();
                ak47.SetActive(true);
                damage = 7;
                timeBetweenFiring = .1f;
                break;
            case 4:
                setActiveFalse();
                bazooka.SetActive(true);
                damage = 0;
                timeBetweenFiring = .7f;
                break;
        }
    }


    private void setActiveFalse()
    {
        gun.SetActive(false);
        shotgun.SetActive(false);
        ak47.SetActive(false);
        bazooka.SetActive(false);
    }


}

