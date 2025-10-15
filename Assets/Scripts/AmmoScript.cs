using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{

    private Shooting shooting;
    private WeaponScript weapon;


    private void Awake()
    {
        shooting = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Shooting>();
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            int random = 1;

           
            if (weapon.weaponLevel == 2)
            {
                random = Random.Range(1,3);
            }

            if (weapon.weaponLevel == 3)
            {
                random = Random.Range(1, 4);
            }


            if (random == 1)
            {
                shooting.gunAmmo += 100;
            }
            if (random == 2)
            {
                shooting.shotGunAmmo += 75;
            }
            if (random == 3)
            {
                shooting.ak47Ammo += 200;
            }
            Destroy(gameObject);
        }
    }
}
