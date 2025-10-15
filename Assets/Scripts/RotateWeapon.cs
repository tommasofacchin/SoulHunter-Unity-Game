using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWeapon : MonoBehaviour
{

    private GameObject target;

    //private float angle;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.transform.position - transform.position) * 10;

        //angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

    }
}
