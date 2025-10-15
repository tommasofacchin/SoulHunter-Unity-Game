using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponClick : MonoBehaviour
{
    private WeaponScript weaponscript;


    private void Awake()
    {
        weaponscript = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponScript>();
    }


    private void OnMouseDown()
    {
        if(weaponscript.currentWeapon == 1)
        {
            switch (weaponscript.weaponLevel)
            {
                case 1:
                    break;
                case 2:
                case 3:
                    weaponscript.currentWeapon = 2;
                    weaponscript.WeaponChanger(2);
                    break;
            }
        }
        else if (weaponscript.currentWeapon == 2)
        {
            switch (weaponscript.weaponLevel)
            {
                case 2:
                    weaponscript.currentWeapon = 1;
                    weaponscript.WeaponChanger(1);
                    break;
                case 3:
                    weaponscript.currentWeapon = 3;
                    weaponscript.WeaponChanger(3);
                    break;
            }
        }
        else if (weaponscript.currentWeapon == 3)
        {
            switch (weaponscript.weaponLevel)
            {
                case 3:
                    weaponscript.currentWeapon = 1;
                    weaponscript.WeaponChanger(1);
                    break;
            }
        }




    }
  

}
