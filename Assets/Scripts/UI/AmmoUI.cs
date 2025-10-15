using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class AmmoUI : MonoBehaviour
{

    public TextMeshProUGUI text;
    private Shooting shooting;
    private WeaponScript weapon;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        shooting = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Shooting>();
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponScript>();
    }

    private void Update()
    {
        if(weapon.currentWeapon == 1)
        {
            text.text = "" + shooting.gunAmmo;
        }
        if (weapon.currentWeapon == 2)
        {
            text.text = "" + shooting.shotGunAmmo;
        }
        if (weapon.currentWeapon == 3)
        {
            text.text = "" + shooting.ak47Ammo;
        }
        if (weapon.currentWeapon == 4)
        {
            text.text = "" + shooting.bazookaAmmo;
        }

    }
}
