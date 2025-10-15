using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletScript : MonoBehaviour
{
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;

    public GameObject weapon;
    private WeaponScript weaponScript;
    //public GameObject joystick;
    //public ShootJoystick joystickScript;

    //private Vector3 joystickPos;
    private Vector3 direction;

    private void Awake()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon");
        weaponScript = weapon.GetComponent<WeaponScript>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //joystick = GameObject.FindGameObjectWithTag("rightJoystick");
        //joystickScript = joystick.GetComponent<ShootJoystick>();

        rb = GetComponent<Rigidbody2D>();


        if (weaponScript.shotgun.activeSelf)
        {
            transform.localScale = new Vector3(.015f, .015f, 1);
        }
        else if (weaponScript.ak47.activeSelf)
        {
            transform.localScale = new Vector3(.01f, .01f, 1);
        }
        else if (weaponScript.bazooka.activeSelf)
        {
            transform.localScale = new Vector3(.02f, .02f, 1);
        }


    }

    private void Start()
    {

        /*
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        rotation.Normalize();
        rb.velocity = new Vector3(mousePos.x, mousePos.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
        */

        Destroy(gameObject, 2f);

        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - transform.position).normalized;

        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
    }


    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    /*
    public void Restart()
    {

        //joystickPos.x = joystickScript.joystick.Horizontal;
        //joystickPos.y = joystickScript.joystick.Vertical;

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        rotation = transform.position - mousePos;


        rb.velocity = new Vector3(mousePos.x, mousePos.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);


    }
    */
}
